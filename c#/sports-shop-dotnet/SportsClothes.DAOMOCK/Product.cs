using SportsClothes.CORE;
using SportsClothes.INTERFACES;

namespace SportsClothes.DAOMOCK
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IProducer Producer { get; set; }
        public ProductType Type { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
    }
}
