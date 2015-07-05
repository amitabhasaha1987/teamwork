using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace teamwork.Utility
{
    public class BitwiseAnd
    {
        public static long[] getFectors(long _and)
        {
            List<long> factorList = new List<long>();
            long reminder = 0, quotient = _and;
            double position = 0.00;
            while (quotient!=1)
            {
                reminder = quotient % 2;
                quotient = quotient / 2;

                if (reminder == 1)
                    factorList.Add((long)Math.Pow(2.00,position));
                position++;
            }
            //add last reminder
            //if (reminder == 1)
             factorList.Add((long)Math.Pow(2.00, position));

            return factorList.ToArray();
        }
    }
}