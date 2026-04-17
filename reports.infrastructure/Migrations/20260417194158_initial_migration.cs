using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reports.infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mirante-reports");

            migrationBuilder.CreateTable(
                name: "TaskItem",
                schema: "mirante-reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    Responsible = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItem", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_DueDate",
                schema: "mirante-reports",
                table: "TaskItem",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_Responsible",
                schema: "mirante-reports",
                table: "TaskItem",
                column: "Responsible");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItem_Status",
                schema: "mirante-reports",
                table: "TaskItem",
                column: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskItem",
                schema: "mirante-reports");
        }
    }
}
