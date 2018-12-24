using System.IO;

namespace DocumentManagement.Models
{
    public class UploadDocumentInputModel
    {
        public int UploadedBy { get; set; }
        public MemoryStream DocumentStream { get; set; }
        public string DocumentName { get; set; }
        public string DocumentContentType { get; set; }
    }
}
