using SportsClothes.CORE;

namespace SportsClothes.INTERFACES
{
    public interface IDao
    {
        int CreateProduct(IProductDto product);
        int CreateProducer(IProducerDto producer);

        IProduct GetProduct(int id);
        IProducer GetProducer(int id);
        IEnumerable<IProduct> GetAllProducts();
        IEnumerable<IProduct> GetFilteredProducts(IProductFilter filter);
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<IProducer> GetFilteredProducers(IProducerFilter filter);

        bool UpdateProduct(int id, IProductDto product);
        bool UpdateProducer(int id, IProducerDto producer);

        bool DeleteProduct(int id);
        bool DeleteProducer(int id);
    }
}
