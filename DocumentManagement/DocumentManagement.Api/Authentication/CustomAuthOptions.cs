using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Primitives;

namespace DocumentManagement.Api
{
    public class CustomAuthOptions : AuthenticationSchemeOptions
    {
        public const string DefaultScheme = "basic auth";
        public string Scheme => DefaultScheme;
        public StringValues AuthKey { get; set; }
    }
}