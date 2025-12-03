using SportsClothes.CORE;

namespace SportsClothes.INTERFACES
{
    public interface IProductDto
    {
        string Name { get; set; }
        int ProducerId { get; set; }
        ProductType Type { get; set; }
        double Price { get; set; }
        string Size { get; set; }
        string Color { get; set; }
    }
}
