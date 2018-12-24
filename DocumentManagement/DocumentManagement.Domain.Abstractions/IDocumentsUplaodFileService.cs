using DocumentManagement.Models;
using System.Threading.Tasks;

namespace DocumentManagement.Domain.Abstractions
{
	public interface IDocumentsUplaodFileService
	{
		Task<UploadDocumentReturnModel> UploadDocument(UploadDocumentInputModel uploadDocumentInputModel);
	}
}
