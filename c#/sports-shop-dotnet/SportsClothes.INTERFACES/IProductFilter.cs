namespace SportsClothes.INTERFACES
{
    public interface IProductFilter
    {
        string SearchTerm { get; set; }
        string Size { get; set; }
        double PriceLowerBound { get; set; }
        double PriceUpperBound { get; set; }
        string Type { get; set; }
        int ProducerId { get; set; }
    }
}
