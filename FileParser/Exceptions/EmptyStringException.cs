using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Exceptions
{
    public class EmptyStringException : ParsingException
    {
        public EmptyStringException(int stringNum) : base("Item and price can't be empty", stringNum)
        {

        }
    }
}
