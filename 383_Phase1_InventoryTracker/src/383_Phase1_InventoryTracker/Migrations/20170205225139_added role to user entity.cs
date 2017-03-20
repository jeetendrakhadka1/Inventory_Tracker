using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace _383_Phase1_InventoryTracker.Migrations
{
    public partial class addedroletouserentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Users");
        }
    }
}
