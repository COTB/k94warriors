using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace K94Warriors.Core.HtmlHelperExtensions
{
    public static class EnumDropdownHelper
    {
        public static HtmlString EnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                        Expression<Func<TModel, TProperty>> modelExpression,
                                                                        string firstElement,
                                                                        IDictionary<string, object> htmlAttributes)
        {
            var typeOfProperty = modelExpression.ReturnType;
            if (!typeOfProperty.IsEnum)
                throw new ArgumentException(string.Format("Type {0} is not an enum", typeOfProperty));

            var enumValues = new SelectList(Enum.GetValues(typeOfProperty));
            return htmlHelper.DropDownListFor(modelExpression, enumValues, firstElement, htmlAttributes);
        }

        public static HtmlString EnumDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
                                                                        Expression<Func<TModel, TProperty>> modelExpression, 
                                                                        string firstElement)
        {
            return EnumDropDownListFor(htmlHelper, modelExpression, firstElement, new Dictionary<string, object>());
        }
    }
}