using System.Diagnostics;
using Domain;
using Microsoft.AspNetCore.Identity;

namespace Persistence
{
    public class Seed
    {
        public static async Task SeedData(DataContext context,
            UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<AppUser>
                {
                    new AppUser
                    {
                        Us_DisplayName = "Izztraab",
                        UserName = "izztraab",
                        Email = "izztraab@Outlook.com"
                    },
                    new AppUser
                    {
                        Us_DisplayName = "Shahraz Iqbal",
                        UserName = "shahraz",
                        Email = "shahraz.i@outlook.com"
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                
                await context.SaveChangesAsync();
            }
        }
    }
}
