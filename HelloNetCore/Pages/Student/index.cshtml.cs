using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloNetCore.Entities;
using HelloNetCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HelloNetCore
{
    public class indexModel : PageModel
    {
        private readonly SchoolContext _context;

        public indexModel(SchoolContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> Students { get; set; }


        public void OnGet(string search)
        {
            Students = string.IsNullOrEmpty(search)
                ? _context.Students
                : Students = _context.Students.Where(x => x.Firstname.ToLower().Contains(search));
           
        }


        [BindProperty]
        public Student Student { get; set; }
        public IActionResult OnPost()
        {
            _context.Students.Add(Student);
            _context.SaveChanges();
            return RedirectToPage("/Student/index");
        }
    }
}