using DocumentManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocumentManagement.Domain.Abstractions
{
	public interface IDocumentsService
	{
		Task<IReadOnlyList<DocumentModel>> GetDocuments();

		Task DeleteDocument(int documentId);
	}
}
