using DocumentManagement.Api.Controllers;
using DocumentManagement.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DocumentManagement.Api.Tests
{
	public class DocumentsControllerTests
	{
		private readonly DocumentsController _documentsController;
		private readonly Mock<IDocumentsService> _documentsServiceMock;

		public DocumentsControllerTests()
		{
			_documentsServiceMock = new Mock<IDocumentsService>();
			_documentsController = new DocumentsController(_documentsServiceMock.Object)
			{
				ControllerContext = IdentityControllerTest.CreateControllerContextInstance()
			};
		}

		[Fact(DisplayName = "Get all documents should return HTTP 200")]
		public async Task GetAllDocumentsHttpStatusOkTest()
		{
			var actual = await _documentsController.Get() as OkObjectResult;
			actual.StatusCode.Should().Equals(200);
		}

		[Fact(DisplayName = "Get get all documents should call service")]
		public async Task GetAllDocumentsCallServiceTest()
		{
			await _documentsController.Get();
			_documentsServiceMock.Verify(x => x.GetDocuments());
		}

		[Fact(DisplayName = "DeleteDocument should return HTTP 204")]
		public async Task DeleteDocumentHttpStatusOkTest()
		{
			var actual = await _documentsController.DeleteDocument(1) as NoContentResult;
			actual.StatusCode.Should().Equals(204);
		}

		[Fact(DisplayName = "DeleteDocument should call service")]
		public async Task DeleteDocumentCallServiceTest()
		{
			await _documentsController.DeleteDocument(1);
			_documentsServiceMock.Verify(x => x.DeleteDocument(1));
		}
	}
}
