using reports.domain.Enums;

namespace reports.domain.Entities;


public class TaskItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public TaskStatusEnum Status { get; set; } = TaskStatusEnum.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; }
    public DateTime? CompletedAt { get; set; }
    public TaskPriorityEnum Priority { get; set; } = TaskPriorityEnum.Medium;
    public string Responsible { get; set; } = null!;
}