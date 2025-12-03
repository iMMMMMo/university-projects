using SportsClothes.CORE;
using SportsClothes.INTERFACES;

namespace SportsClothes.UI.WEBAPP.Models.Dto
{
    public class ProductDto : IProductDto
    {
        public ProductDto()
        {
            Name = string.Empty;
            Size = string.Empty;
            Color = string.Empty;
        }

        public ProductDto(string name, int producerId, ProductType productType, double price, string size, string color)
        {
            Name = name;
            ProducerId = producerId;
            Type = productType;
            Price = price;
            Size = size;
            Color = color;
        }

        public string Name { get; set; }
        public int ProducerId { get; set; }
        public ProductType Type { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
    }
}
    