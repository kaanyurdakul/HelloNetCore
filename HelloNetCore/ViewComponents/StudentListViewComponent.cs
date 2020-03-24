using HelloNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloNetCore.ViewComponents
{
    public class StudentListViewComponent : ViewComponent
    {
        private SchoolContext _context;

        public StudentListViewComponent(SchoolContext context)
        {
            _context = context;
        }

        public ViewViewComponentResult Invoke(string filter)
        {
            filter = HttpContext.Request.Query["filter"]; // getting filter data from query string

            return View(new StudentListViewModel
            {
                Students = _context.Students.Where(s => s.Firstname.ToLower().Contains(filter)).ToList()
            });
        }
    }
}
