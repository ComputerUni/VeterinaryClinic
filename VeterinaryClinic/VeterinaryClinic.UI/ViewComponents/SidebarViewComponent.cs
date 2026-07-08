using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VeterinaryClinic.UI.ViewComponents
{
    public class SidebarViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user = ViewContext.HttpContext.User;
            var isAdmin = user.IsInRole("Manager");
            return View(isAdmin);
        }
    }
}
