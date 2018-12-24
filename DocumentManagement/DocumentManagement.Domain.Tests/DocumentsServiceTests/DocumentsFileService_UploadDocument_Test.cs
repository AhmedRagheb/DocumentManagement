using System;
using Moq;
using System.IO;
using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using DocumentManagement.Models;
using DocumentManagement.DataAccess.Abstractions.Entities;
using DocumentManagement.Domain.Abstractions;
using DocumentManagement.Exceptional;
using DocumentManagement.DataAccess.Abstractions.Repositories;
using DocumentManagement.DataAccess.Abstractions;

namespace DocumentManagement.Domain.Tests.DocumentsServiceTests
{
	public class DocumentsFileService_UploadDocument_Test
	{
		private readonly DocumentsUplaodFileService _documentsFileService;

		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly Mock<IDocumentsRepository> _documentsRepositoryMock;
		private readonly Mock<IDocumentsTypesRepository> _documentsTypesRepositoryMock;
		private readonly Mock<IFileService> _fileSeriveMock;

		public DocumentsFileService_UploadDocument_Test()
		{
			var dateServiceMock = SetupDateService();

			_unitOfWorkMock = MockUnitOfWork();
			_documentsRepositoryMock = SetupDocumentsRepositoryMock();
			_documentsTypesRepositoryMock = SetupDocumentsTypesRepositoryMock();
			_fileSeriveMock = SetupileServiceMock();

			_documentsFileService = new DocumentsUplaodFileService(
				_unitOfWorkMock.Object,
				_documentsRepositoryMock.Object,
				_documentsTypesRepositoryMock.Object, 
				dateServiceMock.Object, 
				_fileSeriveMock.Object);
		}

		[Fact]
		public async Task UploadDocument_Verfiy_CallUploadFileService()
		{
			// Arrange
			var fakeStream = new MemoryStream();
			var uploadDocumentInputModel = new UploadDocumentInputModel
			{
				UploadedBy = 1,
				DocumentName = "test",
				DocumentStream = fakeStream,
				DocumentContentType = "application/pdf"
			};

			// Act
			await _documentsFileService.UploadDocument(uploadDocumentInputModel);

			// Assert
			_fileSeriveMock.Verify(service => service.UploadFile(fakeStream, "test"));
		}

		[Fact]
		public async Task UploadDocument_Verfiy_CallAddDocumentAsync()
		{
			// Arrange
			var fakeStream = new MemoryStream();
			var uploadDocumentInputModel = new UploadDocumentInputModel
			{
				UploadedBy = 1,
				DocumentName = "test",
				DocumentStream = fakeStream,
				DocumentContentType = "application/pdf"
			};

			// Act
			await _documentsFileService.UploadDocument(uploadDocumentInputModel);

			// Assert

			var document = new Document()
			{
				OriginalDocumentName = "test",
				DocumentName = "test",
				DocumentTypeId = 1,
				DocumentPath = "Documents",
				DocumentSize = 0,
				UploadedBy = 1,
				UploadedDate = new DateTime(2018, 05, 01, 1, 1, 1)
			};

			_documentsRepositoryMock.Verify(service => service.AddDocumentAsync(It.IsAny<Document>()));
		}

		[Fact]
		public async Task UploadDocument_Verfiy_CallSaveChanges()
		{
			// Arrange
			var fakeStream = new MemoryStream();
			var uploadDocumentInputModel = new UploadDocumentInputModel
			{
				UploadedBy = 1,
				DocumentName = "test",
				DocumentStream = fakeStream,
				DocumentContentType = "application/pdf"
			};

			// Act
			await _documentsFileService.UploadDocument(uploadDocumentInputModel);

			// Assert
			_unitOfWorkMock.Verify(service => service.SaveChangesAsync());
		}

		[Fact]
		public async Task UploadDocument_Should_Return_NotNullModel()
		{
			// Arrange
			var fakeStream = new MemoryStream();
			var uploadDocumentInputModel = new UploadDocumentInputModel
			{
				UploadedBy = 1,
				DocumentName = "test",
				DocumentStream = fakeStream,
				DocumentContentType = "application/pdf"
			};

			var expected = new UploadDocumentReturnModel
			{
				DocumentId = 0,
				OriginalDocumentName = "test"
			};

			// Act
			var actual = await _documentsFileService.UploadDocument(uploadDocumentInputModel);

			// Assert
			actual.Should().NotBeNull();
			actual.Should().Equals(expected);
		}

		[Fact]
		public async Task UploadDocument_Should_Throw_Exception()
		{
			// Arrange
			var fakeStream = new MemoryStream();
			var uploadDocumentInputModel = new UploadDocumentInputModel
			{
				UploadedBy = 1,
				DocumentName = "test",
				DocumentStream = fakeStream,
				DocumentContentType = "application/pdf"
			};

			_documentsTypesRepositoryMock.Setup(service => service.GetDocumentTypeAsync("application/pdf"))
				.ReturnsAsync(() => null);

			// Act
			var exception = await Assert.ThrowsAsync<ServiceException>(() => _documentsFileService.UploadDocument(uploadDocumentInputModel));

			// Assert
			exception.ErrorCode.Should().Equals(ErrorCodes.UploadDocumentException);
		}

		private Mock<IUnitOfWork> MockUnitOfWork()
		{
			var mockedDbTransaction = new Mock<IDbContextTransaction>();

			var unitOfWorkMock = new Mock<IUnitOfWork>();
			unitOfWorkMock.Setup(service => service.BeginTransactionAsync()).Returns(Task.FromResult(mockedDbTransaction.Object));

			return unitOfWorkMock;
		}

		private Mock<IFileService> SetupileServiceMock()
		{
			var fileServiceMock = new Mock<IFileService>();
			fileServiceMock.Setup(service => service.UploadFile(It.IsAny<MemoryStream>(), It.IsAny<string>())).Returns("NewFile");

			return fileServiceMock;
		}

		private Mock<IDocumentsRepository> SetupDocumentsRepositoryMock()
		{
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

			var documentsRepositoryMock = new Mock<IDocumentsRepository>();
			documentsRepositoryMock.Setup(service => service.GetDocumentAsync(It.IsAny<int>()))
				.ReturnsAsync(document);

			return documentsRepositoryMock;
		}

		private Mock<IDocumentsTypesRepository> SetupDocumentsTypesRepositoryMock()
		{
			var documentType = new DocumentType()
			{
				TypeId = 1,
				IsDeleted = false,
				Extention = ".PDF",
				ContentType = "application/pdf"
			};

			var documentsTypesRepositoryMock = new Mock<IDocumentsTypesRepository>();
			documentsTypesRepositoryMock.Setup(service => service.GetDocumentTypeAsync(It.IsAny<string>()))
				.ReturnsAsync(documentType);

			return documentsTypesRepositoryMock;
		}

		private Mock<IDateService> SetupDateService()
		{
			var dateServiceMock = new Mock<IDateService>();
			dateServiceMock.Setup(date => date.UtcNow).Returns(new DateTime(2018, 05, 01, 1, 1, 1));

			return dateServiceMock;
		}
	}
}
