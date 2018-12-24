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
	public class DocumentsFileService_DownloadDocument_Test
	{
		private readonly DocumentsDownloadFileService _documentsFileService;

		private readonly Mock<IUnitOfWork> _unitOfWorkMock;
		private readonly Mock<IDocumentsRepository> _documentsRepositoryMock;
		private readonly Mock<IFileService> _fileSeriveMock;

		public DocumentsFileService_DownloadDocument_Test()
		{
			var dateServiceMock = SetupDateService();

			_unitOfWorkMock = MockUnitOfWork();
			_documentsRepositoryMock = SetupDocumentsRepositoryMock();
			_fileSeriveMock = SetupileServiceMock();

			_documentsFileService = new DocumentsDownloadFileService(
				_unitOfWorkMock.Object,
				_documentsRepositoryMock.Object,
				dateServiceMock.Object, 
				_fileSeriveMock.Object);
		}

		[Fact]
		public async Task DownloadDocument_Verfiy_CallGetFileService()
		{
			// Act
			await _documentsFileService.DownloadDocument(20);

			// Assert
			_fileSeriveMock.Verify(service => service.GetFile("uploads/documents/", "Test1.jpg"));
		}

		[Fact]
		public async Task UploadDocument_Verfiy_CallAddDocumentAsync()
		{
			// Act
			await _documentsFileService.DownloadDocument(20);

			// Assert
			_documentsRepositoryMock.Verify(service => service.UpdateDocumentAsync(It.IsAny<Document>()));
		}

		[Fact]
		public async Task UploadDocument_Verfiy_CallSaveChanges()
		{
			// Act
			await _documentsFileService.DownloadDocument(20);

			// Assert
			_unitOfWorkMock.Verify(service => service.SaveChangesAsync());
		}

		[Fact]
		public async Task DownloadDocument_Should_Return_NotNullModel()
		{
			// Arrange
			var fakeStream = new MemoryStream();
			var expected = new DocumentDownloadReturnModel
			{
				DocumentContent = fakeStream.ToArray(),
				DocumentContentType = "application/pdf",
				DocumentPath = "/wwwroot/documents/"
			};

			// Act
			var actual = await _documentsFileService.DownloadDocument(20);

			// Assert
			actual.Should().NotBeNull();
			actual.Should().Equals(expected);
		}

		[Fact]
		public async Task DownloadDocument_Should_Throw_Exception()
		{
			// Arrange
			_documentsRepositoryMock.Setup(service => service.GetDocumentAsync(20))
				.ReturnsAsync(() => null);

			// Act
			var exception = await Assert.ThrowsAsync<ServiceException>(() => _documentsFileService.DownloadDocument(20));

			// Assert
			exception.ErrorCode.Should().Equals(ErrorCodes.DownloadDocumentException);
		}

		[Fact]
		public async Task DownloadDocument_Should_Update_LastAccessDate()
		{
			// Arrange
			var document = new Document
			{
				DocumentId = 10,
				DocumentName = "Test2.jpg",
				DocumentSize = 500,
				DocumentPath = "uploads/documents/",
				DocumentTypeId = 1,
				UploadedBy = 1,
				UploadedDate = DateTime.UtcNow,
				LastAccesedDate = null,
				IsDeleted = false,
				DocumentType = new DocumentType
				{
					TypeId = 1,
					IsDeleted = false,
					Extention = ".PDF",
					ContentType = "application/pdf"
				}
			};

			_documentsRepositoryMock.Setup(service => service.GetDocumentAsync(10))
				.ReturnsAsync(document);

			// Act
			await _documentsFileService.DownloadDocument(10);

			// Assert
			document.LastAccesedDate.Should().Equals(new DateTime(2018, 05, 01, 1, 1, 1));
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
			var fakeStream = new MemoryStream();
			var file =  (fileContent: fakeStream.ToArray(), filePath: "/wwwroot/documents/");

			var fileServiceMock = new Mock<IFileService>();
			fileServiceMock.Setup(service => service.GetFile(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(() => file);

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
				IsDeleted = false,
				DocumentType = new DocumentType
				{
					TypeId = 1,
					IsDeleted = false,
					Extention = ".PDF",
					ContentType = "application/pdf"
				}
			};

			var documentsRepositoryMock = new Mock<IDocumentsRepository>();
			documentsRepositoryMock.Setup(service => service.GetDocumentAsync(It.IsAny<int>()))
				.ReturnsAsync(document);

			return documentsRepositoryMock;
		}

		private Mock<IDateService> SetupDateService()
		{
			var dateServiceMock = new Mock<IDateService>();
			dateServiceMock.Setup(date => date.UtcNow).Returns(new DateTime(2018, 05, 01, 1, 1, 1));

			return dateServiceMock;
		}
	}
}
