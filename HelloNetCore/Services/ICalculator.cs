using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloNetCore.Services
{
    public interface ICalculator
    {
        decimal Calculate(decimal amount); //default access modifier is private.
    }
}
