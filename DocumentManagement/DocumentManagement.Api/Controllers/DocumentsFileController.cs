using System.Threading.Tasks;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using DocumentManagement.Models;
using DocumentManagement.Domain.Abstractions;
using DocumentManagement.Domain.Abstractions.Authorization;

namespace DocumentManagement.Api.Controllers
{
	public class DocumentsFileController : IdentityController
	{
		private const int RequestSizeLimitBytes = 100000000;

		private readonly IDocumentsUplaodFileService _documentsFileService;
		private readonly IDocumentsDownloadFileService _documentsDownloadFileService;

		public DocumentsFileController(IDocumentsUplaodFileService documentsFileService, IDocumentsDownloadFileService documentsDownloadFileService)
		{
			_documentsFileService = documentsFileService;
			_documentsDownloadFileService = documentsDownloadFileService;
		}

		/// <summary>
		/// Uploads the Document.
		/// </summary>
		/// <param name="file">The attached file.</param>
		/// <returns>OkResult</returns>
		[Authorize(Policy = Policies.UploadDocument)]
		[HttpPost("documents")]
		[ProducesResponseType(typeof(UploadDocumentReturnModel), (int)HttpStatusCode.OK)]
		[RequestSizeLimit(RequestSizeLimitBytes)]
		public async Task<IActionResult> UploadDocument(IFormFile file)
		{
			using (var stream = new MemoryStream())
			{
				await file.CopyToAsync(stream).ConfigureAwait(false);

				var uploadDocumentInputModel = new UploadDocumentInputModel
				{
					DocumentName = file.FileName,
					DocumentContentType = file.ContentType,
					DocumentStream = stream,
					UploadedBy = UserId
				};

				var uploadedDocument = await _documentsFileService.UploadDocument(uploadDocumentInputModel).ConfigureAwait(false);

				return Ok(uploadedDocument);
			}
		}

		/// <summary>
		/// Download the Document.
		/// </summary>
		/// <param name="documentId">The documentId.</param>
		/// <returns>File</returns>
		[HttpGet("documents/downloads/{documentId}")]
		[Authorize(Policy = Policies.DownloadDocument)]
		[ProducesResponseType(typeof(DocumentDownloadReturnModel), (int)HttpStatusCode.OK)]
		public async Task<IActionResult> DownloadDocument(int documentId)
		{
			var downloadDocument = await _documentsDownloadFileService.DownloadDocument(documentId).ConfigureAwait(false);

			return File(downloadDocument.DocumentContent, downloadDocument.DocumentContentType, downloadDocument.DocumentPath);
		}
	}
}
