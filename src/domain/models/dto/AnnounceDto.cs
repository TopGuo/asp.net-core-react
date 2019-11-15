using domain.models;

namespace domain.models.dto
{
    public class AnnounceDto : BaseModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Type { get; set; }
    }
}