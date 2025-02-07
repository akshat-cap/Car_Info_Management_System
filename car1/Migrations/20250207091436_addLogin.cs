using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace car1.Migrations
{
    /// <inheritdoc />
    public partial class addLogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          
                // Create the UserTypes table
                migrationBuilder.CreateTable(
                    name: "UserTypes",
                    columns: table => new
                    {
                        id = table.Column<int>(nullable: false)
                            .Annotation("SqlServer:Identity", "1, 1"),
                        type = table.Column<string>(nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_UserTypes", x => x.id);
                    });

                // Modify the Logins table to use a fixed length for Username (max 255 characters)
                migrationBuilder.CreateTable(
                    name: "Logins",
                    columns: table => new
                    {
                        Username = table.Column<string>(maxLength: 255, nullable: false),
                        Password = table.Column<string>(nullable: true),
                        UserTypeId = table.Column<int>(nullable: true)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Logins", x => x.Username);
                        table.ForeignKey(
                            name: "FK_Logins_UserTypes_UserTypeId",
                            column: x => x.UserTypeId,
                            principalTable: "UserTypes",
                            principalColumn: "id",
                            onDelete: ReferentialAction.Restrict);
                    });

                // Create an index for UserTypeId for better lookup performance
                migrationBuilder.CreateIndex(
                    name: "IX_Logins_UserTypeId",
                    table: "Logins",
                    column: "UserTypeId");
            }

          


            



            /// <inheritdoc />
            protected override void Down(MigrationBuilder migrationBuilder)
            {

            }
        }
    }





