using System;
using FluentAssertions;
using Moq;
using Xunit;
using DocumentManagement.Domain.Common;

namespace DocumentManagement.Domain.Tests.CommonServicesTests
{
	public class DateServiceTest
	{
		private readonly DateService _dateService;
		public DateServiceTest()
		{
			_dateService = new Mock<DateService>().Object;
		}

		[Fact(DisplayName = "UtcNow test should retuen date close to dateTime now")]
		public void UtcNowTest_should_return_dateCloseToDateTimeNow()
		{
			// Arrange 
			const int oneMinute = 60000;

			//Act
			var actual = _dateService.UtcNow;

			//Assert
			actual.Should().BeCloseTo(DateTime.UtcNow, oneMinute);
		}

		[Fact(DisplayName = "UtcNow string test should return not null date string")]
		public void UtcNowStringTest_should_return_NotNullDateString()
		{
			//Act
			var actual = _dateService.UtcNowString;

			//Assert
			actual.Should().NotBeNull();
		}
	}
}
