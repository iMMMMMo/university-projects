using System.Reflection;
using SportsClothes.INTERFACES;
using Microsoft.Extensions.Configuration;

namespace SportsClothes.BL
{
    public class BL
    {
        private static BL instance;
        private static readonly object lockObject = new object();

        private IDao Dao;

        private BL(string libraryName)
        {
            var dllPath = Path.Combine(AppContext.BaseDirectory, libraryName);

            if (!File.Exists(dllPath))
                throw new FileNotFoundException($"DLL not found: {dllPath}");

            var assembly = Assembly.LoadFrom(dllPath);
            var typeToCreate = assembly.GetTypes().FirstOrDefault(type => type.IsAssignableTo(typeof(IDao)));

            try
            {
                Dao = (IDao)Activator.CreateInstance(typeToCreate, null);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create instance of IDao: {typeToCreate}\n{ex.Message}");
            }
        }

        public static BL Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();
                            var libraryName = configuration["AppSettings:DaoLibraryName"];

                            instance = new BL(libraryName);
                        }
                    }
                }

                return instance;
            }
        }

        public int CreateProduct(IProductDto product) => Dao.CreateProduct(product);

        public int CreateProducer(IProducerDto producer) => Dao.CreateProducer(producer);

        public IProduct GetProduct(int id) => Dao.GetProduct(id);
        public IProducer GetProducer(int id) => Dao.GetProducer(id);
        public IEnumerable<IProduct> GetProducts() => Dao.GetAllProducts();
        public IEnumerable<IProduct> GetFilteredProducts(IProductFilter filter) => Dao.GetFilteredProducts(filter);
        public IEnumerable<IProducer> GetProducers() => Dao.GetAllProducers();
        public IEnumerable<IProducer> GetFilteredProducers(IProducerFilter filter) => Dao.GetFilteredProducers(filter);

        public bool UpdateProduct(int id, IProductDto product) => Dao.UpdateProduct(id, product);
        public bool UpdateProducer(int id, IProducerDto producer) => Dao.UpdateProducer(id, producer);

        public bool DeleteProduct(int id) => Dao.DeleteProduct(id);
        public bool DeleteProducer(int id) => Dao.DeleteProducer(id);
    }
}
