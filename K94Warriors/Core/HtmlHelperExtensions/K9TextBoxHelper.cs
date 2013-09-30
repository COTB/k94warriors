using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace K94Warriors.Core.HtmlHelperExtensions
{
    public static class K9TextBoxHelper
    {
        public static MvcHtmlString K9TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                    Expression<Func<TModel, TProperty>> expression,
                                                                    string placeholder)
        {
            return htmlHelper.K9TextBoxFor(expression, placeholder, "form-control input-lg");
        }

        public static MvcHtmlString K9TextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                    Expression<Func<TModel, TProperty>> expression,
                                                                    string placeholder, 
                                                                    string @class)
        {
            return htmlHelper.TextBoxFor(expression, new Dictionary<string, object>
                {
                    {"placeholder", placeholder},
                    {"class", @class}
                });
        }
    }
}