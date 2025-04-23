using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechStation.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brends",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrendName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brends", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Catalogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Catalogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CatalogId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Catalogs_CatalogId",
                        column: x => x.CatalogId,
                        principalTable: "Catalogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Banners",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrendId = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banners_Brends_BrendId",
                        column: x => x.BrendId,
                        principalTable: "Brends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Banners_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<long>(type: "bigint", nullable: false),
                    Images = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BrendId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brends_BrendId",
                        column: x => x.BrendId,
                        principalTable: "Brends",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentId = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Brends",
                columns: new[] { "Id", "BrendName", "CreatedAt", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, "Dell", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9255), new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9257) },
                    { 2L, "Logitech", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9258), new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9259) },
                    { 3L, "Corsair", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9260), new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9260) },
                    { 4L, "HP", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9261), new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9261) },
                    { 5L, "Samsung", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9262), new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(9263) }
                });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(4252), "Computers & Laptops", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(4252) },
                    { 2L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(4254), "Gaming & Peripherals", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(4254) },
                    { 3L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(4256), "Monitors & Displays", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(4256) }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "CreatedAt", "PaymentMethod", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 999.99m, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2207), "Credit Card", 1, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2207) },
                    { 2L, 259.98m, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2209), "PayPal", 2, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2210) },
                    { 3L, 199.99m, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2211), "Credit Card", 1, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2212) },
                    { 4L, 1399.99m, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2213), "Credit Card", 1, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2214) },
                    { 5L, 749.99m, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2215), "PayPal", 2, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(2215) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Role", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8733), "john@example.com", "John", "Doe", "Password123", "123-456-7890", 0, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8733), "johndoe" },
                    { 2L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8736), "jane@example.com", "Jane", "Smith", "Password123", "123-456-7890", 0, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8737), "janesmith" },
                    { 3L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8739), "alice@example.com", "Alice", "Johnson", "Password123", "123-456-7890", 0, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8739), "alicejohnson" },
                    { 4L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8741), "bob@example.com", "Bob", "Brown", "Password123", "123-456-7890", 0, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8741), "bobbrown" },
                    { 5L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8742), "charlie@example.com", "Charlie", "Davis", "Password123", "123-456-7890", 0, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(8743), "charliedavis" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CatalogId", "CategoryName", "CreatedAt", "Description", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, "Laptops", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6534), "Portable computers for work and play", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6534) },
                    { 2L, 1L, "Accessories", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6536), "Computer accessories including mice, keyboards, and more", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6537) },
                    { 3L, 2L, "Gaming", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6538), "Gaming equipment including chairs, desks, and peripherals", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6538) },
                    { 4L, 1L, "Monitors", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6540), "High-quality monitors for work and entertainment", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6540) },
                    { 5L, 1L, "Desktops", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6541), "Powerful desktop computers for gaming and work", new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(6542) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "Role", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(803), 2, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(804), 1L },
                    { 2L, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(806), 1, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(806), 2L },
                    { 3L, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(808), 1, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(808), 3L },
                    { 4L, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(809), 3, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(809), 4L },
                    { 5L, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(811), 1, new DateTime(2025, 4, 23, 7, 33, 28, 203, DateTimeKind.Utc).AddTicks(811), 5L }
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "BrendId", "CategoryId", "CreatedAt", "Images", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(2788), "banner_computers.jpg", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(2917) },
                    { 2L, 2L, 2L, new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(3008), "banner_accessories.jpg", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(3008) },
                    { 3L, 3L, 3L, new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(3010), "banner_gaming.jpg", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(3010) },
                    { 4L, 4L, 1L, new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(3011), "banner_laptops.jpg", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(3012) },
                    { 5L, 5L, 2L, new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(3013), "banner_monitors.jpg", new DateTime(2025, 4, 23, 7, 33, 28, 200, DateTimeKind.Utc).AddTicks(3013) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrendId", "CategoryId", "CreatedAt", "Description", "Images", "Price", "ProductName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5837), "A powerful and sleek laptop for professionals.", "dell_xps_13.jpg", 999.99m, "Dell XPS 13", new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5838) },
                    { 2L, 2L, 2L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5841), "A high-performance gaming mouse.", "logitech_g_pro_x.jpg", 129.99m, "Logitech G Pro X", new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5841) },
                    { 3L, 3L, 2L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5843), "Mechanical gaming keyboard with RGB lighting.", "corsair_k95_rgb_platinum.jpg", 199.99m, "Corsair K95 RGB Platinum", new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5843) },
                    { 4L, 4L, 1L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5845), "A gaming laptop with top-tier specs.", "hp_omen_15.jpg", 1399.99m, "HP Omen 15", new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5845) },
                    { 5L, 5L, 4L, new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5847), "A curved gaming monitor with 240Hz refresh rate.", "samsung_odyssey_g7.jpg", 749.99m, "Samsung Odyssey G7", new DateTime(2025, 4, 23, 7, 33, 28, 202, DateTimeKind.Utc).AddTicks(5847) }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CreatedAt", "ProductId", "Quantity", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2212), 1L, 1m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2213), 1L },
                    { 2L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2231), 2L, 2m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2231), 2L },
                    { 3L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2232), 3L, 1m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2233), 3L },
                    { 4L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2234), 4L, 1m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2234), 4L },
                    { 5L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2235), 5L, 1m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(2236), 5L }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "ProductId", "Quantity", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9511), 1L, 1, 999.99m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9511), 1L },
                    { 2L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9514), 2L, 2, 259.98m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9514), 2L },
                    { 3L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9516), 3L, 1, 199.99m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9517), 3L },
                    { 4L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9518), 4L, 1, 1399.99m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9519), 4L },
                    { 5L, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9520), 5L, 1, 749.99m, new DateTime(2025, 4, 23, 7, 33, 28, 201, DateTimeKind.Utc).AddTicks(9521), 5L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banners_BrendId",
                table: "Banners",
                column: "BrendId");

            migrationBuilder.CreateIndex(
                name: "IX_Banners_CategoryId",
                table: "Banners",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId",
                table: "CartItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CatalogId",
                table: "Categories",
                column: "CatalogId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_ProductId",
                table: "Favorites",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_UserId",
                table: "Favorites",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_PaymentId",
                table: "OrderDetails",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrendId",
                table: "Products",
                column: "BrendId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banners");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brends");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Catalogs");
        }
    }
}
