using AutoMapper;
using Repository.Abstract;
using Repository.Concrete;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace API
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new AutoMapperConfig()));
            container.RegisterInstance<IMapper>(config.CreateMapper());
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IAuthorRepository, AuthorRepository>();
            container.RegisterType<IBookRepository, BookRepository>();
            container.RegisterType<IBorrowerRepository, BorrowerRepository>();
            container.RegisterType<ILoanRepository, LoanRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}