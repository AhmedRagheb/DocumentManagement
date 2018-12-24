using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DocumentManagement.DataAccess.Abstractions.Entities;

namespace DocumentManagement.DataAccess.DBContext
{
	public interface IDocumentDbContext
    {
		DbSet<Document> Documents { get; set; }
		DbSet<DocumentType> DocumentsTypes { get; set; }

		Task<int> SaveChangesAsync();
		Task<IDbContextTransaction> BeginTransactionAsync();
	}
}
