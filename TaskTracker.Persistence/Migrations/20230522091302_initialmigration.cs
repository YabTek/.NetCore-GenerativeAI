using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskTracker.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fullname = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Owner = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_User",
                        column: x => x.Owner,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "checklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    associated_task = table.Column<int>(type: "integer", nullable: false),
                    Start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    End_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checklists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Task_Checklist",
                        column: x => x.associated_task,
                        principalTable: "tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Fullname", "Password" },
                values: new object[,]
                {
                    { 1, "kebede@gmail.com", "Abebe Kebede", "abebe123@" },
                    { 2, "helen@gmail.com", "Helen Kebede", "helen123@" }
                });

            migrationBuilder.InsertData(
                table: "tasks",
                columns: new[] { "Id", "Description", "End_date", "Owner", "Start_date", "Status", "Title" },
                values: new object[,]
                {
                    { 3, "This task is attending meeting", new DateTime(2023, 5, 22, 12, 13, 2, 90, DateTimeKind.Local).AddTicks(5109), 1, new DateTime(2023, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), 0, "Attend meeting" },
                    { 4, "This task is attending class", new DateTime(2023, 5, 22, 12, 13, 2, 90, DateTimeKind.Local).AddTicks(5114), 1, new DateTime(2023, 5, 22, 0, 0, 0, 0, DateTimeKind.Local), 0, "Attend class" }
                });

            migrationBuilder.InsertData(
                table: "checklists",
                columns: new[] { "Id", "Description", "End_date", "Start_date", "Status", "Title", "associated_task" },
                values: new object[,]
                {
                    { 1, "checklist for a meeting agenda", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "create meeting agenda", 3 },
                    { 2, "checklist for packing things", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "pack things", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_checklists_associated_task",
                table: "checklists",
                column: "associated_task");

            migrationBuilder.CreateIndex(
                name: "IX_tasks_Owner",
                table: "tasks",
                column: "Owner");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checklists");

            migrationBuilder.DropTable(
                name: "tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
