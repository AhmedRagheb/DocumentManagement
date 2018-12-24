using System;

namespace DocumentManagement.Domain.Abstractions
{
	public interface IDateService
	{
		DateTime UtcNow { get; }
		string UtcNowString { get; }
	}
}
