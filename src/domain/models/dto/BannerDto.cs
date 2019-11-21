namespace domain.models.dto
{
    public class BannerDto : BaseModel
    {
        public int? Id { get; set; }
        public int? Types { get; set; }
        public string Pic { get; set; }
        public string JumpUrl { get; set; }
    }
}