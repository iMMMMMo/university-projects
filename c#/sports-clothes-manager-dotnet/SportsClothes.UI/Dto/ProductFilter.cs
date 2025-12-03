using SportsClothes.CORE;
using SportsClothes.INTERFACES;

namespace SportsClothes.UI.Dto
{
    public class ProductFilter : IProductFilter
    {
        public string SearchTerm { get; set; } = string.Empty;
        public string Size { get; set; }
        public double PriceLowerBound { get; set; }
        public double PriceUpperBound { get; set; }
        public string Type { get; set; } = string.Empty;
        public int ProducerId { get; set; }
    }
}
