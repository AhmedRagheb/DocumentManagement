using System.IO;
using Microsoft.AspNetCore.Hosting;
using DocumentManagement.Domain.Abstractions;
using System.Threading.Tasks;

namespace DocumentManagement.Domain.Common
{
	public class FileService : IFileService
	{
		const string DocumentFolder = "documents";

		private readonly IDateService _dateService;
		private readonly IHostingEnvironment _hostingEnvironment;

		public FileService(
			IDateService dateServce,
			IHostingEnvironment hostingEnvironment)
		{
			_hostingEnvironment = hostingEnvironment;
			_dateService = dateServce;
		}

		public string UploadFile(MemoryStream stream, string fileName)
		{
			string uploadsFolder = GetUploadsFolderPath();

			var timestamp = _dateService.UtcNowString;
			var uniqueFileName = $"{timestamp}_{fileName}";

			var filePath = Path.Combine(uploadsFolder, uniqueFileName);

			CreateFileStream(stream, filePath);

			return uniqueFileName;
		}

		public string GetUploadsPath()
		{
			return DocumentFolder;
		}

		public async Task<(byte[] fileContent, string filePath)> GetFile(string filePath, string fileName)
		{
			var path = Path.Combine(GetUploadsRootPath(), filePath, fileName);
			var memory = new MemoryStream();
			using (var stream = new FileStream(path, FileMode.Open))
			{
				await stream.CopyToAsync(memory).ConfigureAwait(false);
			}
			memory.Position = 0;

			return (memory.ToArray(), path);
		}

		private string GetUploadsRootPath()
		{
			if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
			{
				_hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
			}

			return _hostingEnvironment.WebRootPath;
		}

		private string GetUploadsFolderPath()
		{
			string rootPath = GetUploadsRootPath();
			var uploadsFolder = Path.Combine(rootPath, DocumentFolder);

			if (!Directory.Exists(uploadsFolder))
			{
				Directory.CreateDirectory(uploadsFolder);
			}

			return uploadsFolder;
		}

		private void CreateFileStream(MemoryStream documentStream, string filePath)
		{
			using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
			{
				documentStream.WriteTo(file);
				file.Close();
			}
		}

	}
}
