namespace SportsClothes.INTERFACES
{
    public interface IProducer
    {
        int Id { get; set; }
        string Name { get; set; }
        string Description { get; set; }
        string CountryOfOrigin { get; set; }
    }
}
