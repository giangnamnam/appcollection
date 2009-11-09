using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace WindowsFormsApplication2
{
    class CustomerB
    {
        public static explicit operator CustomerA(CustomerB value)
        {
            return new CustomerA(12);
        }

        public static explicit operator CustomerB(CustomerA value)
        {
            return new CustomerB();
        }
    }
}
