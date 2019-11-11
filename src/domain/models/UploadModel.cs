namespace domain.models
{
    public class UploadModel
    {
        public string FileName { get; set; }
        public long Length { get; set; }
        public string FullName { get; set; }
        public string Extension { get; set; }
        public string VirtualPath { get; set; }
        public string FullVirtualPath { get; set; }

    }
}