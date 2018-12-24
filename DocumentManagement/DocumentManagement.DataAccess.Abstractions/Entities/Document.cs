using System;

namespace DocumentManagement.DataAccess.Abstractions.Entities
{
	public class Document
	{
		public int DocumentId { get; set; }
		public string DocumentName { get; set; }
		public string OriginalDocumentName { get; set; }
		public double DocumentSize { get; set; }
		public string DocumentPath { get; set; }
		public int DocumentTypeId { get; set; }
		public int UploadedBy { get; set; }
		public DateTime UploadedDate { get; set; }
		public DateTime? LastAccesedDate { get; set; }
		public bool IsDeleted { get; set; }

		public virtual DocumentType DocumentType { get; set; }
	}
}
