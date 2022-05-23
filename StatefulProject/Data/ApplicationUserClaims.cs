using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
namespace StatefulProject.Data
{
    public class ApplicationUserClaims : UserClaimsPrincipalFactory<ApplicationUser>
    {
        public ApplicationUserClaims(
            UserManager<ApplicationUser> userManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, optionsAccessor)
        {/*Base Constructor*/}

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FullNameEn", user.FullNameEn ?? "Un-Named"));
            return identity;
        }
    }
}
