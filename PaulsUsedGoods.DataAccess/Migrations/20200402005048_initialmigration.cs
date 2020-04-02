using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaulsUsedGoods.DataAccess.Migrations
{
    public partial class initialmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sellers",
                columns: table => new
                {
                    SellerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SellerName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sellers", x => x.SellerId);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreId);
                });

            migrationBuilder.CreateTable(
                name: "TopicOptions",
                columns: table => new
                {
                    TopicOptionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TopicName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicOptions", x => x.TopicOptionId);
                });

            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 128, nullable: false),
                    LastName = table.Column<string>(maxLength: 128, nullable: false),
                    Username = table.Column<string>(maxLength: 128, nullable: false),
                    Password = table.Column<string>(maxLength: 30, nullable: false),
                    Employee = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                    table.ForeignKey(
                        name: "FK_People_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    OrderDate = table.Column<DateTime>(nullable: false),
                    TotalOrderPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_People_OrderId",
                        column: x => x.OrderId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonId = table.Column<int>(nullable: false),
                    SellerId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "SellerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(nullable: false),
                    OrderId = table.Column<int>(nullable: true),
                    SellerId = table.Column<int>(nullable: false),
                    TopicId = table.Column<int>(nullable: false),
                    ItemName = table.Column<string>(maxLength: 128, nullable: false),
                    ItemDescription = table.Column<string>(maxLength: 1024, nullable: true),
                    ItemPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Sellers_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Sellers",
                        principalColumn: "SellerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Items_TopicOptions_TopicId",
                        column: x => x.TopicId,
                        principalTable: "TopicOptions",
                        principalColumn: "TopicOptionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreId", "LocationName" },
                values: new object[] { 1, "Arlington,TX" });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "Employee", "FirstName", "LastName", "Password", "StoreId", "Username" },
                values: new object[] { 1, true, "ad", "min", "admin", 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SellerId",
                table: "Items",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_StoreId",
                table: "Items",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_TopicId",
                table: "Items",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PersonId",
                table: "Orders",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_People_StoreId",
                table: "People",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PersonId",
                table: "Reviews",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_SellerId",
                table: "Reviews",
                column: "SellerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "TopicOptions");

            migrationBuilder.DropTable(
                name: "Sellers");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}
