using SportsClothes.DAOSQL.BO;
using SportsClothes.INTERFACES;
using Microsoft.EntityFrameworkCore;

namespace SportsClothes.DAOSQL
{
    public class Dao : IDao
    {
        private readonly SportsClothesDbContext _context;

        public Dao()
        {
            _context = new SportsClothesDbContext();
        }

        public int CreateProduct(IProductDto product)
        {
            var newProduct = MapProductDto(product);

            _context.Products.Add(newProduct);
            _context.SaveChanges();

            return newProduct.Id;
        }

        public int CreateProducer(IProducerDto producer)
        {
            var newProducer = MapProducerDto(producer);

            _context.Producers.Add(newProducer);
            _context.SaveChanges();

            return newProducer.Id;
        }

        public IProduct GetProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);

            return product;
        }

        public IProducer GetProducer(int id)
        {
            var producer = _context.Producers.FirstOrDefault(p => p.Id == id);

            return producer;
        }

        public IEnumerable<IProduct> GetAllProducts()
        {
            var products = new List<IProduct>();

            products.AddRange(_context.Products
                .Include(p => p.ProducerImpl)
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Producer = p.ProducerImpl,
                    Type = p.Type,
                    Price = p.Price,
                    Size = p.Size,
                    Color = p.Color
                }).ToList());

            return products;
        }

        public IEnumerable<IProduct> GetFilteredProducts(IProductFilter filter)
        {
            var products = GetAllProducts().ToList();

            var filteredProducts = products.Where(product =>
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

        public IEnumerable<IProducer> GetAllProducers()
        {
            var producers = new List<IProducer>();
            producers.AddRange(_context.Producers.ToList());
            return producers;
        }

        public IEnumerable<IProducer> GetFilteredProducers(IProducerFilter filter)
        {
            var producers = GetAllProducers().ToList();

            var filteredProducers = producers.Where(producer =>
                (string.IsNullOrWhiteSpace(filter.SearchTerm) ||
                 producer.Name.Contains(filter.SearchTerm, StringComparison.InvariantCultureIgnoreCase)) &&
                (string.IsNullOrWhiteSpace(filter.Country) ||
                 producer.CountryOfOrigin.Contains(filter.Country, StringComparison.InvariantCultureIgnoreCase))).ToList();

            return filteredProducers;
        }

        public bool UpdateProduct(int id, IProductDto product)
        {
            var updatedProduct = _context.Products.FirstOrDefault(p => p.Id == id);

            if (updatedProduct == null)
                return false;

            UpdateProduct(updatedProduct, product);

            _context.SaveChanges();
            return true;
        }

        public bool UpdateProducer(int id, IProducerDto producer)
        {
            var updatedProducer = _context.Producers.FirstOrDefault(p => p.Id == id);

            if (updatedProducer == null)
                return false;

            UpdateProducer(updatedProducer, producer);

            _context.SaveChanges();
            return true;
        }

        public bool DeleteProduct(int id)
        {
            var productToDelete = _context.Products.FirstOrDefault(p => p.Id == id);

            if (productToDelete == null)
                return false;

            _context.Products.Remove(productToDelete);
            _context.SaveChanges();

            return true;
        }

        public bool DeleteProducer(int id)
        {
            var producerToDelete = _context.Producers.FirstOrDefault(p => p.Id == id);

            if (producerToDelete == null)
                return false;

            var productsFromProducer = _context.Products
                .Include(p => p.ProducerImpl)
                .Where(p => p.ProducerImpl.Id == producerToDelete.Id).ToList();

            if (productsFromProducer.Any())
                _context.Products.RemoveRange(productsFromProducer);

            _context.Producers.Remove(producerToDelete);
            _context.SaveChanges();

            return true;
        }

        private Product MapProductDto(IProductDto product)
        {
            return new Product
            {
                Name = product.Name,
                Producer = GetProducer(product.ProducerId),
                Type = product.Type,
                Price = product.Price,
                Size = product.Size,
                Color = product.Color
            };
        }

        private Producer MapProducerDto(IProducerDto producer)
        {
            return new Producer
            {
                Name = producer.Name,
                Description = producer.Description,
                CountryOfOrigin = producer.CountryOfOrigin
            };
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
