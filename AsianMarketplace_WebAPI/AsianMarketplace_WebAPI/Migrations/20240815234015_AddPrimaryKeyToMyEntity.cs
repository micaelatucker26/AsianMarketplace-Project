using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AsianMarketplace_WebAPI.Migrations
{
    public partial class AddPrimaryKeyToMyEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shopper",
                columns: table => new
                {
                    Username = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shopper__536C85E55B620307", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    CategoryName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SubCateg__737584F73EE87B95", x => x.Name);
                    table.ForeignKey(
                        name: "FK_SubCategory_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    OrderDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Username = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "Shopper_FK",
                        column: x => x.Username,
                        principalTable: "Shopper",
                        principalColumn: "Username");
                });

            migrationBuilder.CreateTable(
                name: "ShoppingList",
                columns: table => new
                {
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    UserID = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    IsActive = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValueSql: "('N')"),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shopping__FDCEE8177E466141", x => new { x.Title, x.UserID });
                    table.ForeignKey(
                        name: "ShoppingList_UserID_FK",
                        column: x => x.UserID,
                        principalTable: "Shopper",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    ImageURL = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    SubCategoryName = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemID);
                    table.ForeignKey(
                        name: "SubCategory_FK",
                        column: x => x.SubCategoryName,
                        principalTable: "SubCategory",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    ItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CartItem__A3060F2106CCBB11", x => new { x.ItemID, x.UserID });
                    table.ForeignKey(
                        name: "CartItem_ItemID_FK",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "CartItem_UserID_FK",
                        column: x => x.UserID,
                        principalTable: "Shopper",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    ItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderIte__9E478651E5C85267", x => new { x.ItemID, x.OrderID });
                    table.ForeignKey(
                        name: "OrderItem_ItemID_FK",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "OrderItem_OrderID_FK",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListItem",
                columns: table => new
                {
                    Title = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    IsCrossedOff = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true, defaultValueSql: "('N')"),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Shopping__17BC9694005304B1", x => new { x.Title, x.UserID, x.ItemID });
                    table.ForeignKey(
                        name: "ShoppingListItem_Item_FK",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ShoppingListItem_ShoppingList_FK",
                        columns: x => new { x.Title, x.UserID },
                        principalTable: "ShoppingList",
                        principalColumns: new[] { "Title", "UserID" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_UserID",
                table: "CartItem",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_SubCategoryName",
                table: "Item",
                column: "SubCategoryName");

            migrationBuilder.CreateIndex(
                name: "IX_Order_Username",
                table: "Order",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_OrderID",
                table: "OrderItem",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingList_UserID",
                table: "ShoppingList",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListItem_ItemID",
                table: "ShoppingListItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_CategoryId",
                table: "SubCategory",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "ShoppingListItem");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "ShoppingList");

            migrationBuilder.DropTable(
                name: "SubCategory");

            migrationBuilder.DropTable(
                name: "Shopper");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
