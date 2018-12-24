using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace DocumentManagement.DataAccess.Abstractions
{
	public interface IUnitOfWork
	{
		Task<int> SaveChangesAsync();

		Task<IDbContextTransaction> BeginTransactionAsync();
	}
}
