using DocumentManagement.Models;
using System.Threading.Tasks;

namespace DocumentManagement.Domain.Abstractions
{
	public interface IDocumentsDownloadFileService
	{
		Task<DocumentDownloadReturnModel> DownloadDocument(int documentId);
	}
}
