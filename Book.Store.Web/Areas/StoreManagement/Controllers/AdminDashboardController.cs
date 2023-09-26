using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Book.Store.Web.Areas.StoreManagement.Controllers
{
    [Area("StoreManagement")]
    [Route("StoreManagement/AdminDashboard")]
    public class AdminDashboardController : Controller
    {
        [Authorize(Roles = "Admin")]
        [HttpGet, Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
