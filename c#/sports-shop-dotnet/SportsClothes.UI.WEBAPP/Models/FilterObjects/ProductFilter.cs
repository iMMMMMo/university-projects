using SportsClothes.CORE;
using SportsClothes.INTERFACES;
using System.ComponentModel.DataAnnotations;

namespace SportsClothes.UI.WEBAPP.Models.FilterObjects
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
