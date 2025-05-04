using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolManagmentSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
        name: "School");

            // Rename all tables from dbo to School schema
            migrationBuilder.RenameTable(
                name: "Subjects",
                schema: "dbo",
                newName: "Subjects",
                newSchema: "School");

            migrationBuilder.RenameTable(
                name: "StudentSubjects",
                schema: "dbo",
                newName: "StudentSubjects",
                newSchema: "School");

            migrationBuilder.RenameTable(
                name: "Students",
                schema: "dbo",
                newName: "Students",
                newSchema: "School");

            migrationBuilder.RenameTable(
                name: "Instructors",
                schema: "dbo",
                newName: "Instructors",
                newSchema: "School");

            migrationBuilder.RenameTable(
                name: "Ins_Subjects",
                schema: "dbo",
                newName: "Ins_Subjects",
                newSchema: "School");

            migrationBuilder.RenameTable(
                name: "DepartmetSubjects",
                schema: "dbo",
                newName: "DepartmetSubjects",
                newSchema: "School");

            migrationBuilder.RenameTable(
                name: "Departments",
                schema: "dbo",
                newName: "Departments",
                newSchema: "School");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectNameEn",
                schema: "School",
                table: "Subjects",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectNameAr",
                schema: "School",
                table: "Subjects",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "School",
                table: "Students",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                schema: "School",
                table: "Students",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                schema: "School",
                table: "Students",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                schema: "School",
                table: "Students",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DNameEn",
                schema: "School",
                table: "Departments",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DNameAr",
                schema: "School",
                table: "Departments",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Period",
                schema: "School",
                table: "Subjects",
                column: "Period");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Subjects_Period",
                schema: "School",
                table: "Subjects");

            migrationBuilder.RenameTable(
                name: "Subjects",
                schema: "School",
                newName: "Subjects");

            migrationBuilder.RenameTable(
                name: "StudentSubjects",
                schema: "School",
                newName: "StudentSubjects");

            migrationBuilder.RenameTable(
                name: "Students",
                schema: "School",
                newName: "Students");

            migrationBuilder.RenameTable(
                name: "Instructors",
                schema: "School",
                newName: "Instructors");

            migrationBuilder.RenameTable(
                name: "Ins_Subjects",
                schema: "School",
                newName: "Ins_Subjects");

            migrationBuilder.RenameTable(
                name: "DepartmetSubjects",
                schema: "School",
                newName: "DepartmetSubjects");

            migrationBuilder.RenameTable(
                name: "Departments",
                schema: "School",
                newName: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectNameEn",
                table: "Subjects",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectNameAr",
                table: "Subjects",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameEn",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NameAr",
                table: "Students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Students",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DNameEn",
                table: "Departments",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DNameAr",
                table: "Departments",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(150)",
                oldMaxLength: 150,
                oldNullable: true);
        }
    }
}
