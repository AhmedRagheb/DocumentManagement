using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentManagement.Models;

namespace DocumentManagement.Domain.Abstractions
{
	public interface IDocumentsTypesService
	{
		Task<IReadOnlyList<DocumentTypeModel>> GetDocumentsTypes();
	}
}
