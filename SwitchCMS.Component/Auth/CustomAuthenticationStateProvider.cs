using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SwitchCMS.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SwitchCMS.Component.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        [Inject]
        ISessionStorageService session { get; set; }

        public CustomAuthenticationStateProvider(ISessionStorageService _session)
        {
            this.session = _session;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            try
            {
                var token = await session.GetItemAsync<AuthenticationResponseModel>("Token");
                if (token == null)
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

                TokenHandler tokenHandler = new TokenHandler();
                var identity = new ClaimsIdentity(new Claim[]
               {
                new Claim(ClaimTypes.Role, token.Role.ToString()!),
                new Claim(ClaimTypes.Email, token.UserName!)
               },
               "Basic Auth");
                var user = new ClaimsPrincipal(identity);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
                return await Task.FromResult(new AuthenticationState(user));

            }
            catch
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

        }

        public async Task UpdateAuthenticationState(AuthenticationResponseModel Model)
        {
            ClaimsPrincipal claimsPrincipal;
            if (Model != null)
            {
                await session.SetItemAsync("Token", Model);
                TokenHandler tokenHandler = new TokenHandler();

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Role, Model.Role.ToString()!),
                new Claim(ClaimTypes.Email,Model.UserName!)
                },
                "Basic Auth"));
            }
            else
            {
                await session.RemoveItemAsync("Token");
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

    }
}
