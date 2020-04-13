using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Exceptions
{
    public class MissingSeparatorException : ParsingException
    {
        public MissingSeparatorException(int stringNum) : base("Missing separator", stringNum)
        {

        }
    }
}
