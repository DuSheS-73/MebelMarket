namespace MebelMarket.DAL.EntityModels
{
    public class ProductFile : Entity
    {
        public string Uid { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string ProductUid { get; set; }
    }
}
