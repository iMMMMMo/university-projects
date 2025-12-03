using SportsClothes.CORE;
using SportsClothes.INTERFACES;
using SportsClothes.UI.WEBAPP.Models.Dto;
using SportsClothes.UI.WEBAPP.Models.ViewModels;

namespace SportsClothes.UI.WEBAPP.Services
{
    public class ProducerService : IProducerService
    {
        private readonly BL.BL _bl;

        public ProducerService()
        {
            _bl = BL.BL.Instance;
        }

        public int CreateProducer(IProducerDto newProducer)
        {
            return _bl.CreateProducer(newProducer);
        }

        public ProducerDto GetProducer(int id)
        {
            var producer = _bl.GetProducer(id);

            if (producer == null)
                return null;

            return new ProducerDto
            {
                Name = producer.Name,
                Description = producer.Description,
                CountryOfOrigin = producer.CountryOfOrigin,
            };
        }

        public IList<ProducerViewModel> GetProducers()
        {
            var producers = _bl.GetProducers().ToList();

            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CountryOfOrigin = p.CountryOfOrigin,
            }).ToList();
        }

        public IList<ProducerViewModel> GetFilteredProducers(IProducerFilter filter)
        {
            var producers = _bl.GetFilteredProducers(filter).ToList();

            return producers.Select(p => new ProducerViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CountryOfOrigin = p.CountryOfOrigin,
            }).ToList();
        }

        public bool UpdateProducer(int id, IProducerDto updatedProducer)
        {
            return _bl.UpdateProducer(id, updatedProducer);
        }

        public bool DeleteProducer(int id)
        {
            return _bl.DeleteProducer(id);
        }

        public (bool IsSuccess, string Message) Validate(IProducerDto producer)
        {
            if (string.IsNullOrWhiteSpace(producer.Name))
                return (false, "Producer's name is required.");
            if (string.IsNullOrWhiteSpace(producer.Description))
                return (false, "Producer's description is required.");
            if (producer.Description.Length > 300)
                return (false, "Description exceeds the maximum length of 300 characters.");
            if (string.IsNullOrEmpty(producer.CountryOfOrigin))
                return (false, "Producer's country of origin must be specified.");

            return (true, "Success");
        }
    }
}
