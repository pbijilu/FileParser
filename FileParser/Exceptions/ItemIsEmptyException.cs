using System;
using System.Collections.Generic;
using System.Text;

namespace FileParser.Exceptions
{
    public class ItemIsEmptyException : ParsingException
    {
        public ItemIsEmptyException(int stringNum) : base("Item can't be empty", stringNum)
        {
        }
    }
}
