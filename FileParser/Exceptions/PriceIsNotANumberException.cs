using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Exceptions
{
    public class PriceIsNotANumberException : ParsingException
    {
        public PriceIsNotANumberException(int stringNum) : base("Price is not in a number format or spaces are present", stringNum)
        {

        }
    }
}
