using FileParser.Exceptions;
using System;
using System.IO;

namespace FileParser
{
    class Program
    {
        static readonly string textFileToLog = @"E:\4.csv";

        static void Main(string[] args)
        {
            Parser parser = new Parser();
            try
            {
                parser.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine($"This is a message from Program.Main. If you got here, then the exception message is logging in separate file :) Also you can see it right now: {e.Message}");
                using (StreamWriter file = File.AppendText(textFileToLog))
                {
                    file.WriteLine(e.Message);
                }
            }
        }
    }
}
