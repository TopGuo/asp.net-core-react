namespace domain.models.dto
{
    public class MessageTypeDto : BaseModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Pic { get; set; }
        public int Types { get; set; }
        public int Order { get; set; }
    }
}