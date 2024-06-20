using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Qode.Infrastructure.Models;
using System.Security.Claims;

namespace Microsoft.AspNetCore.Routing
{
    internal static class IdentityComponentsEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var accountGroup = endpoints.MapGroup("/Account");

            accountGroup.MapPost("/Logout", async (
                ClaimsPrincipal user,
                SignInManager<ApplicationUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect($"~/");
            });

            var manageGroup = accountGroup.MapGroup("/Manage").RequireAuthorization();

            return accountGroup;
        }
    }
}
