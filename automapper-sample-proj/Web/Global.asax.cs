using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using DataModels = Repo.EF.Models;
using DomainModels = Domain.Models;
using Autofac.Integration.Mvc;
using Log.Logic;
using Log.Contracts;

namespace Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{

			var mapperConfig = new AutoMapper.MapperConfiguration(cfg =>
			{
				cfg.CreateMap<DataModels.Employee, DomainModels.Employee>().ReverseMap();
				cfg.CreateMap<DataModels.Status, DomainModels.Status>().ReverseMap();
			});
			var mapper = mapperConfig.CreateMapper();

			var logger = new CustomLog();
			var unit = Repo.EF.UnitOfWork.CreateUnitOfWorkWithCustomLog(logger);
			var repo = Repo.EF.Repository<DataModels.Employee>.CreateRepositoryWithDbContext(unit.DataContext);
			var mappingDecorator = Repo.Mapping.MappingDecorator<DataModels.Employee, DomainModels.Employee>.CreateMappingDecoratorWithMapperAndRepo(mapper, repo);
			var logic = Domain.Logic.EmployeeLogic.CreateLogicWithMappingDecoratorAndLogger(mappingDecorator, logger);


			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterType<CustomLog>().As<ICustomLog>();
			builder.RegisterInstance(unit).As<Repo.Contracts.IUnitOfWork>();
			builder.RegisterType<Repo.EF.Repository<DataModels.Employee>>().As<Repo.Contracts.IRepository<DataModels.Employee>>();
			builder.RegisterType<Repo.Mapping.MappingDecorator<DataModels.Employee, DomainModels.Employee>>().As<Repo.Contracts.IMappingDecorator<DataModels.Employee, DomainModels.Employee>>();
			builder.RegisterType<>
			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
