using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using ChirpyR.Domain.Repository;

namespace ChirpyR.Mvc4
{


    public class CompositionRoot
    {
        private readonly IDependencyResolver _dependencyResolver;

        public CompositionRoot()
        {
            this._dependencyResolver = CompositionRoot.CreateControllerFactory();
        }

        public IDependencyResolver DependencyResolver
        {
            get
            {
                return _dependencyResolver;
            }
        }

        private static IDependencyResolver CreateControllerFactory()
        {
            
            IChirpyRRepository repository = (IChirpyRRepository)Activator.CreateInstance(Type.GetType(
                    "ChirpyR.Data.Repository.ChirpyRSqlRepository, ChirpyR.Data", true),
                    new object [] { "ChirpyRConnection", "dbo" });
            var resolver = new WebApiDependencyResolver(repository);
            return resolver;
        }
    }
}