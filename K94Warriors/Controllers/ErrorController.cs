using System.Net;
using System.Web.Mvc;

namespace K94Warriors.Controllers
{
    public partial class ErrorController : Controller
    {
        //
        // GET: /Error/

        public virtual ActionResult Error404()
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
            return View();
        }

        public virtual ActionResult Error403()
        {
            Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return View();
        }
    }
}
