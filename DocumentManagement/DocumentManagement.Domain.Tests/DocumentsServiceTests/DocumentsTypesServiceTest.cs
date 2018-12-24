using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;
using DocumentManagement.DataAccess.Abstractions.Entities;
using DocumentManagement.DataAccess.Abstractions.Repositories;
using DocumentManagement.Models;

namespace DocumentManagement.Domain.Tests.DocumentsServiceTests
{
	public class DocumentsTypesServiceTest
	{
		private readonly DocumentsTypesService _documentsService;
		private readonly Mock<IDocumentsTypesRepository> _documentsRepositoryMock;

		public DocumentsTypesServiceTest()
		{
			_documentsRepositoryMock = new Mock<IDocumentsTypesRepository>();
			_documentsService = new DocumentsTypesService(_documentsRepositoryMock.Object);
		}

		[Fact]
		public async Task GetDocumentsTypesAsync_Returns_ReadOnlyCollectionOfDocuments()
		{
			//arrange
			var models = new List<DocumentType>
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

			_documentsRepositoryMock.Setup(mock => mock.GetDocumentsTypesAsync()).ReturnsAsync(models);

			//act
			var actual = await _documentsService.GetDocumentsTypes();

			//assert
			actual.Should().NotBeNull();
			actual.Count.Should().Equals(2);

			_documentsRepositoryMock.Verify(mock => mock.GetDocumentsTypesAsync(), Times.Once);
		}

		[Fact]
		public async Task GetDocumentsTypesAsync_Returns_ReadOnlyCollectionOfDocumentsTypes()
		{
			//arrange
			var expected = new List<DocumentTypeModel>
			{
				new DocumentTypeModel
				{
					ContentType = "image/jpeg",
					Extention = ".jpg",
					TypeId = 1
				},
				new DocumentTypeModel
				{
					ContentType = "image/gif",
					Extention = ".gif",
					TypeId = 2,
				}
			};

			var models = new List<DocumentType>
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

			_documentsRepositoryMock.Setup(mock => mock.GetDocumentsTypesAsync()).ReturnsAsync(models);

			//act
			var actual = await _documentsService.GetDocumentsTypes();

			//assert
			actual.Should().BeEquivalentTo(expected);
		}
	}
}
