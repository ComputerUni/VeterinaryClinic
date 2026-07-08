using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VeterinaryClinic.UI.ViewComponents
{
    public class ProfileInfoViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user = ViewContext.HttpContext.User;
            var userName = user.FindFirst(ClaimTypes.Name)?.Value;
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            return View((userName, email, role));
        }
    }
}
