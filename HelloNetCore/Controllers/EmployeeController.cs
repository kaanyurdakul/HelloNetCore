using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloNetCore.Entities;
using HelloNetCore.Models;
using HelloNetCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelloNetCore.Controllers
{
    public class EmployeeController : Controller
    {
        private ICalculator _calculator;
        public EmployeeController(ICalculator calculator)
        {
            _calculator = calculator;
        }
        public IActionResult Add()
        {
            EmployeeAddViewModel employeeAddViewModel = new EmployeeAddViewModel
            {
                Employee = new Employee(),
                Cities = new List<SelectListItem>
                {
                    new SelectListItem{Text="Ankara", Value="6"},
                    new SelectListItem{Text="Antalya", Value="7"},
                    new SelectListItem{Text="Adana", Value="1"}
                }
            };
            return View(employeeAddViewModel);
        }
        [HttpPost]
        public IActionResult Add(Employee employee)
        {
            return View();
        }
        public string Calculator()
        {
            return _calculator.Calculate(100).ToString();
        }
    }
}