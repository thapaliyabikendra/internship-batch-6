using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetManagementApp.Migrations
{
    /// <inheritdoc />
    public partial class CreatesForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetCategories_CategoryNameId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Departments_OwnByDepartmentId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_OwnByDepartmentId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "OwnByDepartmentId",
                table: "Assets",
                newName: "DeprtmentId");

            migrationBuilder.RenameColumn(
                name: "CategoryNameId",
                table: "Assets",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_CategoryNameId",
                table: "Assets",
                newName: "IX_Assets_DepartmentId");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetCategoryId",
                table: "Assets",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetCategoryId",
                table: "Assets",
                column: "AssetCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetCategories_AssetCategoryId",
                table: "Assets",
                column: "AssetCategoryId",
                principalTable: "AssetCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Departments_DepartmentId",
                table: "Assets",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assets_AssetCategories_AssetCategoryId",
                table: "Assets");

            migrationBuilder.DropForeignKey(
                name: "FK_Assets_Departments_DepartmentId",
                table: "Assets");

            migrationBuilder.DropIndex(
                name: "IX_Assets_AssetCategoryId",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "AssetCategoryId",
                table: "Assets");

            migrationBuilder.RenameColumn(
                name: "DeprtmentId",
                table: "Assets",
                newName: "OwnByDepartmentId");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "Assets",
                newName: "CategoryNameId");

            migrationBuilder.RenameIndex(
                name: "IX_Assets_DepartmentId",
                table: "Assets",
                newName: "IX_Assets_CategoryNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OwnByDepartmentId",
                table: "Assets",
                column: "OwnByDepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_AssetCategories_CategoryNameId",
                table: "Assets",
                column: "CategoryNameId",
                principalTable: "AssetCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Assets_Departments_OwnByDepartmentId",
                table: "Assets",
                column: "OwnByDepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
