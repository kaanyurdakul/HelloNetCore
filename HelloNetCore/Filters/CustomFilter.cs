using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloNetCore.Filters
{
    public class CustomFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
           //here is compiled first.
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //here is compiled last
        }
    }
}
