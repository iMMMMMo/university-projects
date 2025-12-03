using SportsClothes.CORE;

namespace SportsClothes.INTERFACES
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        ProductType Type { get; set; }
        double Price { get; set; }
        string Size { get; set; }
        string Color { get; set; }
    }
}
