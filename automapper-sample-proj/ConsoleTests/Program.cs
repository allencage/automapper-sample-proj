using AutoMapper;
using Log.Logic;
using Repo.EF;
using Repo.Mapping;
using Domain.Logic;
using DataModels = Repo.EF.Models;
using DomainModels = Domain.Models;

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
                var logic = Logic.CreateLogicWithMappingDecoratorAndLogger(mappingDecorator, logger);
                logic.AddEmployee(CreateEmployee());
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
