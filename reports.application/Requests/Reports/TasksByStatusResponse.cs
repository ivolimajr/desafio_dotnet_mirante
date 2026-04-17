using reports.domain.Enums;

namespace reports.application.Requests.Reports;

public class TasksByStatusResponse
{
    public TaskStatusEnum Status { get; set; }
    public int Total { get; set; }
}