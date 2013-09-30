using System.Web.Mvc;
using WebMatrix.WebData;

namespace K94Warriors.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected int CurrentUserId
        {
            get { return WebSecurity.CurrentUserId; }
        }
    }
}
