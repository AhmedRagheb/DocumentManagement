using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DocumentManagement.Domain.Abstractions.Authorization;

namespace DocumentManagement.Api.Tests
{
	public static class IdentityControllerTest
	{
		public static ControllerContext CreateControllerContextInstance()
		{
			var controllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext
				{
					User = GetUserClaimsTest()
				}
			};

			return controllerContext;
		}

		private static ClaimsPrincipal GetUserClaimsTest()
		{
			return new ClaimsPrincipal(new ClaimsIdentity(new[]
			{
				new Claim(IdentityController.UserIdClaim, "1"),
				new Claim(IdentityController.UserRoleClaim, Roles.Admin)
			}));
		}
	}
}
