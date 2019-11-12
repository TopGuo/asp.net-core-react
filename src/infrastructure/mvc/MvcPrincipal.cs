using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace infrastructure.mvc
{
    public class MvcPrincipal : ClaimsPrincipal
    {
        private List<MvcIdentity> identitys = new List<MvcIdentity>();

        private MvcIdentity identity;

        public override IIdentity Identity => identity;

        public MvcPrincipal()
        {
            identity = MvcIdentity.Instance;
            identitys.Add(identity);
        }

        public MvcPrincipal(MvcIdentity identity)
            : base(new ClaimsIdentity(identity, new Claim[1]
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", identity.RoleId.ToString(), "http://www.w3.org/2001/XMLSchema#string")
            }))
        {
            this.identity = identity;
            identitys.Add(identity);
        }
    }
}