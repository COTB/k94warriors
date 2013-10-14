using K94Warriors.Filters;
using K94Warriors.Models;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace K94Warriors.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void SetDogViewBag(DogProfile dog)
        {
            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;
        }

        protected int CurrentUserId
        {
            get 
            {
                var user = Session["CurrentUser"] as User;

                if (user == null)
                {
                    user = K9AuthorizeAttribute.ReloadUserIntoSession(HttpContext);
                }

                return user.UserID;
            }
        }
    }
}
