namespace domain.models
{
    public class BaseModel
    {
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}