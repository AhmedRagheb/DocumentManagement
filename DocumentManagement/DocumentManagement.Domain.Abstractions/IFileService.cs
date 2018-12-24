using System.IO;
using System.Threading.Tasks;

namespace DocumentManagement.Domain.Abstractions
{
	public interface IFileService
	{
		string UploadFile(MemoryStream stream, string fileName);

		string GetUploadsPath();

		Task<(byte[] fileContent, string filePath)> GetFile(string filePath, string fileName);
	}
}
