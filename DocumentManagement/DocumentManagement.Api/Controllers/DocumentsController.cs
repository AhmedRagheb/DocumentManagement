using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Domain.Abstractions;
using System.Threading.Tasks;
using DocumentManagement.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using DocumentManagement.Domain.Abstractions.Authorization;

namespace DocumentManagement.Api.Controllers
{
	public class DocumentsController : Controller
	{
		private readonly IDocumentsService _documentsService;

		public DocumentsController(IDocumentsService documentsService)
		{
			_documentsService = documentsService;
		}

		/// <summary>
		/// Get All documents
		/// </summary>
		/// <returns>Ok</returns>
		[Authorize]
		[HttpGet("documents")]
		[ProducesResponseType(typeof(IReadOnlyList<DocumentModel>), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> Get()
		{
			var documents = await _documentsService.GetDocuments().ConfigureAwait(false);
			return Ok(documents);
		}

		/// <summary>
		/// Delete a document
		/// </summary>
		/// <returns>NoContent</returns>
		[Authorize(Policy = Policies.DeleteDocument)]
		[HttpDelete("documents/{documentId}")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		public async Task<IActionResult> DeleteDocument(int documentId)
		{
			await _documentsService.DeleteDocument(documentId).ConfigureAwait(false);
			return NoContent();
		}
	}
}
