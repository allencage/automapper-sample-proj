using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using DataModels = Repo.EF.Models;
using DomainModels = Domain.Models;
using Autofac.Integration.Mvc;
using Log.Logic;
using Log.Contracts;
using Domain.Logic;
using Domain.Contracts;
using Repo.EF;
using AutoMapper;
using System.Data.Entity;
using Repo.Contracts;
using Repo.Mapping;

namespace Web
{
	public class MvcApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{

			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<DataModels.Employee, DomainModels.EmployeeDomainModel>().ReverseMap();
				cfg.CreateMap<DataModels.Status, DomainModels.Status>().ReverseMap();
			});
			var mapper = mapperConfig.CreateMapper();

			//var logger = new CustomLog();
			//var unit = Repo.EF.UnitOfWork.CreateUnitOfWorkWithCustomLog(logger);
			//var repo = Repo.EF.Repository<DataModels.Employee>.CreateRepositoryWithDbContext(unit.DataContext);
			//var mappingDecorator = Repo.Mapping.MappingDecorator<DataModels.Employee, DomainModels.Employee>.CreateMappingDecoratorWithMapperAndRepo(mapper, repo);
			//var logic = Domain.Logic.EmployeeLogic.CreateLogicWithMappingDecoratorAndLogger(mappingDecorator, logger);


			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterInstance(mapper).As<IMapper>();
			builder.RegisterType<CustomLog>().As<ICustomLog>();
			builder.RegisterType<Repository<DataModels.Employee>>().As<IRepository<DataModels.Employee>>();

			builder.RegisterType<MappingDecorator<DataModels.Employee, DomainModels.EmployeeDomainModel>>().As<IMappingDecorator<DataModels.Employee, DomainModels.EmployeeDomainModel>>();

			builder.RegisterType<EmployeeLogic>().As<IEmployeeLogic>();
			builder.RegisterType<DataContext>().As<DbContext>();
			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
