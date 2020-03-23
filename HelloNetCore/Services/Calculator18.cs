using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloNetCore.Services
{
    public class Calculator18 : ICalculator
    {
        public decimal Calculate(decimal amount)
        {
            return amount * 118 / 100;
        }
    }
}
