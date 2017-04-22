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
				cfg.CreateMap<DataModels.Employee, DomainModels.EmployeeDomainModel>().ReverseMap();
				cfg.CreateMap<DataModels.Status, DomainModels.Status>().ReverseMap();
			});
			var mapper = mapperConfig.CreateMapper();
			using (var unit = UnitOfWork.CreateUnitOfWorkWithCustomLog(logger))
			{
				var repo = Repository<DataModels.Employee>.CreateRepositoryWithDbContext(unit.DataContext);
				var mappingDecorator = MappingDecorator<DataModels.Employee, DomainModels.EmployeeDomainModel>.CreateMappingDecoratorWithMapperAndRepo(mapper, repo);
				//var logic = EmployeeLogic.CreateLogicWithMappingDecoratorAndLogger(mappingDecorator, logger);
				//logic.AddEmployee(CreateEmployee());
				//logic.AddEmployee(CreateEmployee());
				//logic.AddEmployee(CreateEmployee());
				unit.Commit();
			}

			//using(var unit = new UnitOfWork())
			//{
			//	var results = unit.Repo.GetAll();
			//}
			//Console.Read();
		}

        static DomainModels.EmployeeDomainModel CreateEmployee()
        {
            return new DomainModels.EmployeeDomainModel
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
