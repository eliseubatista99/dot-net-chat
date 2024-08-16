using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetChatApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    ChatId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ChatMembers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatMessages = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.ChatId);
                });

            migrationBuilder.CreateTable(
                name: "MessageReceivers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MessageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReadDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReceivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Sender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Receivers = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "MessageReceivers");

            migrationBuilder.DropTable(
                name: "Messages");
        }
    }
}
