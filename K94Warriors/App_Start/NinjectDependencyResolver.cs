using System.Web.Http.Dependencies;
using Ninject;

namespace K94Warriors.App_Start
{
    /// <summary>
    /// This class is the resolver, but it is also the global scope
    /// so we derive from NinjectScope.
    /// </summary>
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(kernel.BeginBlock());
        }
    }
}