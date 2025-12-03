using SportsClothes.INTERFACES;
using SportsClothes.UI.WEBAPP.Models.Dto;
using SportsClothes.UI.WEBAPP.Models.ViewModels;

namespace SportsClothes.UI.WEBAPP.Services
{
    public interface IProducerService
    {
        int CreateProducer(IProducerDto newProducer);
        ProducerDto GetProducer(int id);
        IList<ProducerViewModel> GetProducers();
        IList<ProducerViewModel> GetFilteredProducers(IProducerFilter filter);
        bool UpdateProducer(int id, IProducerDto updatedProducer);
        bool DeleteProducer(int id);
        (bool IsSuccess, string Message) Validate(IProducerDto producer);
    }
}
