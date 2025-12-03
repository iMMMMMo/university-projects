using SportsClothes.INTERFACES;
using SportsClothes.UI.WEBAPP.Models;
using SportsClothes.UI.WEBAPP.Models.ViewModels;

namespace SportsClothes.UI.WEBAPP.Services
{
    public interface IProductService
    {
        int CreateProduct(IProductDto newProduct);
        IProductDto GetProduct(int id);
        IList<ProductViewModel> GetProducts();
        IList<ProductViewModel> GetFilteredProducts(IProductFilter filter);
        bool UpdateProduct(int id, IProductDto updatedProduct);
        bool DeleteProduct(int id);
        IList<ProducerData> GetProducersData();
        (bool IsSuccess, string Message) Validate(IProductDto product);
    }
}
