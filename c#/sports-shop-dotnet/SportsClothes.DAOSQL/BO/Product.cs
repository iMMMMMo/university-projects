using SportsClothes.CORE;
using SportsClothes.INTERFACES;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsClothes.DAOSQL.BO
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductType Type { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }

        public int ProducerId { get; set; }

        [NotMapped]
        public IProducer Producer
        {
            get => ProducerImpl;
            set
            {
                ProducerImpl = (Producer)value;
                ProducerId = ProducerImpl.Id;
            }
        }

        public Producer ProducerImpl { get; set; }
    }
}
