using HelloNetCore.Entities;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloNetCore.TagHelpers
{
    [HtmlTargetElement("employee-list")]
    public class EmployeeListTagHelper :TagHelper
    {
        private List<Employee> _employees;
        public EmployeeListTagHelper()
        {
            _employees = new List<Employee> {
                new Employee{Id=1 , FirstName="Aylin", LastName="Aslım", CityId=7 },
                new Employee{Id=2 , FirstName="Cemal", LastName="Süreyya", CityId=34 },
                new Employee{Id=3 , FirstName="Melek", LastName="Subaşı", CityId=34 }
            };
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            //StringBuilder stringBuilder = new StringBuilder();
            string content = "";
            foreach (var item in _employees)
            {
                //stringBuilder.AppendFormat("<h2><a href='/employee/detail/{0}'>{1}</a></h2>", item.Id, item.FirstName);
                content += $"<h2><a href='/employee/detail/{item.Id}'>{item.FirstName}</a></h2>";
            }

            //output.Content.SetHtmlContent(stringBuilder.toString());
            output.Content.SetHtmlContent(content);
            base.Process(context, output);  
        }

    }
}
