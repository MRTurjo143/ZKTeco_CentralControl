using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZKTeco.Migrations
{
    public partial class empCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "empCode",
                table: "HR_Emp_DeviceInfoHIK",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "empCode",
                table: "HR_Emp_DeviceInfoHIK");
        }
    }
}
