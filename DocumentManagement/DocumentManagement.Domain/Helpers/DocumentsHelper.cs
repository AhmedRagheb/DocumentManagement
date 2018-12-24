using DocumentManagement.DataAccess.Abstractions.Entities;
using DocumentManagement.Models;

namespace DocumentManagement.Domain.Helpers
{
	internal static class DocumentsHelper
	{
		internal static DocumentModel MapToDocumentModel(this Document document)
		{
			return new DocumentModel
			{
				DocumentId = document.DocumentId,
				OriginalDocumentName = document.OriginalDocumentName,
				DocumentSize = document.DocumentSize,
				UploadedDate = document.UploadedDate,
				LastAccesedDate = document.LastAccesedDate
			};
		}

		internal static DocumentTypeModel MapToDocumentTypeModel(this DocumentType documentType)
		{
			return new DocumentTypeModel
			{
				TypeId = documentType.TypeId,
				ContentType = documentType.ContentType,
				Extention = documentType.Extention
			};
		}
	}
}
