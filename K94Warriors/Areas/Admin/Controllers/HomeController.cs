using K94Warriors.Filters;
using System.Web.Mvc;

namespace K94Warriors.Areas.Admin.Controllers
{
    [K9Authorize(Roles = "Admin")]
    public partial class HomeController : Controller
    {
        public virtual ActionResult Index() => View();
    }
}
