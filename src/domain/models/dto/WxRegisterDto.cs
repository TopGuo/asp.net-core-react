namespace domain.models.dto
{
    public class WxRegisterDto
    {
        public string Iv { get; set; }
        public string EncryptedData { get; set; }
        public int? referrer { get; set; }
        public string Code { get; set; }
    }
}