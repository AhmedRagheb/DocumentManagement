using System.Collections.Generic;
using System.Linq;
using DocumentManagement.Domain.Abstractions.Authorization;
using DocumentManagement.Domain.Abstractions;

namespace DocumentManagement.Domain.Abstractions.Authorization
{
	public class AuthorizationService : IAuthorizationService
	{
		public IReadOnlyCollection<string> GetUserPolicies(IEnumerable<string> roles)
		{
			if (roles == null)
			{
				return new List<string>().AsReadOnly();
			}

			var allPolicies = GetAllPolicies();
			var userPolicies = roles.SelectMany(role => allPolicies.Where(policy => policy.Value.Contains(role)).Select(x => x.Key))
									.Distinct().ToList();

			return userPolicies.AsReadOnly();
		}

		public Dictionary<string, List<string>> GetAllPolicies()
		{
			var policies = new Dictionary<string, List<string>>
			{
				{
					Policies.UploadDocument, new List<string>
					{
						Roles.Admin
					}
				},
				{
					Policies.DownloadDocument, new List<string>
					{
						Roles.Admin,
						Roles.User,
					}
				},
				{
					Policies.DeleteDocument, new List<string>
					{
						Roles.Admin,
						Roles.User
					}
				}
			};

			return policies;
		}
	}
}
