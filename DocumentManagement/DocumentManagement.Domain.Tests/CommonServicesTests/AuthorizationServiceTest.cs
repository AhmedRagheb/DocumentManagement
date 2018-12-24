using System.Collections.Generic;
using FluentAssertions;
using Xunit;
using DocumentManagement.Domain.Abstractions.Authorization;

namespace DocumentManagement.Domain.Tests.CommonServicesTests
{
	public class AuthorizationServiceTest
	{
		private readonly AuthorizationService _authorizationService;

		public AuthorizationServiceTest()
		{
			_authorizationService = new AuthorizationService();
		}

		[Fact(DisplayName = "Get User Policies with null roles should return empty policies")]
		public void GetUserPolicies_WithNull_ShouldReturn_EmptyPolicies()
		{
			var expected = new List<string>().AsReadOnly();
			var actual = _authorizationService.GetUserPolicies(null);

			actual.Should().BeEquivalentTo(expected);
		}

		[Fact(DisplayName = "Get User Policies with empty roles should return empty policies")]
		public void GetUserPolicies_WithEmptyArray_ShouldReturn_EmptyPolicies()
		{
			var expected = new List<string>().AsReadOnly();
			var actual = _authorizationService.GetUserPolicies(new string[0]);

			actual.Should().BeEquivalentTo(expected);
		}

		[Fact(DisplayName = "Get User Policies with role admin should return right policies")]
		public void GetUserPolicies_ShouldReturn_RightPolicies()
		{
			var roles = new List<string>
			{
				Roles.Admin
			};

			var expected = new List<string>
			{
				Policies.UploadDocument,
                Policies.DownloadDocument,
                Policies.DeleteDocument
			};

			var actual = _authorizationService.GetUserPolicies(roles);

			actual.Should().BeEquivalentTo(expected);
		}

		[Fact(DisplayName = "Get all policies should return not empty Dictionary")]
		public void GetAllPolicies_ShouldReturn_NotEmptyDictionary()
		{
			var actual = _authorizationService.GetAllPolicies();

			actual.Should().NotBeEmpty();
			actual.Should().NotBeNull();
		}

		[Fact(DisplayName = "Get all policies should return Dictionary")]
		public void GetAllPolicies_ShouldReturn_Dictionary()
		{
			var actual = _authorizationService.GetAllPolicies();

			actual.Should().BeOfType<Dictionary<string, List<string>>>();
		}
	}
}
