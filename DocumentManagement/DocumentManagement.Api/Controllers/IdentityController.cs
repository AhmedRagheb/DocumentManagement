using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.Api
{
    /// <summary>
    /// Identity Controller to get current authorized user claims
    /// </summary>
    public class IdentityController : Controller
    {
        protected IdentityController()
        {
        }

        /// <summary>
        /// Gets the UserIdClaim
        /// </summary>
        public static readonly string UserIdClaim = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

		/// <summary>
		/// The user role claim
		/// </summary>
		public static readonly string UserRoleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

		/// <summary>
		/// Gets the user identifier from user claims.
		/// </summary>
		/// <value>
		/// The user identifier.
		/// </value>
		protected int UserId => int.TryParse(User.Claims.Single(claim => claim.Type == UserIdClaim)?.Value, out var userId) ? userId : throw new InvalidOperationException();

		/// <summary>
		/// Gets the user roles.
		/// </summary>
		/// <returns>Roles array</returns>
		protected string[] GetUserRoles()
	    {
		    return User.Claims.Where(claim => claim.Type == UserRoleClaim).Select(role => role.Value).ToArray();
		}
	}
}
