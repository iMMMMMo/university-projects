using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsClothes.CORE;
using SportsClothes.INTERFACES;

namespace SportsClothes.DAOSQL.BO
{
    public class Producer : IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CountryOfOrigin { get; set; }
    }
}
