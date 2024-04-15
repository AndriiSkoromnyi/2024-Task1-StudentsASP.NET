using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Students.Common.Migrations
{
    /// <inheritdoc />
    public partial class RelacionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LectureRoomId",
                table: "Subject",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subject_LectureRoomId",
                table: "Subject",
                column: "LectureRoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subject_LectureRoom_LectureRoomId",
                table: "Subject",
                column: "LectureRoomId",
                principalTable: "LectureRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subject_LectureRoom_LectureRoomId",
                table: "Subject");

            migrationBuilder.DropIndex(
                name: "IX_Subject_LectureRoomId",
                table: "Subject");

            migrationBuilder.DropColumn(
                name: "LectureRoomId",
                table: "Subject");
        }
    }
}
