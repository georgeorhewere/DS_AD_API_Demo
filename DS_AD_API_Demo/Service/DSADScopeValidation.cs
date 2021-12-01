using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DS_AD_API_Demo.Service
{
    public class HasDSADScope : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Scope { get; }

        public HasDSADScope(string scope, string issuer)
        {
            Scope = scope ?? throw new ArgumentNullException(nameof(scope));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }


    }
}
