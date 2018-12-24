using System.Collections.Generic;

namespace DocumentManagement.DataAccess.Abstractions.Entities
{
	public class DocumentType
	{
		public int TypeId { get; set; }
		public string Extention { get; set; }
		public string ContentType { get; set; }
		public bool IsDeleted { get; set; }

		public virtual ICollection<Document> Documents { get; set; }
	}
}
