using System.Collections.Generic;

namespace DocumentManagement.Domain.Abstractions
{
	public interface IAuthorizationService
	{
		IReadOnlyCollection<string> GetUserPolicies(IEnumerable<string> roles);

		Dictionary<string, List<string>> GetAllPolicies();
	}
}
