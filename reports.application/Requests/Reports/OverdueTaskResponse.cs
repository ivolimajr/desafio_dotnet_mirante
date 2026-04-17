using reports.domain.Enums;

namespace reports.application.Requests.Reports;

public class OverdueTaskResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Responsible { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public TaskPriorityEnum Priority { get; set; }
    public int DaysLate { get; set; }
}