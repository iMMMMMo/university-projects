using SportsClothes.INTERFACES;

namespace SportsClothes.UI.Dto
{
    public class ProducerFilter : IProducerFilter
    {
        public ProducerFilter()
        {
            SearchTerm = string.Empty;
            Country = string.Empty;
        }

        public string SearchTerm { get; set; }
        public string Country { get; set; }
    }
}
