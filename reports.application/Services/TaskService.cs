using Microsoft.VisualBasic;
using reports.application.Requests.Reports;
using reports.application.Requests.Tasks;
using reports.application.Responses;
using reports.application.Responses.Tasks;
using reports.domain.Common;
using reports.domain.Entities;
using reports.domain.Enums;
using reports.domain.Filters;
using reports.domain.Interfaces;

namespace reports.application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskResponse> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken = default)
    {
        var entity = new TaskItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            DueDate = DateTime.SpecifyKind(request.DueDate, DateTimeKind.Utc),
            Priority = request.Priority,
            Responsible = request.Responsible,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(entity, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);

        return Map(entity);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return false;

        _repository.Remove(entity);
        await _repository.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<TaskResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity is null ? null : Map(entity);
    }

    public async Task<PagedResult<TaskResponse>> GetFilteredAsync(TaskFilter filter, CancellationToken cancellationToken = default)
    {
        var (items, totalCount) = await _repository.GetFilteredAsync(filter, cancellationToken);

        return new PagedResult<TaskResponse>
        {
            Items = items.Select(Map),
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<TaskResponse?> UpdateAsync(Guid id, UpdateTaskRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        if (entity is null) return null;

        entity.Title = request.Title;
        entity.Description = request.Description;
        entity.Status = request.Status;
        entity.DueDate = DateTime.SpecifyKind(request.DueDate, DateTimeKind.Utc);
        entity.Priority = request.Priority;
        entity.Responsible = request.Responsible;
        entity.CompletedAt = request.Status == TaskStatusEnum.Completed
            ? request.CompletedAt ?? DateTime.UtcNow
            : null;

        _repository.Update(entity);
        await _repository.SaveChangesAsync(cancellationToken);

        return Map(entity);
    }

    public async Task<IEnumerable<TasksByStatusResponse>> GetTasksByStatusAsync(CancellationToken cancellationToken = default)
    {
        var grouped = await _repository.GetTasksGroupedByStatusAsync(cancellationToken);

        return grouped.Select(g => new TasksByStatusResponse
        {
            Status = g.Key,
            Total = g.Count()
        });
    }

    public async Task<IEnumerable<TasksByResponsibleResponse>> GetTasksByResponsibleAsync(CancellationToken cancellationToken = default)
    {
        var grouped = await _repository.GetTasksGroupedByResponsibleAsync(cancellationToken);

        return grouped.Select(g => new TasksByResponsibleResponse
        {
            Responsible = g.Key,
            Total = g.Count()
        });
    }

    public async Task<IEnumerable<OverdueTaskResponse>> GetOverdueTasksAsync(CancellationToken cancellationToken = default)
    {
        var tasks = await _repository.GetOverdueTasksAsync(cancellationToken);

        return tasks.Select(x => new OverdueTaskResponse
        {
            Id = x.Id,
            Title = x.Title,
            Responsible = x.Responsible,
            DueDate = x.DueDate,
            Priority = x.Priority,
            DaysLate = (DateTime.UtcNow.Date - x.DueDate.Date).Days
        });
    }

    public async Task<AverageCompletionTimeResponse> GetAverageCompletionTimeAsync(CancellationToken cancellationToken = default)
    {
        var average = await _repository.GetAverageCompletionTimeInDaysAsync(cancellationToken);

        return new AverageCompletionTimeResponse
        {
            AverageDaysToComplete = average
        };
    }

    public IEnumerable<EnumItemResponse> GetTaskStatus()
    {
        return Enum.GetValues<TaskStatusEnum>()
                       .Cast<TaskStatusEnum>()
                       .Select(x => new EnumItemResponse
                       {
                           Id = (int)x,
                           Name = x.ToString()
                       });
    }

    public IEnumerable<EnumItemResponse> GetTaskPriority()
    {
        return Enum.GetValues<TaskPriorityEnum>()
                       .Cast<TaskPriorityEnum>()
                       .Select(x => new EnumItemResponse
                       {
                           Id = (int)x,
                           Name = x.ToString()
                       });
    }

    private static TaskResponse Map(TaskItem entity)
    {
        return new TaskResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Status = entity.Status,
            CreatedAt = entity.CreatedAt,
            DueDate = entity.DueDate,
            CompletedAt = entity.CompletedAt,
            Priority = entity.Priority,
            Responsible = entity.Responsible
        };
    }
}