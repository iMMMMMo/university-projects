using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsClothes.CORE;
using SportsClothes.INTERFACES;

namespace SportsClothes.DAOMOCK
{
    public class DAOMock : IDao
    {
        private readonly IList<IProduct> _products;
        private readonly IList<IProducer> _producers;

        public DAOMock()
        {
            _producers = new List<IProducer>
             {
                new Producer() { Id = 1, Name = "Adidas", Description = "Leader in sportswear and innovation", CountryOfOrigin = "Germany" },
                new Producer() { Id = 2, Name = "Nike", Description = "Famous for iconic sports shoes and apparel", CountryOfOrigin = "United States" },
                new Producer() { Id = 3, Name = "New Balance", Description = "High-performance sports clothing and footwear", CountryOfOrigin = "United States" },
                new Producer() { Id = 4, Name = "Puma", Description = "Innovative sport and lifestyle products", CountryOfOrigin = "Germany" },
                new Producer() { Id = 5, Name = "Reebok", Description = "Global fitness brand", CountryOfOrigin = "United States" }
            };


            _products = new List<IProduct>
            {
                new Product
                {
                    Id = 1,
                    Name = "Adidas UltraBoost Shoes",
                    Producer = _producers[0],
                    Type = ProductType.Shoes,
                    Price = 180.00,
                    Size = "42",
                    Color = "Black"
                },
                new Product
                {
                    Id = 2,
                    Name = "Nike Air Max 90",
                    Producer = _producers[1],
                    Type = ProductType.Shoes,
                    Price = 120.00,
                    Size = "44",
                    Color = "White"
                },
                new Product
                {
                    Id = 3,
                    Name = "New Balance Classic Hoodie",
                    Producer = _producers[2],
                    Type = ProductType.Hoodie,
                    Price = 65.00,
                    Size = "L",
                    Color = "Gray"
                },
                new Product
                {
                    Id = 4,
                    Name = "Puma Tracksuit",
                    Producer = _producers[3],
                    Type = ProductType.Jacket,
                    Price = 90.00,
                    Size = "M",
                    Color = "Blue"
                },
                new Product
                {
                    Id = 5,
                    Name = "Reebok T-Shirt",
                    Producer = _producers[4],
                    Type = ProductType.TShirt,
                    Price = 25.00,
                    Size = "S",
                    Color = "Red"
                }
            };
        }

        public int CreateProduct(IProductDto product)
        {
            var newProduct = MapProductDto(product);
            _products.Add(newProduct);

            return newProduct.Id;
        }

        public int CreateProducer(IProducerDto producer)
        {
            var newProducer = MapProducerDto(producer);
            _producers.Add(newProducer);

            return newProducer.Id;
        }

        public IProduct GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            return product;
        }

        public IProducer GetProducer(int id)
        {
            var producer = _producers.FirstOrDefault(p => p.Id == id);
            return producer;
        }

        public IEnumerable<IProduct> GetAllProducts()
        {
            return _products;
        }

        public IEnumerable<IProduct> GetFilteredProducts(IProductFilter filter)
        {
            var filteredProducts = _products.Where(product =>
                (string.IsNullOrWhiteSpace(filter.SearchTerm) ||
                 product.Name.Contains(filter.SearchTerm, StringComparison.InvariantCultureIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(filter.Size) || product.Size.ToString().Equals(filter.Size, StringComparison.InvariantCultureIgnoreCase)) &&
                (filter.PriceLowerBound == 0 && filter.PriceUpperBound == 0 ||
                 IsInRange(filter.PriceLowerBound, filter.PriceUpperBound, product.Price)) &&
                (string.IsNullOrWhiteSpace(filter.Type) ||
                 product.Type.ToString().Equals(filter.Type, StringComparison.InvariantCultureIgnoreCase)) &&
                (filter.ProducerId == 0 || product.Producer.Id == filter.ProducerId)
            ).ToList();

            return filteredProducts;
        }


        public IEnumerable<IProducer> GetAllProducers()
        {
            return _producers;
        }

        public IEnumerable<IProducer> GetFilteredProducers(IProducerFilter filter)
        {
            var filteredProducers = _producers.Where(producer =>
                (string.IsNullOrWhiteSpace(filter.SearchTerm) ||
                 producer.Name.Contains(filter.SearchTerm, StringComparison.InvariantCultureIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(filter.Country) ||
                 producer.CountryOfOrigin.Contains(filter.Country, StringComparison.InvariantCultureIgnoreCase)))
                .ToList();

            return filteredProducers;
        }

        private bool IsInRange(double min, double max, double value)
        {
            return min switch
            {
                0 when max == 0 => true,
                0 when max > 0 => value <= max,
                > 0 when max == 0 => value >= min,
                > 0 when max > 0 => min <= value && value <= max,
                _ => false
            };
        }

        public bool UpdateProduct(int id, IProductDto product)
        {
            var updatedProduct = _products.FirstOrDefault(p => p.Id == id);

            if (updatedProduct == null)
                return false;

            UpdateProduct(updatedProduct, product);

            return true;
        }

        public bool UpdateProducer(int id, IProducerDto producer)
        {
            var updatedProducer = _producers.FirstOrDefault(p => p.Id == id);

            if (updatedProducer == null)
                return false;

            UpdateProducer(updatedProducer, producer);

            return true;
        }

        public bool DeleteProduct(int id)
        {
            var productToDelete = _products.FirstOrDefault(p => p.Id == id);

            if (productToDelete == null)
                return false;

            _products.Remove(productToDelete);
            return true;
        }

        public bool DeleteProducer(int id)
        {
            var producerToDelete = _producers.FirstOrDefault(p => p.Id == id);

            if (producerToDelete == null)
                return false;

            var productsFromProducer = _products.Where(p => p.Producer.Id == producerToDelete.Id).ToList();

            foreach (var product in productsFromProducer)
            {
                _products.Remove(product);
            }

            _producers.Remove(producerToDelete);
            return true;
        }

        private Product MapProductDto(IProductDto product)
        {
            return new Product
            {
                Id = GetLatestProductId(),
                Name = product.Name,
                Producer = GetProducer(product.ProducerId),
                Type = product.Type,
                Price = product.Price,
                Size = product.Size,
                Color = product.Color
            };
        }

        private int GetLatestProductId()
        {
            if (_products.Count == 0)
                return 1;

            var latestProduct = _products.MaxBy(p => p.Id);
            return latestProduct?.Id + 1 ?? 1;
        }

        private Producer MapProducerDto(IProducerDto producer)
        {
            return new Producer
            {
                Id = GetLatestProducerId(),
                Name = producer.Name,
                Description = producer.Description,
                CountryOfOrigin = producer.CountryOfOrigin,
            };
        }

        private int GetLatestProducerId()
        {
            if (_producers.Count == 0)
                return 1;

            var latestProducer = _producers.MaxBy(p => p.Id);
            return latestProducer?.Id + 1 ?? 1;
        }

        private void UpdateProduct(IProduct product, IProductDto productDto)
        {
            product.Name = productDto.Name;
            product.Producer = GetProducer(productDto.ProducerId);
            product.Type = productDto.Type;
            product.Price = productDto.Price;
            product.Size = productDto.Size;
            product.Color = productDto.Color;
        }

        private void UpdateProducer(IProducer producer, IProducerDto producerDto)
        {
            producer.Name = producerDto.Name;
            producer.Description = producerDto.Description;
            producer.CountryOfOrigin = producerDto.CountryOfOrigin;
        }
    }
}
