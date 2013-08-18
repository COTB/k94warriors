using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K94Warriors.Filters
{
    /// <summary>
    /// Maps Page Title from Route to ViewData/Bag.
    /// </summary>
    public class PageTitleAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Occurs just before the Action is executed.
        /// </summary>
        /// <param name="filterContext">The filter context.</param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // Get page title
            var pageTitle = filterContext.RouteData.Values["pageTitle"] as string;
            // If not set make it the action title
            if (string.IsNullOrWhiteSpace(pageTitle))
            {
                pageTitle = filterContext.RouteData.Values["action"] as string;
            }

            // Save to ViewData
            filterContext.Controller.ViewData["PageTitle"] = pageTitle;

            base.OnActionExecuting(filterContext);
        }
    }
}