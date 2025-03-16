using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public class FileRecord : Entity
    {
        public string FileName { get; set; }
        public string OriginalName { get; set; }
        public string FilePath { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
    }
}
