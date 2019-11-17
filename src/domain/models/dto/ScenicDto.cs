namespace domain.models.dto
{
    public class ScenicDto : BaseModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string LTitle { get; set; }
        public string Content { get; set; }
        public int Order { get; set; }
        public string Mark1 { get; set; }
        public string Mark2 { get; set; }
        public string Pic { get; set; }
        public int? LookCount { get; set; }
    }
}