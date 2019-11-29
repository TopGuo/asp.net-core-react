namespace domain.models
{
    public class UserModel:BaseModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string PhoneNum { get; set; }
    }
}