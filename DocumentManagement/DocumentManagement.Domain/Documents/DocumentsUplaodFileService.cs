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
	public class DocumentsUplaodFileService : IDocumentsUplaodFileService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IDocumentsRepository _documentsRepository;
		private readonly IDocumentsTypesRepository _documentsTypesRepository;
		private readonly IDateService _dateService;
		private readonly IFileService _fileService;

		public DocumentsUplaodFileService(
			IUnitOfWork unitOfWork,
			IDocumentsRepository documentsRepository,
			IDocumentsTypesRepository documentsTypesRepository,
			IDateService dateServce,
			IFileService fileService)
		{
			_unitOfWork = unitOfWork;
			_documentsRepository = documentsRepository;
			_documentsTypesRepository = documentsTypesRepository;
			_fileService = fileService;
			_dateService = dateServce;
		}

		public async Task<UploadDocumentReturnModel> UploadDocument(UploadDocumentInputModel uploadDocumentInputModel)
		{
			using (var transaction = await _unitOfWork.BeginTransactionAsync().ConfigureAwait(false))
			{
				try
				{
					DocumentType documentType = await GetDocumentType(uploadDocumentInputModel).ConfigureAwait(false);

					var uploadFileName = _fileService.UploadFile(uploadDocumentInputModel.DocumentStream, uploadDocumentInputModel.DocumentName);

					var document = new Document()
					{
						OriginalDocumentName = uploadDocumentInputModel.DocumentName,
						DocumentName = uploadFileName,
						DocumentTypeId = documentType.TypeId,
						DocumentPath = _fileService.GetUploadsPath(),
						DocumentSize = uploadDocumentInputModel.DocumentStream.Length,
						UploadedBy = uploadDocumentInputModel.UploadedBy,
						UploadedDate = _dateService.UtcNow
					};

					await _documentsRepository.AddDocumentAsync(document).ConfigureAwait(false);
					await _unitOfWork.SaveChangesAsync().ConfigureAwait(false);

					var returnModel = new UploadDocumentReturnModel
					{
						DocumentId = document.DocumentId,
						OriginalDocumentName = document.OriginalDocumentName,
					};

					transaction.Commit();

					return returnModel;
				}
				catch (Exception exception)
				{
					transaction.Rollback();
					throw new ServiceException(ErrorCodes.UploadDocumentException, "Something went wrong while uploading a document", exception);
				}
			}
		}

		private async Task<DocumentType> GetDocumentType(UploadDocumentInputModel uploadDocumentInputModel)
		{
			var documentType = await _documentsTypesRepository.GetDocumentTypeAsync(uploadDocumentInputModel.DocumentContentType).ConfigureAwait(false);

			if (documentType == null)
			{
				throw new ServiceException(ErrorCodes.FileNotSuported, "this document type is not supported");
			}

			return documentType;
		}
	}
}
