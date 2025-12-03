using SportsClothes.CORE;
using SportsClothes.INTERFACES;

namespace SportsClothes.UI.WEBAPP.Models.Dto
{
    public class ProducerDto : IProducerDto
    {
        public ProducerDto()
        {
            Name = string.Empty;
            Description = string.Empty;
            CountryOfOrigin = string.Empty;
        }

        public ProducerDto(string name, string description, string countryOfOrigin)
        {
            Name = name;
            Description = description;
            CountryOfOrigin = countryOfOrigin;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string CountryOfOrigin { get; set; }
    }
}
