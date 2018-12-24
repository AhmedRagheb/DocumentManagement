using System;

namespace DocumentManagement.Models
{
    public class DocumentModel
    {
        public int DocumentId { get; set; }
        public string OriginalDocumentName { get; set; }
        public double DocumentSize { get; set; }
        public DateTime UploadedDate { get; set; }
        public DateTime? LastAccesedDate { get; set; }
    }
}
