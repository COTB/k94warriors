using K94Warriors.Data.Contracts;
using System.Web.Mvc;

namespace K94Warriors.Models.Binders
{
    public class DogProfileModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (value == null)
                return base.BindModel(controllerContext, bindingContext);

            string dogProfileIdParam = value.AttemptedValue;

            if (string.IsNullOrEmpty(dogProfileIdParam))
                return base.BindModel(controllerContext, bindingContext);

            int dogProfileId;

            if (!int.TryParse(dogProfileIdParam, out dogProfileId))
                return base.BindModel(controllerContext, bindingContext);

            // hack, can't inject here AFAIK
            var repo = DependencyResolver.Current.GetService<IRepository<DogProfile>>();

            var dog = repo.GetById(dogProfileId);

            return dog ?? base.BindModel(controllerContext, bindingContext);
        }
    }
}