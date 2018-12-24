using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using DocumentManagement.Domain.Abstractions.Authorization;

namespace DocumentManagement.Api
{
	public class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
	{
        const string UserNameHeader = "Username";

        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) 
			: base(options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			var headers = Request.Headers;

			if (headers.ContainsKey(UserNameHeader))
			{
                var identity = new ClaimsIdentity("custom auth type");

                headers.TryGetValue(UserNameHeader, out StringValues userNameValue);

				var userName = userNameValue.ToString();
				AddUserClaims(userName, identity);
				CheckAdminRole(headers, identity);

				var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), Options.Scheme);

				return Task.FromResult(AuthenticateResult.Success(ticket));
			}
			else
			{
				return Task.FromResult(AuthenticateResult.Fail("Cannot read authorization header."));
			}
		}

		private static void AddUserClaims(string userName, ClaimsIdentity identity)
		{
			var claims = new List<Claim>
				{
					new Claim(ClaimTypes.Name, userName),
					new Claim(ClaimTypes.Role, Roles.User)
				};

			identity.AddClaims(claims);
		}

		private static void CheckAdminRole(IHeaderDictionary headers, ClaimsIdentity identity)
		{
			headers.TryGetValue("Admin", out StringValues admin);

			if (admin.ToString() == "1")
			{
				var adminRoleClaim = new List<Claim>
				{
					new Claim(ClaimTypes.NameIdentifier, "1"),
					new Claim(ClaimTypes.Role, Roles.Admin)
				};

				identity.AddClaims(adminRoleClaim);
			}
		}
	}
}