using Makarevich_proj.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Makarevich_proj.Controllers
{
    public class ImageController (UserManager<AppUser> um): Controller
    {
        public async Task<IActionResult> GetAvatar()
        {
            //Находим полную информацию о пользователе
            var email = User.Identity.Name;
            var user=await um.FindByEmailAsync(email);
            if (user.Avatar != null)  return File(user.Avatar, "image/*"); 
            
            return File("images/5.jpg", "image/*");
        }
    }
}
