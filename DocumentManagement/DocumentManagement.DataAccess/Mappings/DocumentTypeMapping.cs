using DocumentManagement.DataAccess.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DocumentManagement.DataAccess.Mappings
{
	public static class DocumentTypeMapping
	{
		public static void Map(EntityTypeBuilder<DocumentType> entityBuilder)
		{
			entityBuilder.ToTable("DocumentsTypes");
			entityBuilder.HasKey(t => t.TypeId);
			entityBuilder.Property(t => t.Extention).HasMaxLength(10).IsRequired();
			entityBuilder.Property(t => t.ContentType).HasMaxLength(100).IsRequired();
			entityBuilder.Property(t => t.IsDeleted).IsRequired();
		}
	}
}
