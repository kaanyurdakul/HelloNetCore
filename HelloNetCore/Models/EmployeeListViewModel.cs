using HelloNetCore.Entities;
using System.Collections.Generic;

namespace HelloNetCore.Models
{
    public class EmployeeListViewModel
    {
        public List<Employee> Employees { get; set; }
        public List<string> Cities { get; set; }
    }
}