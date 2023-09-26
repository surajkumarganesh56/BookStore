using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Book.Store.Web.Areas.StoreManagement.Controllers
{
    [Area("StoreManagement")]
    [Route("StoreManagement/Book")]
    public class BookController : Controller
    {
        [Authorize(Roles = "Store")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
