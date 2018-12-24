using DocumentManagement.DataAccess.Repositories;
using System.Collections.Generic;
using DocumentManagement.DataAccess.Abstractions.Entities;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using System;
using Moq;
using System.Threading;

namespace DocumentManagement.DataAccess.Tests
{
	public class DocumentsRepositoryTests : InMemoryDbContextTest
	{
		private readonly DocumentsRepository _documentsRepository;

		public DocumentsRepositoryTests()
		{
			SeedDatabase();
			_documentsRepository = new DocumentsRepository(DbContext);
		}

		[Fact]
		public async Task GetAll_ReturnsExpected()
		{
			//Arrange 
			var expected = new List<DocumentType>
			{
				new DocumentType
				{
					ContentType = "image/jpeg",
					Documents = null,
					Extention = ".jpg",
					IsDeleted = false,
					TypeId = 1
				},
				new DocumentType
				{
					ContentType = "image/gif",
					Documents = null,
					Extention = ".gif",
					IsDeleted = false,
					TypeId = 2,
				}
			};

			//act
			var documents = await _documentsRepository.GetDocumentsAsync();

			//assert
			documents.Should().HaveSameCount(expected);
			documents.Should().Contain(c => c.DocumentId == 10 && c.DocumentName == "Test.jpg");
			documents.Should().Contain(c => c.DocumentId == 11 && c.DocumentName == "Test1.jpg");
			documents.Should().NotContain(c => c.DocumentId == 12 && c.DocumentName == "Test2.jpg");
		}

		[Fact]
		public async Task GetDocument_ReturnsExpected()
		{
			//Arrange 
			var expected = new DocumentType
			{
				ContentType = "image/gif",
				Documents = null,
				Extention = ".gif",
				IsDeleted = false,
				TypeId = 2,
			};

			//act
			var document= await _documentsRepository.GetDocumentAsync(10);

			//assert
			document.Should().NotBeNull();
			document.DocumentName.Should().Equals("Test.jpg");
		}

		[Fact]
		public async Task AddDocumentAsync_ReturnsExpected()
		{
			//Arrange 
			var document = new Document
			{
				DocumentId = 20,
				DocumentName = "Test1.jpg",
				DocumentSize = 105520,
				DocumentPath = "uploads/documents/",
				DocumentTypeId = 1,
				UploadedBy = 1,
				UploadedDate = DateTime.UtcNow,
				LastAccesedDate = DateTime.UtcNow,
				IsDeleted = false
			};

			//act
			await _documentsRepository.AddDocumentAsync(document);

			//assert
			DbContextMock.Verify(x => x.AddAsync(document, default(CancellationToken)), Times.Once);
		}

		[Fact]
		public async Task DeleteDocumentAsync_ReturnsExpected()
		{
			//Arrange 
			var document = await DbContext.Documents.FindAsync(10);

			//act
			await _documentsRepository.DeleteDocumentAsync(10);

			//assert
			document.IsDeleted.Should().BeTrue();
		}

		private void SeedDatabase()
		{
			DbContext.DocumentsTypes.AddRange(new List<DocumentType>
			{
				new DocumentType
				{
					TypeId = 1,
					ContentType = "image/jpeg",
					Extention = ".jpg",
					IsDeleted = false
				}
			});

			DbContext.Documents.AddRange(new List<Document>
			{
				new Document
				{
					DocumentId = 10,
					DocumentName = "Test.jpg",
					DocumentSize = 105520,
					DocumentPath = "uploads/documents/",
					DocumentTypeId = 1,
					UploadedBy= 1,
					UploadedDate = DateTime.UtcNow,  
					LastAccesedDate = DateTime.UtcNow,
					IsDeleted = false
				},
				new Document
				{
					DocumentId = 11,
					DocumentName = "Test1.jpg",
					DocumentSize = 105520,
					DocumentPath = "uploads/documents/",
					DocumentTypeId = 1,
					UploadedBy= 1,
					UploadedDate = DateTime.UtcNow,
					LastAccesedDate = DateTime.UtcNow,
					IsDeleted = false
				},
				new Document
				{
					DocumentId = 12,
					DocumentName = "Test2.jpg",
					DocumentSize = 105520,
					DocumentPath = "uploads/documents/",
					DocumentTypeId = 1,
					UploadedBy= 1,
					UploadedDate = DateTime.UtcNow,
					LastAccesedDate = DateTime.UtcNow,
					IsDeleted = true
				},
			});

			DbContext.SaveChanges();
		}
	}
}
