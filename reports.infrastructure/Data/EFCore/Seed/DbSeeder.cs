using reports.domain.Entities;
using reports.domain.Enums;
using reports.infrastructure.Data.Context;

namespace reports.infrastructure.Data.Seed;

public static class DbSeeder
{
    public static async Task SeedAsync(PostgresContext context)
    {
        //Impede que a semente seja executada mais de uma vez, evitando dados duplicados
        if (context.Tasks.Any())
            return;

        var random = new Random();

        var responsaveis = new[]
        {
            "Pedro",
            "João",
            "Maria",
            "Ana",
            "Carlos"
        };

        var tarefas = new List<TaskItem>();

        for (int i = 1; i <= 50; i++)
        {
            var createdAt = DateTime.UtcNow.AddDays(-random.Next(1, 60));
            var dueDate = createdAt.AddDays(random.Next(1, 20));

            var status = (TaskStatusEnum)random.Next(1, 4);
            DateTime? completedAt = null;

            if (status == TaskStatusEnum.Completed)
            {
                completedAt = createdAt.AddDays(random.Next(1, 15));
            }

            tarefas.Add(new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = $"Tarefa {i}",
                Description = $"Descrição da tarefa {i}",
                CreatedAt = createdAt,
                DueDate = dueDate,
                CompletedAt = completedAt,
                Status = status,
                Priority = (TaskPriorityEnum)random.Next(1, 4),
                Responsible = responsaveis[random.Next(responsaveis.Length)]
            });
        }

        await context.Tasks.AddRangeAsync(tarefas);
        await context.SaveChangesAsync();
    }
}