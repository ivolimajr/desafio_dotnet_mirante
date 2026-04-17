using reports.domain.Enums;

namespace reports.domain.Filters;

public class TaskFilter
{
    /// <summary>
    /// Filtra pelo status da tarefa.
    /// </summary>
    public TaskStatusEnum? Status { get; set; }

    /// <summary>
    /// Filtra pelo responsável.
    /// </summary>
    public string? Responsible { get; set; }

    /// <summary>
    /// Data inicial de criação.
    /// </summary>
    public DateTime? CreatedFrom { get; set; }

    /// <summary>
    /// Data final de criação.
    /// </summary>
    public DateTime? CreatedTo { get; set; }

    /// <summary>
    /// Data inicial de vencimento.
    /// </summary>
    public DateTime? DueDateFrom { get; set; }

    /// <summary>
    /// Data final de vencimento.
    /// </summary>
    public DateTime? DueDateTo { get; set; }

    /// <summary>
    /// Filtra pela prioridade.
    /// </summary>
    public TaskPriorityEnum? Priority { get; set; }

    /// <summary>
    /// Número da página.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Quantidade de itens por página.
    /// </summary>
    public int PageSize { get; set; } = 10;
}