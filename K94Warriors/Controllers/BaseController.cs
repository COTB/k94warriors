using K94Warriors.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K94Warriors.Controllers
{
    public abstract class BaseController : Controller
    {
        protected void SetDogViewBag(DogProfile dog)
        {
            ViewBag.DogId = dog.ProfileID;
            ViewBag.DogName = dog.Name;
        }
    }
}
