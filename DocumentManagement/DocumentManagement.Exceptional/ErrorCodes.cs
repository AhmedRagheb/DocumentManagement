namespace DocumentManagement.Exceptional
{
	/// <summary>
	/// All error codes are stored in this file, the format that we are using is four digits,
	/// the first digit determines what area the error originates from. The areas we have 
	/// identified so far are as following:
	/// </summary>
	public enum ErrorCodes
	{
		UnKnownError = 9999,
		InternalError = 9998,
		ValidationError = 9997,

		// Database exceptions
		DbSaveChangesException = 1001,

		// Service layer
		FileNotSuported = 2000,
		FileNotExist = 2001,
		FileSizeExceedLimit = 2002,
		UploadDocumentException = 3000,
		DownloadDocumentException = 3001
	}
}
