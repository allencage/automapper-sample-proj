using AutoMapper;
using Log.Logic;
using Logic;
using Repo.EF;
using Repo.Mapping;
using System;
using DataModels = Repo.EF.Models;
using DomainModels = Models;

namespace ConsoleTests
{
	class Program
	{
		static void Main(string[] args)
		{
			var logger = new CustomLog();

			var mapperConfig = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<DataModels.Employee, DomainModels.Employee>().ReverseMap();
				cfg.CreateMap<DataModels.Status, DomainModels.Status>().ReverseMap();
			});
			var mapper = mapperConfig.CreateMapper();
            using(var unit = UnitOfWork.CreateUnitOfWorkWithCustomLog(logger))
            {
                var repo = Repository<DataModels.Employee>.CreateRepositoryWithDbContext(unit.DataContext);
                var mappingDecorator = MappingDecorator<DataModels.Employee, DomainModels.Employee>.CreateMappingDecoratorWithMapperAndRepo(mapper, repo);
                var logic = Logic.Logic.CreateLogicWithMappingDecoratorAndLogger(mappingDecorator, logger);
                var emp = logic.GetEmployee(2);
                logic.DeleteEmployee(emp);
                unit.Commit();
            }
            //Console.Read();
        }

        static DomainModels.Employee CreateEmployee()
        {
            return new DomainModels.Employee
            {
                FirstName = "Alin",
                LastName = "Ciuca",
                Status = new DomainModels.Status
                {
                    IsHired = true,
                    ContractType = "Fulltime"
                }
            };
        }
	}
}
