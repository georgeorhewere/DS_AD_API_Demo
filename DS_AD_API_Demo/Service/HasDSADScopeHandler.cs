using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DS_AD_API_Demo.Service
{
    public class HasDSADScopeHandler : AuthorizationHandler<HasDSADScope>
    {
        private readonly string adScope = "http://schemas.microsoft.com/identity/claims/scope";
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasDSADScope requirement)
        {
            // If user does not have the scope claim, get out of here
            if (!context.User.HasClaim(c => c.Type == adScope && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            // Split the scopes string into an array
            var scopes = context.User.FindFirst(c => c.Type == adScope && c.Issuer == requirement.Issuer).Value.Split(' ');

            // Succeed if the scope array contains the required scope
            if (scopes.Any(s => s == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
