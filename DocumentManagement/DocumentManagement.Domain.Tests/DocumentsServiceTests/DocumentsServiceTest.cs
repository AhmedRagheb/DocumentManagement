using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using FluentAssertions;
using DocumentManagement.DataAccess.Abstractions.Entities;
using DocumentManagement.DataAccess.Abstractions;
using DocumentManagement.DataAccess.Abstractions.Repositories;
using DocumentManagement.Models;

namespace DocumentManagement.Domain.Tests.DocumentsServiceTests
{
	public class DocumentsServiceTest
	{
		private readonly DocumentsService _documentsService;
		private readonly Mock<IDocumentsRepository> _documentsRepositoryMock;
		private readonly Mock<IUnitOfWork> _unitOfWorkMock;

		public DocumentsServiceTest()
		{
			_unitOfWorkMock = new Mock<IUnitOfWork>();
			_documentsRepositoryMock = new Mock<IDocumentsRepository>();

			_documentsService = new DocumentsService(_unitOfWorkMock.Object, _documentsRepositoryMock.Object);
		}

		[Fact]
		public async Task GetDocumentsAsync_Returns_ReadOnlyCollectionOfDocuments()
		{
			//arrange
			var models = new List<Document>
			{
				new Document { DocumentId = 1, LastAccesedDate = new DateTime(2017, 05, 1) },
				new Document { DocumentId = 2, LastAccesedDate = new DateTime(2018, 05, 1) }
			};

			_documentsRepositoryMock.Setup(mock => mock.GetDocumentsAsync()).ReturnsAsync(models);

			//act
			var actual = await _documentsService.GetDocuments();

			//assert
			actual.Should().NotBeNull();
			actual.Count.Should().Equals(2);

			_documentsRepositoryMock.Verify(mock => mock.GetDocumentsAsync(), Times.Once);
		}

		[Fact]
		public async Task GetDocumentsAsync_Returns_ReadOnlyCollectionOfDocuments_WithRestrictedOrder()
		{
			//arrange
			var expected = new List<DocumentModel>
			{
				new DocumentModel
				{
					DocumentId = 11,
					OriginalDocumentName = "Test1.jpg",
					DocumentSize = 105520,
					UploadedDate = new DateTime(2018, 05, 1),
					LastAccesedDate = new DateTime(2018, 05, 1),
				},
				new DocumentModel
				{
					DocumentId = 10,
					OriginalDocumentName = "Test.jpg",
					DocumentSize = 105520,
					UploadedDate = new DateTime(2017, 05, 1),
					LastAccesedDate = new DateTime(2017, 05, 1),
				}
			};

			var models = new List<Document>
			{
				new Document
				{
					DocumentId = 10,
					DocumentName = "Test.jpg",
					OriginalDocumentName = "Test.jpg",
					DocumentSize = 105520,
					DocumentPath = "uploads/documents/",
					DocumentTypeId = 1,
					UploadedBy= 1,
					UploadedDate = new DateTime(2017, 05, 1),
					LastAccesedDate = new DateTime(2017, 05, 1),
					IsDeleted = false
				},
				new Document
				{
					DocumentId = 11,
					DocumentName = "Test1.jpg",
					OriginalDocumentName = "Test1.jpg",
					DocumentSize = 105520,
					DocumentPath = "uploads/documents/",
					DocumentTypeId = 1,
					UploadedBy= 1,
					UploadedDate =new DateTime(2018, 05, 1),
					LastAccesedDate = new DateTime(2018, 05, 1),
					IsDeleted = false
				},
			};

			_documentsRepositoryMock.Setup(mock => mock.GetDocumentsAsync()).ReturnsAsync(models);

			//act
			var actual = await _documentsService.GetDocuments();

			//assert
			actual.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
		}

		[Fact]
		public async Task DeleteDocumentAsync_Verify_CallService()
		{
			//arrange
			_documentsRepositoryMock.Setup(mock => mock.DeleteDocumentAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(true));

			//act
			await _documentsService.DeleteDocument(1);

			//assert
			_documentsRepositoryMock.Verify(mock => mock.DeleteDocumentAsync(1), Times.Once);
		}

		[Fact]
		public async Task DeleteDocumentAsync_Verify_CallSaveChanges()
		{
			//arrange
			_documentsRepositoryMock.Setup(mock => mock.DeleteDocumentAsync(It.IsAny<int>()))
				.Returns(Task.FromResult(true));

			//act
			await _documentsService.DeleteDocument(1);

			//assert
			_unitOfWorkMock.Verify(mock => mock.SaveChangesAsync(), Times.Once);
		}
	}
}
