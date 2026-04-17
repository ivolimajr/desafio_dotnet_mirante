using reports.application.Requests.Reports;
using reports.application.Requests.Tasks;
using reports.application.Responses.Tasks;
using reports.domain.Common;
using reports.domain.Filters;

namespace reports.application.Services
{
    public interface ITaskService
    {
        Task<TaskResponse?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PagedResult<TaskResponse>> GetFilteredAsync(TaskFilter filter, CancellationToken cancellationToken = default);
        Task<TaskResponse> CreateAsync(CreateTaskRequest request, CancellationToken cancellationToken = default);
        Task<TaskResponse?> UpdateAsync(Guid id, UpdateTaskRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);

        Task<IEnumerable<TasksByStatusResponse>> GetTasksByStatusAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<TasksByResponsibleResponse>> GetTasksByResponsibleAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<OverdueTaskResponse>> GetOverdueTasksAsync(CancellationToken cancellationToken = default);
        Task<AverageCompletionTimeResponse> GetAverageCompletionTimeAsync(CancellationToken cancellationToken = default);
    }
}
