using System;
using DocumentManagement.Domain.Abstractions;

namespace DocumentManagement.Domain.Common
{
	public class DateService : IDateService
	{
		public string UtcNowString => $"{DateTime.UtcNow:yyyyMMddhhmmss}";
		public DateTime UtcNow => DateTime.UtcNow;
	}
}
