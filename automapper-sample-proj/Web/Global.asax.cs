﻿using System.Web.Mvc;
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

			var builder = new ContainerBuilder();
			builder.RegisterControllers(typeof(MvcApplication).Assembly);
			builder.RegisterInstance(mapper).As<IMapper>();
			builder.RegisterType<CustomLog>().As<ICustomLog>();
			builder.RegisterType<EmployeeRepository>().As<IRepository<DataModels.Employee>>();

			builder.RegisterType<MappingDecorator<DataModels.Employee, DomainModels.EmployeeDomainModel>>().As<IMappingDecorator<DataModels.Employee, DomainModels.EmployeeDomainModel>>();

			builder.RegisterType<EmployeeLogic>().As<IEmployeeLogic<DomainModels.EmployeeDomainModel>>();
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
