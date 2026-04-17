using Microsoft.AspNetCore.Mvc;
using reports.application.Requests.Reports;
using reports.application.Requests.Tasks;
using reports.application.Responses;
using reports.application.Responses.Tasks;
using reports.application.Services;
using reports.domain.Common;
using reports.domain.Enums;
using reports.domain.Filters;

namespace Api.Reports.Controllers;

/// <summary>
/// Endpoints para gerenciamento de tarefas.
/// </summary>
[ApiController]
[Route("api/tasks")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Lista tarefas com filtros e paginação.
    /// </summary>
    /// <param name="status">Filtra pelo status da tarefa.</param>
    /// <param name="responsible">Filtra pelo responsável.</param>
    /// <param name="createdFrom">Data inicial de criação.</param>
    /// <param name="createdTo">Data final de criação.</param>
    /// <param name="dueDateFrom">Data inicial de vencimento.</param>
    /// <param name="dueDateTo">Data final de vencimento.</param>
    /// <param name="priority">Filtra pela prioridade.</param>
    /// <param name="pageNumber">Número da página. Padrão: 1.</param>
    /// <param name="pageSize">Quantidade de itens por página. Padrão: 10.</param>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>Lista paginada de tarefas.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResult<TaskResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<TaskResponse>>> GetAll(
        [FromQuery] TaskStatusEnum? status,
        [FromQuery] string? responsible,
        [FromQuery] DateTime? createdFrom,
        [FromQuery] DateTime? createdTo,
        [FromQuery] DateTime? dueDateFrom,
        [FromQuery] DateTime? dueDateTo,
        [FromQuery] TaskPriorityEnum? priority,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var filter = new TaskFilter
        {
            Status = status,
            Responsible = responsible,
            CreatedFrom = createdFrom,
            CreatedTo = createdTo,
            DueDateFrom = dueDateFrom,
            DueDateTo = dueDateTo,
            Priority = priority,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        var result = await _taskService.GetFilteredAsync(filter, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retorna uma tarefa pelo identificador.
    /// </summary>
    /// <param name="id">Identificador da tarefa.</param>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>A tarefa encontrada.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var task = await _taskService.GetByIdAsync(id, cancellationToken);

        if (task is null)
            return NotFound();

        return Ok(task);
    }

    /// <summary>
    /// Cria uma nova tarefa.
    /// </summary>
    /// <param name="request">Dados da tarefa a ser criada.</param>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>A tarefa criada.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TaskResponse>> Create(
        [FromBody] CreateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var created = await _taskService.CreateAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Atualiza uma tarefa existente.
    /// </summary>
    /// <param name="id">Identificador da tarefa.</param>
    /// <param name="request">Novos dados da tarefa.</param>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>A tarefa atualizada.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(TaskResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TaskResponse>> Update(
        Guid id,
        [FromBody] UpdateTaskRequest request,
        CancellationToken cancellationToken)
    {
        var updated = await _taskService.UpdateAsync(id, request, cancellationToken);

        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    /// <summary>
    /// Remove uma tarefa.
    /// </summary>
    /// <param name="id">Identificador da tarefa.</param>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>Sem conteúdo quando removido com sucesso.</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _taskService.DeleteAsync(id, cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Retorna os status que um pedido pode ter
    /// </summary>
    /// <returns></returns>
    [HttpGet("lista-de-status")]
    [ProducesResponseType(typeof(IEnumerable<EnumItemResponse>), StatusCodes.Status200OK)]
    public IActionResult GetTaskStatuses()
    {
        return Ok(_taskService.GetTaskStatus());
    }

    /// <summary>
    /// Retorna as prioridades que um pedido pode ter
    /// </summary>
    /// <returns></returns>
    [HttpGet("lista-de-prioridades")]
    [ProducesResponseType(typeof(IEnumerable<EnumItemResponse>), StatusCodes.Status200OK)]
    public IActionResult GetTaskPriorities()
    {
        return Ok(_taskService.GetTaskPriority());
    }
}