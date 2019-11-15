using System;

namespace domain.models.dto
{
    public class BackstageCookie
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public Guid Token { get; set; }
        public string UserName { get; set; }
        public int RoleId { get; set; }
        public DateTime LastLogin { get; set; }
    }
}