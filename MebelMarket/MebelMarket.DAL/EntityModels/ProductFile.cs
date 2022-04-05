namespace MebelMarket.DAL.EntityModels
{
    public class ProductFile : Entity
    {
        public string Id { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string ProductId { get; set; }
    }
}
