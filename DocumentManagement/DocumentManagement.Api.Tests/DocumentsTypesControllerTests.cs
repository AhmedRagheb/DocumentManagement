using DocumentManagement.Api.Controllers;
using DocumentManagement.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace DocumentManagement.Api.Tests
{
	public class DocumentsTypesControllerTests
	{
		private readonly DocumentsTypesController _documentsTypesController;
		private readonly Mock<IDocumentsTypesService> _documentsTypesServiceMock;

		public DocumentsTypesControllerTests()
		{
			_documentsTypesServiceMock = new Mock<IDocumentsTypesService>();
			_documentsTypesController = new DocumentsTypesController(_documentsTypesServiceMock.Object)
			{
				ControllerContext = IdentityControllerTest.CreateControllerContextInstance()
			};
		}

		[Fact(DisplayName = "Get all documents types should return HTTP 200")]
		public async Task GetAllDocumentsHttpStatusOkTest()
		{
			var actual = await _documentsTypesController.Get() as OkObjectResult;
			actual.StatusCode.Should().Equals(200);
		}

		[Fact(DisplayName = "Get get all documents types should call service")]
		public async Task GetAllDocumentsCallServiceTest()
		{
			await _documentsTypesController.Get();
			_documentsTypesServiceMock.Verify(x => x.GetDocumentsTypes());
		}
	}
}
