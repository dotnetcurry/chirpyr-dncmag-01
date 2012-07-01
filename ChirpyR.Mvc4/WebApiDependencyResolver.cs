using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChirpyR.Domain.Repository;
using System.Web.Http;
using ChirpyR.Mvc4.Controllers;

namespace ChirpyR.Mvc4
{
    public class WebApiDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
    {
        IChirpyRRepository _repository;

        public WebApiDependencyResolver(IChirpyRRepository repository)
        {
            _repository = repository;
        }
        public System.Web.Http.Dependencies.IDependencyScope BeginScope()
        {
            return this;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(BaseChirpyRController) ||
                serviceType == typeof(ChirpyRController))
            {
                return Activator.CreateInstance(serviceType);//, new object [] {_repository});
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return new List<object>();
        }

        public void Dispose()
        {
            // No Op
        }
    }
}