using reports.domain.Entities;
using reports.domain.Enums;
using reports.domain.Filters;

namespace reports.domain.Interfaces
{
    public interface ITaskRepository
    {
        Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IEnumerable<TaskItem>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<(IEnumerable<TaskItem> Items, int TotalCount)> GetFilteredAsync(TaskFilter filter, CancellationToken cancellationToken = default);
        Task AddAsync(TaskItem task, CancellationToken cancellationToken = default);
        void Update(TaskItem task);
        void Remove(TaskItem task);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<IEnumerable<IGrouping<TaskStatusEnum, TaskItem>>> GetTasksGroupedByStatusAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<IGrouping<string, TaskItem>>> GetTasksGroupedByResponsibleAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TaskItem>> GetOverdueTasksAsync(CancellationToken cancellationToken = default);
        Task<double> GetAverageCompletionTimeInDaysAsync(CancellationToken cancellationToken = default);
    }
}
