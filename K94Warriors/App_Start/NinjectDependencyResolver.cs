using System.Web.Http.Dependencies;
using Ninject;

namespace K94Warriors
{
    /// <summary>
    /// This class is the resolver, but it is also the global scope
    /// so we derive from NinjectScope.
    /// </summary>
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_kernel.BeginBlock());
        }
    }
}