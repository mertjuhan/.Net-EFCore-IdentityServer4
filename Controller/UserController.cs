using Loggerdinates.API.IdentityServer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace Loggerdinates.API.IdentityServer.Controller
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserSaveViewModel model)
        {
            ApplicationUser user = new ApplicationUser();
            user.Email = model.Email;
            user.UserName = model.UserName;

           var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x=>x.Description));
            }

            return Ok("Üye başarıyla Kaydedildi.");
            
        }
    }
}
