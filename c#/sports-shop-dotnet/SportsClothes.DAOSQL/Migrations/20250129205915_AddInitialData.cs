using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsClothes.DAOSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Dodanie danych do tabeli Producers
            migrationBuilder.Sql("INSERT INTO Producers (Name, CountryOfOrigin, Description) VALUES ('Nike', 'USA', 'Leading sportswear brand')");
            migrationBuilder.Sql("INSERT INTO Producers (Name, CountryOfOrigin, Description) VALUES ('Adidas', 'Germany', 'Known for sports apparel and shoes')");

            // Dodanie danych do tabeli Products
            migrationBuilder.Sql("INSERT INTO Products (Name, Color, Price, Size, Type, ProducerId) VALUES ('T-shirt', 'Red', 19.99, 'M', 1, 1)");
            migrationBuilder.Sql("INSERT INTO Products (Name, Color, Price, Size, Type, ProducerId) VALUES ('Sweatshirt', 'Blue', 39.99, 'L', 2, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Usuwanie danych w przypadku wycofania migracji
            migrationBuilder.Sql("DELETE FROM Products WHERE Name IN ('T-shirt', 'Sweatshirt')");
            migrationBuilder.Sql("DELETE FROM Producers WHERE Name IN ('Nike', 'Adidas')");
        }
    }
}
