using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.DataAccess.DBContext;
using DocumentManagement.DataAccess.Abstractions.Entities;
using DocumentManagement.DataAccess.Abstractions.Repositories;

namespace DocumentManagement.DataAccess.Repositories
{
	public class DocumentsRepository : IDocumentsRepository
	{
		private readonly IDocumentDbContext _dbContext;

		public DocumentsRepository(IDocumentDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<Document>> GetDocumentsAsync()
		{
			var documents = await _dbContext.Documents
				.Include(document => document.DocumentType)
				.Where(document => !document.IsDeleted)
				.ToListAsync().ConfigureAwait(false);

			return documents;
		}

		public async Task DeleteDocumentAsync(int documentId)
		{
			var document = await GetDocumentAsync(documentId).ConfigureAwait(false);
			document.IsDeleted = true;
		}

		public async Task AddDocumentAsync(Document document)
		{
			 await _dbContext.Documents.AddAsync(document).ConfigureAwait(false);
		}

		public async Task<Document> GetDocumentAsync(int documentId)
		{
			var document = await _dbContext.Documents
									.Include(d => d.DocumentType)
									.SingleAsync(d => d.DocumentId == documentId)
									.ConfigureAwait(false);

			return document;
		}

		public void UpdateDocumentAsync(Document document)
		{
			_dbContext.Documents.Attach(document);
		}
	}
}
