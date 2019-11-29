using System;
using System.Collections.Generic;
using System.Text;

namespace domain.models
{
   public class ShopModel: BaseModel
    {
        public string NickName { get; set; }
        public string Title { get; set; }
        public string PhoneNum { get; set; }
        public int Status { get; set; } = -1;
        public int Id { get; set; }
        public int UserId { get; set; } 
        public string LogoPic { get; set; }
        public DateTime CreateTime { get; set; }
 

    }
}
