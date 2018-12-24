using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DocumentManagement.DataAccess.Abstractions.Entities;
using DocumentManagement.DataAccess.Mappings;

namespace DocumentManagement.DataAccess.DBContext
{
	public class DocumentDbContext : DbContext, IDocumentDbContext
    {
		public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
		{
		}

		public DbSet<Document> Documents { get; set; }
		public DbSet<DocumentType> DocumentsTypes { get; set; }

		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await Database.BeginTransactionAsync().ConfigureAwait(false);
		}

		public virtual async Task<int> SaveChangesAsync()
		{
			var result = await base.SaveChangesAsync().ConfigureAwait(false);
			return result;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			DocumentMapping.Map(modelBuilder.Entity<Document>());
			DocumentTypeMapping.Map(modelBuilder.Entity<DocumentType>());
		}
	}
}
