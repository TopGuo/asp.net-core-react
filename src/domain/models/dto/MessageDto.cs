namespace domain.models.dto
{
    public class MessageDto : BaseModel
    {
        public int? Id { get; set; }
        public int Types { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Pics { get; set; }
        public int Order { get; set; }
        public int LookCount { get; set; }
    }
}