using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using DocumentManagement.DataAccess.DBContext;
using System;

namespace DocumentManagement.DataAccess.Tests
{
	public abstract class InMemoryDbContextTest
	{
		protected Mock<DocumentDbContext> DbContextMock { get; }
		protected DocumentDbContext DbContext => DbContextMock.Object;

		protected InMemoryDbContextTest()
		{
			var options = new DbContextOptionsBuilder<DocumentDbContext>()
				.UseInMemoryDatabase(Guid.NewGuid().ToString())
				.EnableSensitiveDataLogging()
				.ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
				.Options;

			DbContextMock = new Mock<DocumentDbContext>(options) { CallBase = true };
		}
	}
}
