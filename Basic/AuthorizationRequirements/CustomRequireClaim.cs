using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Basic.AuthorizationRequirements
{
    public class CustomRequireClaim : IAuthorizationRequirement
    {
        public string ClaimType { get; }
        public CustomRequireClaim(string claimType)
        {
            this.ClaimType = claimType;

        }
    }

    public class CustomRequireClaimHandler : AuthorizationHandler<CustomRequireClaim>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context
            , CustomRequireClaim requirement)
        {
            var result = context.User.Claims.Any(x => x.Type == requirement.ClaimType);
            if (result)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
    public static class AuthorizationPolicyBuilderExtensions{
        public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder
            , string claimType){
                builder.AddRequirements(new CustomRequireClaim(claimType));
                return builder;
        }
    }
}