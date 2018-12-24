using DocumentManagement.DataAccess.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentManagement.DataAccess.Mappings
{
	public static class DocumentMapping
	{
		public static void Map(EntityTypeBuilder<Document> entityBuilder)
		{
			entityBuilder.ToTable("Documents");
			entityBuilder.HasKey(t => t.DocumentId);
			entityBuilder.Property(t => t.DocumentName).HasMaxLength(150).IsRequired();
			entityBuilder.Property(t => t.OriginalDocumentName).HasMaxLength(150).IsRequired();
			entityBuilder.Property(t => t.DocumentSize).IsRequired();
			entityBuilder.Property(t => t.DocumentPath).HasMaxLength(500).IsRequired();
			entityBuilder.Property(t => t.DocumentTypeId).IsRequired();
			entityBuilder.Property(t => t.UploadedBy).IsRequired();
			entityBuilder.Property(t => t.UploadedDate).IsRequired();
			entityBuilder.Property(t => t.LastAccesedDate);
			entityBuilder.Property(t => t.IsDeleted).IsRequired();

			entityBuilder.HasOne(t => t.DocumentType)
						.WithMany(t => t.Documents)
						.HasForeignKey(t => t.DocumentTypeId);
		}
	}
}
