using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Makarevich_proj.Data
{
    public class DbInit
    {
        public static async Task SetupIdentityAdmin(WebApplication application) //БД это скоуп сервис. 
        {
            using var scope = application.Services.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

            var user = await userManager.FindByEmailAsync("admin@gmail.com"); //Проверяем, есть ли пользователь с таким имейлом. Если нет- то создаём.
            if (user == null)
            {
                user = new AppUser();
                await userManager.SetEmailAsync(user, "admin@gmail.com");
                await userManager.SetUserNameAsync(user, user.Email);
                user.EmailConfirmed = true;
                await userManager.CreateAsync(user, "123456");

                var claim = new Claim(ClaimTypes.Role, "admin"); //Создаём клеймо со значением
                await userManager.AddClaimAsync(user, claim); //Добавляем клеймо
            }
        }

    }
}
