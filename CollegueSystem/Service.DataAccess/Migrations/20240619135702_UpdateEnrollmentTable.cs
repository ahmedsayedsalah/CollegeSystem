using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Service.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEnrollmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolllments_Courses_CourseId",
                table: "Enrolllments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolllments_Students_StudentId",
                table: "Enrolllments");

            migrationBuilder.DropIndex(
                name: "IX_Enrolllments_CourseId",
                table: "Enrolllments");

            migrationBuilder.DropIndex(
                name: "IX_Enrolllments_StudentId",
                table: "Enrolllments");

            //migrationBuilder.DropColumn(
            //    name: "CrsId",
            //    table: "Enrolllments");

            //migrationBuilder.DropColumn(
            //    name: "StdId",
            //    table: "Enrolllments");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolllments_CrsId",
                table: "Enrolllments",
                column: "CrsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolllments_Courses_CrsId",
                table: "Enrolllments",
                column: "CrsId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolllments_Students_StdId",
                table: "Enrolllments",
                column: "StdId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrolllments_Courses_CrsId",
                table: "Enrolllments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrolllments_Students_StdId",
                table: "Enrolllments");

            migrationBuilder.DropIndex(
                name: "IX_Enrolllments_CrsId",
                table: "Enrolllments");

            //migrationBuilder.AddColumn<int>(
            //    name: "CourseId",
            //    table: "Enrolllments",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<int>(
            //    name: "StudentId",
            //    table: "Enrolllments",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enrolllments_CourseId",
                table: "Enrolllments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrolllments_StudentId",
                table: "Enrolllments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolllments_Courses_CourseId",
                table: "Enrolllments",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrolllments_Students_StudentId",
                table: "Enrolllments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
