using Microsoft.AspNetCore.Mvc;
using reports.application.Requests.Reports;
using reports.application.Services;

namespace Api.Reports.Controllers;

/// <summary>
/// Endpoints analíticos e relatórios de tarefas.
/// </summary>
[ApiController]
[Route("api/relatorios")]
public class ReportsController : ControllerBase
{
    private readonly ITaskService _taskService;

    public ReportsController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    /// <summary>
    /// Retorna a quantidade de tarefas agrupadas por status.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>Lista com total de tarefas por status.</returns>
    [HttpGet("tarefas-por-status")]
    [ProducesResponseType(typeof(IEnumerable<TasksByStatusResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TasksByStatusResponse>>> GetTasksByStatus(
        CancellationToken cancellationToken)
    {
        var result = await _taskService.GetTasksByStatusAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retorna a quantidade de tarefas agrupadas por responsável.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>Lista com total de tarefas por responsável.</returns>
    [HttpGet("tarefas-por-responsavel")]
    [ProducesResponseType(typeof(IEnumerable<TasksByResponsibleResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TasksByResponsibleResponse>>> GetTasksByResponsible(
        CancellationToken cancellationToken)
    {
        var result = await _taskService.GetTasksByResponsibleAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retorna a lista de tarefas atrasadas.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>Lista de tarefas com vencimento expirado e ainda não concluídas.</returns>
    [HttpGet("tarefas-atrasadas")]
    [ProducesResponseType(typeof(IEnumerable<OverdueTaskResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<OverdueTaskResponse>>> GetOverdueTasks(
        CancellationToken cancellationToken)
    {
        var result = await _taskService.GetOverdueTasksAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Retorna o tempo médio de conclusão das tarefas.
    /// </summary>
    /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
    /// <returns>Tempo médio, em dias, para conclusão das tarefas.</returns>
    [HttpGet("tempo-medio-conclusao")]
    [ProducesResponseType(typeof(AverageCompletionTimeResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult<AverageCompletionTimeResponse>> GetAverageCompletionTime(
        CancellationToken cancellationToken)
    {
        var result = await _taskService.GetAverageCompletionTimeAsync(cancellationToken);
        return Ok(result);
    }
}