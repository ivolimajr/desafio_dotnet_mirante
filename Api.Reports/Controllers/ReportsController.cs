using Microsoft.AspNetCore.Mvc;
using reports.application.Services;
using reports.domain.Interfaces;

namespace Api.Reports.Controllers;

[ApiController]
[Route("api/relatorios")]
public class ReportsController : ControllerBase
{
    private readonly ITaskService _taskService;

    public ReportsController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet("tarefas-por-status")]
    public async Task<IActionResult> GetTasksByStatus(CancellationToken cancellationToken)
    {
        var result = await _taskService.GetTasksByStatusAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("tarefas-por-responsavel")]
    public async Task<IActionResult> GetTasksByResponsible(CancellationToken cancellationToken)
    {
        var result = await _taskService.GetTasksByResponsibleAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("tarefas-atrasadas")]
    public async Task<IActionResult> GetOverdueTasks(CancellationToken cancellationToken)
    {
        var result = await _taskService.GetOverdueTasksAsync(cancellationToken);
        return Ok(result);
    }

    [HttpGet("tempo-medio-conclusao")]
    public async Task<IActionResult> GetAverageCompletionTime(CancellationToken cancellationToken)
    {
        var result = await _taskService.GetAverageCompletionTimeAsync(cancellationToken);
        return Ok(result);
    }
}