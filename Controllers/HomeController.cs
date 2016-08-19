using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CachingDemo.Web.Models.Data;
using System.Runtime.Caching;

namespace CachingDemo.Web.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public IVehicleRepository Repository { get; set; }

		public HomeController()
			: this(new VehicleRepository())
		{
		}

		public HomeController(IVehicleRepository repository)
		{
			this.Repository = repository;
		}

		public ActionResult Index()
		{
			ViewData["Message"] = "Welcome to my caching demo!";

			var data = from v in Repository.GetVehicles()
					   orderby v.Name
					   select v;

			return View(data);
		}

		[HttpPost]
		public ActionResult Index(FormCollection form)
		{
			Repository.ClearCache();
			return RedirectToAction("Index");
		}

		public ActionResult Edit(Guid id)
		{
			var vehicle = Repository.GetVehicles().Single(v => v.Id == id);

			return View(vehicle);
		}

		[HttpPost]
		public ActionResult Edit(Vehicle vehicle)
		{
			Repository.Update(vehicle);
			Repository.SaveChanges();

			return RedirectToAction("Index");
		}

		public ActionResult Create()
		{
			var newVehicle = new Vehicle() { Id = Guid.NewGuid() };

			return View(newVehicle);
		}

		[HttpPost]
		public ActionResult Create(Vehicle vehicle)
		{
			Repository.Insert(vehicle);
			Repository.SaveChanges();

			return RedirectToAction("Index");
		}

		public ActionResult About()
		{
			return View();
		}
	}
}
