using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using DocumentManagement.DataAccess.DBContext;
using DocumentManagement.Exceptional;
using DocumentManagement.DataAccess.Abstractions;

namespace DocumentManagement.DataAccess
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly IDocumentDbContext _dbContext;

		public UnitOfWork(IDocumentDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<IDbContextTransaction> BeginTransactionAsync()
		{
			return await _dbContext.BeginTransactionAsync().ConfigureAwait(false);
		}

		public virtual async Task<int> SaveChangesAsync()
		{
			try
			{
				var result = await _dbContext.SaveChangesAsync().ConfigureAwait(false);
				return result;
			}
			catch (Exception exception)
			{
				throw new DataAccessException(ErrorCodes.DbSaveChangesException, "Something went wrong in the database.", exception);
			}
		}
	}
}
