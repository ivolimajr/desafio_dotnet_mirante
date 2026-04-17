using reports.domain.Enums;

namespace reports.application.Responses.Tasks;

public class TaskResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public TaskStatusEnum Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public TaskPriorityEnum Priority { get; set; }
    public string Responsible { get; set; } = null!;
}