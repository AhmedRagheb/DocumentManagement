using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Models;
using DocumentManagement.Domain.Abstractions;
using DocumentManagement.Domain.Helpers;
using DocumentManagement.DataAccess.Abstractions.Repositories;
using DocumentManagement.DataAccess.Abstractions;

namespace DocumentManagement.Domain
{
	public class DocumentsService : IDocumentsService
	{
		private readonly IDocumentsRepository _documentsRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DocumentsService(IUnitOfWork unitOfWork, IDocumentsRepository documentsRepository)
		{
			_unitOfWork = unitOfWork;
			_documentsRepository = documentsRepository;
		}

		public async Task DeleteDocument(int documentId)
		{
			await _documentsRepository.DeleteDocumentAsync(documentId).ConfigureAwait(false);
			await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
		}

		public async Task<IReadOnlyList<DocumentModel>> GetDocuments()
		{
			var documents = await _documentsRepository.GetDocumentsAsync().ConfigureAwait(false);

			var documentsModel = documents.Select(document => document.MapToDocumentModel());
			var orderedDocumentsModel = documentsModel.OrderByDescending(document => document.LastAccesedDate).ToList();

			return orderedDocumentsModel.AsReadOnly();
		}
	}
}
