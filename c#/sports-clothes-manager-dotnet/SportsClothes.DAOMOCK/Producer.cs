using SportsClothes.CORE;
using SportsClothes.INTERFACES;

namespace SportsClothes.DAOMOCK
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CountryOfOrigin { get; set; }
    }
}
