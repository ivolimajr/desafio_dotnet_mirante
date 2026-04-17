using Microsoft.EntityFrameworkCore;
using reports.domain.Entities;
using reports.domain.Enums;
using reports.domain.Filters;
using reports.domain.Interfaces;
using reports.infrastructure.Data.Context;

namespace reports.infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly PostgresContext _context;

    public TaskRepository(PostgresContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
    {
        await _context.Tasks.AddAsync(task, cancellationToken);
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Tasks
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetFilteredAsync(
        TaskFilter filter,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Tasks.AsQueryable();

        if (filter.Status.HasValue)
            query = query.Where(x => x.Status == filter.Status.Value);

        if (!string.IsNullOrWhiteSpace(filter.Responsible))
            query = query.Where(x => x.Responsible.ToLower() == filter.Responsible.ToLower());

        if (filter.CreatedFrom.HasValue)
            query = query.Where(x => x.CreatedAt >= filter.CreatedFrom.Value);

        if (filter.CreatedTo.HasValue)
            query = query.Where(x => x.CreatedAt <= filter.CreatedTo.Value);

        if (filter.DueDateFrom.HasValue)
            query = query.Where(x => x.DueDate >= filter.DueDateFrom.Value);

        if (filter.DueDateTo.HasValue)
            query = query.Where(x => x.DueDate <= filter.DueDateTo.Value);

        if (filter.Priority.HasValue)
            query = query.Where(x => x.Priority == filter.Priority.Value);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<IEnumerable<IGrouping<TaskStatusEnum, TaskItem>>> GetTasksGroupedByStatusAsync(
        CancellationToken cancellationToken = default)
    {
        var items = await _context.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return items.GroupBy(x => x.Status);
    }

    public async Task<IEnumerable<IGrouping<string, TaskItem>>> GetTasksGroupedByResponsibleAsync(
        CancellationToken cancellationToken = default)
    {
        var items = await _context.Tasks
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return items.GroupBy(x => x.Responsible);
    }

    public async Task<IEnumerable<TaskItem>> GetOverdueTasksAsync(CancellationToken cancellationToken = default)
    {
        var today = DateTime.UtcNow.Date;

        return await _context.Tasks
            .AsNoTracking()
            .Where(x => x.Status != TaskStatusEnum.Completed && x.DueDate.Date < today)
            .OrderBy(x => x.DueDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<double> GetAverageCompletionTimeInDaysAsync(CancellationToken cancellationToken = default)
    {
        var completedTasks = await _context.Tasks
            .AsNoTracking()
            .Where(x => x.Status == TaskStatusEnum.Completed && x.CompletedAt.HasValue)
            .ToListAsync(cancellationToken);

        if (!completedTasks.Any())
            return 0;

        return completedTasks
            .Average(x => (x.CompletedAt!.Value - x.CreatedAt).TotalDays);
    }

    public void Remove(TaskItem task)
    {
        _context.Tasks.Remove(task);
    }

    public void Update(TaskItem task)
    {
        _context.Tasks.Update(task);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}