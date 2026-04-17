using reports.domain.Enums;

namespace reports.domain.Filters;

public class TaskFilter
{
    public TaskStatusEnum? Status { get; set; }
    public string? Responsible { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public DateTime? DueDateFrom { get; set; }
    public DateTime? DueDateTo { get; set; }
    public TaskPriorityEnum? Priority { get; set; }

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}