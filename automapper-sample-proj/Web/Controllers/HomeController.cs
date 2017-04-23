using Domain.Contracts;
using Repo.EF;
using System.Web.Mvc;
using DomainModels = Domain.Models;

namespace Web.Controllers
{
	public class HomeController : Controller
	{
		IEmployeeLogic<DomainModels.EmployeeDomainModel> _logic;

		public HomeController(IEmployeeLogic<DomainModels.EmployeeDomainModel> logic)
		{
			_logic = logic;
		}

		public ActionResult Index()
		{

			var results = _logic.GetAllEmployees();
			return View(results);
		}

		public ActionResult Edit(long id = 0)
		{
			using(var unit = new UnitOfWork())
			{
				var employee = unit.Repo.Get(id);
				if (employee == null)
					return HttpNotFound();
				return View(employee);
			}
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Repo.EF.Models.Employee employee)
		{
			if (ModelState.IsValid)
			{
				using(var unit = new UnitOfWork())
				{
					unit.Repo.Update(employee);
					unit.Commit();
				}
			}
			return RedirectToAction("Index");
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}