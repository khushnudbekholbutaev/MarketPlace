using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TechStation.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedUpdate : Migration
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
                    { 1L, "Dell", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3778), new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3779) },
                    { 2L, "Logitech", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3780), new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3781) },
                    { 3L, "Corsair", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3781), new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3782) },
                    { 4L, "HP", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3782), new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3783) },
                    { 5L, "Samsung", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3784), new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(3784) }
                });

            migrationBuilder.InsertData(
                table: "Catalogs",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(7528), "Computers & Laptops", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(7528) },
                    { 2L, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(7530), "Gaming & Peripherals", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(7530) },
                    { 3L, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(7531), "Monitors & Displays", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(7531) }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "Id", "Amount", "CreatedAt", "PaymentMethod", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 999.99m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4082), "Credit Card", 1, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4083) },
                    { 2L, 259.98m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4085), "PayPal", 2, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4085) },
                    { 3L, 199.99m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4087), "Credit Card", 1, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4087) },
                    { 4L, 1399.99m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4088), "Credit Card", 1, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4088) },
                    { 5L, 749.99m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4089), "PayPal", 2, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(4090) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "Role", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9542), "xurshid@example.com", "Xurshid", "Oqmonov", "Password123", "+998935000000", 2, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9542), "VanTux" },
                    { 2L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9644), "abdulbosit@example.com", "Abdulbosit", "Abdullayev", "Password123", "+998901234567", 3, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9644), "abdulbosit" },
                    { 3L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9646), "dilshod@example.com", "Dilshod", "Karimov", "Password123", "+998931112233", 1, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9646), "dilshodk" },
                    { 4L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9648), "nigora@example.com", "Nigora", "Yusupova", "Password123", "+998947654321", 1, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9648), "nigora" },
                    { 5L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9650), "javohir@example.com", "Javohir", "Toirov", "Password123", "+998933334455", 1, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(9650), "javohirt" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CatalogId", "CategoryName", "CreatedAt", "Description", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, "Laptops", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9430), "Portable computers for work and play", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9431) },
                    { 2L, 1L, "Accessories", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9433), "Computer accessories including mice, keyboards, and more", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9433) },
                    { 3L, 2L, "Gaming", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9434), "Gaming equipment including chairs, desks, and peripherals", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9434) },
                    { 4L, 1L, "Monitors", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9435), "High-quality monitors for work and entertainment", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9436) },
                    { 5L, 1L, "Desktops", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9436), "Powerful desktop computers for gaming and work", new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(9437) }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "Role", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1425), 2, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1425), 1L },
                    { 2L, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1427), 3, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1427), 2L },
                    { 3L, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1428), 1, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1428), 3L },
                    { 4L, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1429), 1, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1430), 4L },
                    { 5L, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1430), 1, new DateTime(2025, 4, 23, 12, 42, 19, 129, DateTimeKind.Utc).AddTicks(1431), 5L }
                });

            migrationBuilder.InsertData(
                table: "Banners",
                columns: new[] { "Id", "BrendId", "CategoryId", "CreatedAt", "Images", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8320), "banner_computers.jpg", new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8428) },
                    { 2L, 2L, 2L, new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8504), "banner_accessories.jpg", new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8505) },
                    { 3L, 3L, 3L, new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8506), "banner_gaming.jpg", new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8507) },
                    { 4L, 4L, 1L, new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8507), "banner_laptops.jpg", new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8508) },
                    { 5L, 5L, 2L, new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8509), "banner_monitors.jpg", new DateTime(2025, 4, 23, 12, 42, 19, 126, DateTimeKind.Utc).AddTicks(8509) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BrendId", "CategoryId", "CreatedAt", "Description", "Images", "Price", "ProductName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1L, 1L, 1L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6893), "A powerful and sleek laptop for professionals.", "dell_xps_13.jpg", 999.99m, "Dell XPS 13", new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6894) },
                    { 2L, 2L, 2L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6897), "A high-performance gaming mouse.", "logitech_g_pro_x.jpg", 129.99m, "Logitech G Pro X", new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6897) },
                    { 3L, 3L, 2L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6898), "Mechanical gaming keyboard with RGB lighting.", "corsair_k95_rgb_platinum.jpg", 199.99m, "Corsair K95 RGB Platinum", new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6899) },
                    { 4L, 4L, 1L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6900), "A gaming laptop with top-tier specs.", "hp_omen_15.jpg", 1399.99m, "HP Omen 15", new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6900) },
                    { 5L, 5L, 4L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6902), "A curved gaming monitor with 240Hz refresh rate.", "samsung_odyssey_g7.jpg", 749.99m, "Samsung Odyssey G7", new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(6902) }
                });

            migrationBuilder.InsertData(
                table: "CartItems",
                columns: new[] { "Id", "CreatedAt", "ProductId", "Quantity", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6186), 1L, 1m, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6187), 1L },
                    { 2L, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6201), 2L, 2m, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6202), 2L },
                    { 3L, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6203), 3L, 1m, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6203), 3L },
                    { 4L, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6204), 4L, 1m, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6205), 4L },
                    { 5L, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6206), 5L, 1m, new DateTime(2025, 4, 23, 12, 42, 19, 127, DateTimeKind.Utc).AddTicks(6206), 5L }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "ProductId", "Quantity", "TotalAmount", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1858), 1L, 1, 999.99m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1858), 1L },
                    { 2L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1861), 2L, 2, 259.98m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1861), 2L },
                    { 3L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1862), 3L, 1, 199.99m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1863), 3L },
                    { 4L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1864), 4L, 1, 1399.99m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1864), 4L },
                    { 5L, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1865), 5L, 1, 749.99m, new DateTime(2025, 4, 23, 12, 42, 19, 128, DateTimeKind.Utc).AddTicks(1866), 5L }
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
