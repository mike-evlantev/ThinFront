using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using ThinFront.Core.Infrastructure;

namespace ThinFront.API.Infrastructure
{
    public class ThinFrontAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private Func<IAuthorizationRepository> _authRepository;
        private IAuthorizationRepository AuthRepository
        {
            get
            {
                return _authRepository.Invoke();
            }
        }

        public ThinFrontAuthorizationServerProvider(Func<IAuthorizationRepository> authRepositoryFactory)
        {
            _authRepository = authRepositoryFactory;
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            // Validating the user
            var user = await AuthRepository.FindUser(context.UserName, context.Password);

            // If username/password dont match OR user doesnt exist  
            if (user == null)
            {
                context.SetError("invalid_grant", "The username or password is incorrect");
                return;
            }
            else
            {
                // If the username/password match the authentication token is granted
                var token = new ClaimsIdentity(context.Options.AuthenticationType);
                token.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                token.AddClaim(new Claim(ClaimTypes.Role, user.Role.Name));
                context.Validated(token);
            }
        }
    }
}