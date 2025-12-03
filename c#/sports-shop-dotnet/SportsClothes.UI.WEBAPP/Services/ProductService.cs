using SportsClothes.INTERFACES;
using SportsClothes.UI.WEBAPP.Models;
using SportsClothes.UI.WEBAPP.Models.Dto;
using SportsClothes.UI.WEBAPP.Models.ViewModels;

namespace SportsClothes.UI.WEBAPP.Services
{
    public class ProductService : IProductService
    {
        private readonly BL.BL _bl;

        public ProductService()
        {
            _bl = BL.BL.Instance;
        }

        public int CreateProduct(IProductDto newProduct)
        {
            return _bl.CreateProduct(newProduct);
        }

        public IProductDto GetProduct(int id)
        {
            
            var product = _bl.GetProduct(id);

            if (product == null)
                return null;

            return new ProductDto
            {
                Name = product.Name,
                ProducerId = product.Producer.Id,
                Type = product.Type,
                Size = product.Size,
                Color = product.Color,
                Price = product.Price
            };
        }

        public IList<ProductViewModel> GetProducts()
        {
            var products = _bl.GetProducts().ToList();

            return products.Select(v => new ProductViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Producer = v.Producer.Name,
                Type = v.Type.ToString(),
                Size = v.Size,
                Color = v.Color,
                Price = v.Price
            }).ToList();
        }

        public IList<ProductViewModel> GetFilteredProducts(IProductFilter filter)
        {
            var products = _bl.GetFilteredProducts(filter);

            return products.Select(v => new ProductViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Producer = v.Producer.Name,
                Type = v.Type.ToString(),
                Size = v.Size,
                Color = v.Color,
                Price = v.Price
            }).ToList();
        }

        public bool UpdateProduct(int id, IProductDto updatedProduct)
        {
            return _bl.UpdateProduct(id, updatedProduct);
        }

        public bool DeleteProduct(int id)
        {
            return _bl.DeleteProduct(id);
        }

        public IList<ProducerData> GetProducersData()
        {
            var producers = _bl.GetProducers().ToList();
            return producers.Select(p => new ProducerData
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();
        }

        public (bool IsSuccess, string Message) Validate(IProductDto product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
                return (false, "Product's name is required.");
            if (product.ProducerId == 0)
                return (false, "To add product you must specify its producer.");

            return (true, "Success");
        }
    }
}
