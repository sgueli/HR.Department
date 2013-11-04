using HR.Department.DAL;
using HR.Department.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HR.Department.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeePersister persister = new EmployeePersister { File = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/Employee.js") };

        public ActionResult Index()
        {
            var employees = persister.Get();
            return View(employees);
        }

        public ActionResult Create()
        {
            var employee = new Employee { };
            return View(employee);
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            persister.Set(employee);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var employee = persister.Get().Where(e => e.Id == id).SingleOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(Employee e)
        {
            persister.Set(e);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(Employee e)
        {
            persister.Delete(e);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var employee = persister.Get().Where(e => e.Id == id).SingleOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Details(Employee e)
        {
            persister.Set(e);
            return RedirectToAction("Index");
        }


    }
}
