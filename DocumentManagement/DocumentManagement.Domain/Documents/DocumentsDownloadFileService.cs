using System;
using System.Threading.Tasks;
using DocumentManagement.DataAccess.Abstractions.Entities;
using DocumentManagement.Models;
using DocumentManagement.Domain.Abstractions;
using DocumentManagement.Exceptional;
using DocumentManagement.DataAccess.Abstractions;
using DocumentManagement.DataAccess.Abstractions.Repositories;

namespace DocumentManagement.Domain
{
	public class DocumentsDownloadFileService : IDocumentsDownloadFileService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IDocumentsRepository _documentsRepository;
		private readonly IDateService _dateService;
		private readonly IFileService _fileService;

		public DocumentsDownloadFileService(
			IUnitOfWork unitOfWork,
			IDocumentsRepository documentsRepository,
			IDateService dateServce,
			IFileService fileService)
		{
			_unitOfWork = unitOfWork;
			_documentsRepository = documentsRepository;
			_fileService = fileService;
			_dateService = dateServce;
		}

		public async Task<DocumentDownloadReturnModel> DownloadDocument(int documentId)
		{
			using (var transaction = await _unitOfWork.BeginTransactionAsync().ConfigureAwait(false))
			{
				try
				{
					var document = await GetDocument(documentId).ConfigureAwait(false);
					await UpdateDocumentLastAccesedDate(document).ConfigureAwait(false);

					var file = await _fileService.GetFile(document.DocumentPath, document.DocumentName).ConfigureAwait(false);

					var result = new DocumentDownloadReturnModel
					{
						DocumentContent = file.fileContent,
						DocumentContentType = document.DocumentType.ContentType,
						DocumentPath = file.filePath
					};

					transaction.Commit();

					return result;
				}
				catch (Exception exception)
				{
					transaction.Rollback();
					throw new ServiceException(ErrorCodes.DownloadDocumentException, "Something went wrong while downlading a document", exception);
				}
			}
		}

		private async Task<Document> GetDocument(int documentId)
		{
			var document = await _documentsRepository.GetDocumentAsync(documentId).ConfigureAwait(false);

			if (document == null)
			{
				throw new ServiceException(ErrorCodes.FileNotExist, "this document is not exist anymore");
			}

			return document;
		}

		private async Task UpdateDocumentLastAccesedDate(Document document)
		{
			document.LastAccesedDate = _dateService.UtcNow;
			_documentsRepository.UpdateDocumentAsync(document);

			await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);
		}
	}
}
