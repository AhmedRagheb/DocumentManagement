using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DocumentManagement.Models;
using DocumentManagement.Domain.Abstractions;

namespace DocumentManagement.Api.Controllers
{
	public class DocumentsTypesController : IdentityController
	{
		private readonly IDocumentsTypesService _documentsTypesService;

		public DocumentsTypesController(IDocumentsTypesService documentsTypesService)
		{
			_documentsTypesService = documentsTypesService;
		}

		/// <summary>
		/// Get All documents Types
		/// </summary>
		/// <returns>Ok</returns>
		[Authorize]
		[HttpGet("documents-Types")]
		[ProducesResponseType(typeof(IReadOnlyList<DocumentTypeModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get()
		{
			var documentsTypes = await _documentsTypesService.GetDocumentsTypes().ConfigureAwait(false);

			return Ok(documentsTypes);
		}
	}
}