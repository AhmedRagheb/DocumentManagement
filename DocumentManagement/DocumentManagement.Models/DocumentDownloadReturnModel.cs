
namespace DocumentManagement.Models
{
	public class DocumentDownloadReturnModel
	{
		public string DocumentPath { get; set; }
		public string DocumentContentType { get; set; }
		public byte[] DocumentContent { get; set; }
	}
}
