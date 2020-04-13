using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Exceptions
{
    public class ParsingException : Exception
    {
        public int StringNum { get; }

        public ParsingException(string message, int stringNum) : base($"Parsing exception! {message}")
        {
            StringNum = stringNum;
        }
    }
}
