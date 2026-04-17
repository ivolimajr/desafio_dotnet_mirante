using reports.domain.Enums;

namespace reports.application.Requests.Tasks;

public class CreateTaskRequest
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriorityEnum Priority { get; set; }
    public string Responsible { get; set; } = null!;
}