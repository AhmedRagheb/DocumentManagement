using DocumentManagement.DataAccess.Repositories;
using System.Collections.Generic;
using DocumentManagement.DataAccess.Abstractions.Entities;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace DocumentManagement.DataAccess.Tests
{
	public class DocumentsTypesRepositoryTests : InMemoryDbContextTest
	{
		private readonly DocumentsTypesRepository documentsTypesRepository;

		public DocumentsTypesRepositoryTests()
		{
			SeedDatabase();
			documentsTypesRepository = new DocumentsTypesRepository(DbContext);
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
			var documentsTypes = await documentsTypesRepository.GetDocumentsTypesAsync();

			//assert
			documentsTypes.Should().HaveSameCount(expected);
			documentsTypes.Should().BeEquivalentTo(expected);
		}

		[Fact]
		public async Task GetDocumentType_ReturnsExpected()
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
			var documentsType = await documentsTypesRepository.GetDocumentTypeAsync("image/gif");

			//assert
			documentsType.Should().BeEquivalentTo(expected);
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
				},
				new DocumentType
				{
					TypeId = 2,
					ContentType = "image/gif",
					Extention = ".gif",
					IsDeleted = false
				},
				new DocumentType
				{
					TypeId = 3,
					ContentType = "text/plain",
					Extention = ".txt",
					IsDeleted = true
				},
			});

			DbContext.SaveChanges();
		}
	}
}
