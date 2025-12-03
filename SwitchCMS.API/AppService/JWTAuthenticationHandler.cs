using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using SwitchCMS.API.Services.Authentication;
using SwitchCMS.Model.Authentication;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SwitchCMS.API.AppService
{
    public class JWTAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public const string AuthScheme = "Bearer";
        public const string AuthHeader = "Authorization";
        private readonly IUserLoginService _userLoginService;

        public JWTAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserLoginService userLoginService
            )
            : base(options, logger, encoder, clock)
        {
            _userLoginService = userLoginService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            // Handle AllowAnonymous attribute
            var endpoint = Context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
                return AuthenticateResult.NoResult();

            if (!Request.Headers.ContainsKey(AuthHeader))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                string? token = null;
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers[AuthHeader]);
                if (authHeader.Scheme == AuthScheme) token = authHeader.Parameter;
                else return AuthenticateResult.Fail("Invalid authentication type");
                if (token == null) return AuthenticateResult.Fail("Invalid Authorization Header");
                UserAccount user = _userLoginService.GetUserDetails(token);
                if (user == null) return AuthenticateResult.Fail("Invalid Token");

                var claims = user.Claims.Select(x => new Claim(x.UserType ?? string.Empty, x.UserValue ?? string.Empty));
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("Authorization Failed");
            }
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.Headers["WWW-Authenticate"] = AuthScheme;
            return base.HandleChallengeAsync(properties);
        }

    }
}
