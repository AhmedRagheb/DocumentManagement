using DocumentManagement.DataAccess.DBContext;
using DocumentManagement.DataAccess.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.DataAccess.Abstractions.Repositories;

namespace DocumentManagement.DataAccess.Repositories
{
	public class DocumentsTypesRepository : IDocumentsTypesRepository
	{
		private readonly IDocumentDbContext _dbContext;

		public DocumentsTypesRepository(IDocumentDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<DocumentType>> GetDocumentsTypesAsync()
		{
			var documentsTypes = await _dbContext.DocumentsTypes.Where(type => !type.IsDeleted).ToListAsync().ConfigureAwait(false);

			return documentsTypes;
		}

		public async Task<DocumentType> GetDocumentTypeAsync(string contentType)
		{
			var documentType = await _dbContext.DocumentsTypes.FirstOrDefaultAsync(type =>
				type.ContentType == contentType
				&& !type.IsDeleted).ConfigureAwait(false);

			return documentType;
		}
	}
}
