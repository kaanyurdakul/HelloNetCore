using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloNetCore.Entities;
using HelloNetCore.Models;
using Microsoft.AspNetCore.Mvc;

namespace HelloNetCore.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Hello sweety";
        }
        public ViewResult Index2()
        {
            return View();
        }
        public ViewResult Index3()
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1 , FirstName="Aylin", LastName="Aslım", CityId=7 },
                new Employee{Id=2 , FirstName="Cemal", LastName="Süreyya", CityId=34 },
                new Employee{Id=3 , FirstName="Melek", LastName="Subaşı", CityId=34 }
            };

            List<string> cities = new List<string> { "Ankara", "Antalya" };

            var model = new EmployeeListViewModel
            {
                Employees = employees,
                Cities = cities
            };
            return View(model);
        }
        public IActionResult Index4()
        {
            return StatusCode(400); //Result: Bad Request
            //return StatusCode(404);
        }
        public StatusCodeResult Index5()
        {
            return BadRequest(); // Result: Same as above result.
            //return NotFound();
        }
        public RedirectResult Index6()
        {
            return Redirect("/home/Index3"); // Redirecting to Index3
        }
        public ActionResult Index7()
        {
            return RedirectToAction("Index3"); //Same as above result
        }
        public ActionResult Index8()
        {
            return RedirectToRoute("first"); // Work with this route name;
        }
        public JsonResult Index9()
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1 , FirstName="Aylin", LastName="Aslım", CityId=7 },
                new Employee{Id=2 , FirstName="Cemal", LastName="Süreyya", CityId=34 },
                new Employee{Id=3 , FirstName="Melek", LastName="Subaşı", CityId=34 }
            };
            return Json(employees);
        }
        public ViewResult RazorDemo()
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1 , FirstName="Aylin", LastName="Aslım", CityId=7 },
                new Employee{Id=2 , FirstName="Cemal", LastName="Süreyya", CityId=34 },
                new Employee{Id=3 , FirstName="Melek", LastName="Subaşı", CityId=34 }
            };

            List<string> cities = new List<string> { "Ankara", "İstanbul", "Eskişehir", "İzmir", "Antalya" };

            var model = new EmployeeListViewModel
            {
                Employees = employees,
                Cities = cities
            };
            return View(model);
        }

        public JsonResult Index10(string key)
        {
            List<Employee> employees = new List<Employee> {
                new Employee{Id=1 , FirstName="Aylin", LastName="Aslım", CityId=7 },
                new Employee{Id=2 , FirstName="Cemal", LastName="Süreyya", CityId=34 },
                new Employee{Id=3 , FirstName="Melek", LastName="Subaşı", CityId=34 }
            };
            var result = string.IsNullOrEmpty(key) ? employees : employees.Where(x => x.FirstName.ToLower().Contains(key));

            //same shit different smell :)

            //IEnumerable<Employee> result;
            //if (string.IsNullOrEmpty(key))
            //    result = employees;
            //else
            //    result = employees.Where(x => x.FirstName.ToLower().Contains(key));
   
            return Json(result);
        }

        public ViewResult EmployeeForm()
        {
            return View();
        }

        public string Index11(int Id)
        {
            return Id.ToString();
        }
        

    }
}