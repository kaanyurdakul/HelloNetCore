using System;
using HelloNetCore.Entities;
using HelloNetCore.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloNetCore.Controllers
{
    public class SessionDemoController : Controller
    {

        public string Index()
        {
            HttpContext.Session.SetInt32("age", 32);
            HttpContext.Session.SetString("name", "Engin");
            HttpContext.Session.SetObject("student",
                new Student 
                    { Email = "kaanyrdkl@gmail.com", Firstname = "Kaan", Lastname = "Yurdakul", Id = 1 }
            );

            return "Session was created";
        }


        public string GetSessions()
        {
            return String.Format("Hello {0}, you are {1}. Student is {2}", HttpContext.Session.GetString("name"),
                HttpContext.Session.GetInt32("age"), HttpContext.Session.GetObject<Student>("student").Firstname);

        }

    }
}