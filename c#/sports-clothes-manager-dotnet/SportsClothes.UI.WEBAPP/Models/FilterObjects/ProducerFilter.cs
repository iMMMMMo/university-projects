using SportsClothes.INTERFACES;

namespace SportsClothes.UI.WEBAPP.Models.FilterObjects
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
