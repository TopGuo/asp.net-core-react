using System;
using System.Collections.Generic;

namespace domain.models.dto
{
    public class ShopDto : BaseModel
    {
        public int? Id { get; set; }
        public int UserId { get; set; }
        public int Status { get; set; }
        public int Order { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LogoPic { get; set; }
        public int LookCount { get; set; }
        public int Types { get; set; }
        public int ShopType { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string PhoneNum { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Pic { get; set; }
        public string NickName { get; set; }
    }

    public class ShopDto2
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string LogoPic { get; set; }
        public int LookCount { get; set; }
        public int Types { get; set; }
        public int ShopType { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        public string PhoneNum { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Pic { get; set; }
        public string NickName { get; set; }
        public decimal Distance { get; set; }
    }
}