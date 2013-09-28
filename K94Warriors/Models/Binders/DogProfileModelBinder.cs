using K94Warriors.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace K94Warriors.Models.Binders
{
    public class DogProfileModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            string dogProfileIdParam = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).AttemptedValue;

            int dogProfileId;

            if (!int.TryParse(dogProfileIdParam, out dogProfileId))
                return null;

            // hack, can't inject here AFAIK
            var repo = DependencyResolver.Current.GetService<IRepository<DogProfile>>();

            var dog = repo.GetById(dogProfileId);

            return dog;
        }
    }
}