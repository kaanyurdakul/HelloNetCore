using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloNetCore.Services
{
    public class Calculator8 : ICalculator
    {
        public decimal Calculate(decimal amount)
        {
            return amount * 108 / 100;
        }
    }
}
