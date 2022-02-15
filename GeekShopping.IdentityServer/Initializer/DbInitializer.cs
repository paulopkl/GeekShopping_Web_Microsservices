using GeekShopping.IdentityServer.DB.Model;
using GeekShopping.IdentityServer.DB.Model.Context;
using GeekShopping.IdentityServer.Models.Configuration;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using IdentityModel;

namespace GeekShopping.IdentityServer.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly MySQLContext _context;
        private readonly UserManager<ApplicationUser> _user;
        private readonly RoleManager<IdentityRole> _roles;

        public DbInitializer(MySQLContext context, UserManager<ApplicationUser> user, RoleManager<IdentityRole> roles)
        {
            _context = context;
            _user = user;
            _roles = roles;
        }

        public void Initialize()
        {
            // Verify if table 'aspnetroles' contains an Name = Admin
            if (_roles.FindByNameAsync(IdentityConfiguration.Admin).Result != null) return;

            // Add role "admin" to 'aspnetuserroles' table
            _roles.CreateAsync(new IdentityRole(IdentityConfiguration.Admin)).GetAwaiter().GetResult();

            // Add role "client" to 'aspnetuserroles' table
            _roles.CreateAsync(new IdentityRole(IdentityConfiguration.Client)).GetAwaiter().GetResult();

            // Add an admin user to 'aspnetusers' table
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = "paulo-admin",
                Email = "paulo-admin@hotmail.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (19) 98378-1727",
                FirstName = "Paulo",
                LastName = "Admin"
            };

            _user.CreateAsync(admin, "P@ulo123").GetAwaiter().GetResult();
            _user.AddToRoleAsync(admin, IdentityConfiguration.Admin).GetAwaiter().GetResult();

            // Add claim to 'aspnetuserclaims' table
            var adminClaims = _user.AddClaimsAsync(admin, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{admin.FirstName} {admin.LastName}"),
                new Claim(JwtClaimTypes.GivenName, admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName, admin.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Admin)
            }).Result;

            //////////////// ----------------
            
            // Add an admin user to 'aspnetusers'
            ApplicationUser client = new ApplicationUser()
            {
                UserName = "paulo-client",
                Email = "paulo-client@hotmail.com.br",
                EmailConfirmed = true,
                PhoneNumber = "+55 (19) 98378-1727",
                FirstName = "Paulo",
                LastName = "Client"
            };

            _user.CreateAsync(client, "P@ulo123").GetAwaiter().GetResult();
            _user.AddToRoleAsync(client, IdentityConfiguration.Client).GetAwaiter().GetResult();

            // Add claim to 'aspnetuserclaims'
            var clientClaims = _user.AddClaimsAsync(client, new Claim[]
            {
                new Claim(JwtClaimTypes.Name, $"{client.FirstName} {client.LastName}"),
                new Claim(JwtClaimTypes.GivenName, client.FirstName),
                new Claim(JwtClaimTypes.FamilyName, client.LastName),
                new Claim(JwtClaimTypes.Role, IdentityConfiguration.Client)
            }).Result;
        }
    }
}
