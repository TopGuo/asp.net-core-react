using System;

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

    public class ScenicDto2
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
        public DateTime CreateTime { get; set; }
        public User2 User { get; set; } = new User2();
    }
    public class User2
    {
        public string Name { get; set; } = "星辰无限";
        public string Avatar { get; set; } = "images/1000-1.png";
    }
}