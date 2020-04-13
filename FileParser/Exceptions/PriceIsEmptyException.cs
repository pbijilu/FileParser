using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Exceptions
{
    public class PriceIsEmptyException : ParsingException
    {
        public PriceIsEmptyException(int stringNum) : base("Price can't be empty", stringNum)
        {

        }
    }
}
