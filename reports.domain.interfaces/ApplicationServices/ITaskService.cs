using reports.domain.Common;
using reports.domain.Filters;
using reports.domain.Requests.Reports;
using reports.domain.Requests.Tasks;
using reports.domain.Responses.Tasks;

namespace reports.domain.interfaces.ApplicationServices
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
