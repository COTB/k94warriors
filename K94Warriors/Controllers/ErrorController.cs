using System.Net;
using System.Web.Mvc;

namespace K94Warriors.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Error404()
        {
            Response.StatusCode = (int) HttpStatusCode.NotFound;
            return View();
        }

        public ActionResult Error403()
        {
            Response.StatusCode = (int) HttpStatusCode.Forbidden;
            return View();
        }
    }
}
