using Moq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Models;
using DocumentManagement.Api.Controllers;
using DocumentManagement.Domain.Abstractions;

namespace DocumentManagement.Api.Tests
{
	public class DocumentsFileControllerTests
	{
		private readonly DocumentsFileController _documentsFileController;
		private readonly Mock<IDocumentsUplaodFileService> _documentsUploadFileServiceMock;
		private readonly Mock<IDocumentsDownloadFileService> _documentsDownloadFileServiceMock;

		public DocumentsFileControllerTests()
		{
			_documentsUploadFileServiceMock = new Mock<IDocumentsUplaodFileService>();
			_documentsDownloadFileServiceMock = new Mock<IDocumentsDownloadFileService>();

			_documentsFileController = new DocumentsFileController(_documentsUploadFileServiceMock.Object, _documentsDownloadFileServiceMock.Object)
			{
				ControllerContext = IdentityControllerTest.CreateControllerContextInstance()
			};
		}

		[Fact(DisplayName = "Upload Document should return HTTP 200")]
		public async Task UploadDocumentHttpStatusOkTest()
		{
			//Arrange
			var fileMock = new Mock<IFormFile>();
			using (var ms = new MemoryStream())
			{
				using (var writer = new StreamWriter(ms))
				{
					writer.Write("fake File");
					fileMock.Setup(_ => _.FileName).Returns("test.pdf");
					fileMock.Setup(_ => _.Length).Returns(ms.Length);
					fileMock.Setup(_ => _.ContentType).Returns("application/pdf");
				}
			}

			//Act
			var actual = await _documentsFileController.UploadDocument(fileMock.Object) as OkObjectResult;

			//Assert
			actual.StatusCode.Should().Equals(200);
		}

		[Fact(DisplayName = "Upload Document should call service")]
		public async Task GetAllAuthorsServiceTest()
		{
			//Arrange
			var fileMock = new Mock<IFormFile>();
			using (var ms = new MemoryStream())
			{
				using (var writer = new StreamWriter(ms))
				{
					writer.Write("fake File");
					fileMock.Setup(_ => _.FileName).Returns("test.pdf");
					fileMock.Setup(_ => _.Length).Returns(ms.Length);
					fileMock.Setup(_ => _.ContentType).Returns("application/pdf");
				}
			}
			
			//Act
			await _documentsFileController.UploadDocument(fileMock.Object);

			//Assert
			_documentsUploadFileServiceMock.Verify(x => x.UploadDocument(It.IsAny<UploadDocumentInputModel>()));
		}

		[Fact(DisplayName = "Download Document should return Not null File Content Result")]
		public async Task DownloadDocumentsResultOkTest()
		{
			//Arrange
			var fakeStream = new MemoryStream();
			var model = new DocumentDownloadReturnModel
			{
				DocumentPath = "/documents/",
				DocumentContentType = "application/pdf",
				DocumentContent = fakeStream.ToArray()
			};

			_documentsDownloadFileServiceMock.Setup(service => service.DownloadDocument(1))
				.Returns(Task.FromResult(model));

			//Act
			var actual = await _documentsFileController.DownloadDocument(1) as FileContentResult;

			//Assert
			actual.Should().NotBeNull();
		}

		[Fact(DisplayName = "Download Document should call service")]
		public async Task DownloadDocumentsCallServiceTest()
		{
			//Arrange
			var fakeStream = new MemoryStream();
			var model = new DocumentDownloadReturnModel
			{
				DocumentPath = "/documents/",
				DocumentContentType = "application/pdf",
				DocumentContent = fakeStream.ToArray()
			};

			_documentsDownloadFileServiceMock.Setup(service => service.DownloadDocument(1))
				.Returns(Task.FromResult(model));

			//Act
			await _documentsFileController.DownloadDocument(1);

			//Assert
			_documentsDownloadFileServiceMock.Verify(x => x.DownloadDocument(1));
		}
	}
}
