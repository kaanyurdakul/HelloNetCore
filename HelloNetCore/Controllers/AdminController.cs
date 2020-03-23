using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HelloNetCore.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        [Route("")]
        [Route("save")]
        [Route("~/save")] // e.g:"localhost:5000/save"
        public String Save()
        {
            return "Saved";
        }
        [Route("delete/{id?}")]
        public String Delete(int id)
        {
            return String.Format("Deleted :{0}",id);
        }
        [Route("update")]
        public String Update()
        {
            return "Updated";
        }
    }
}