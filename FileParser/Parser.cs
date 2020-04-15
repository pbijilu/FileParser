using FileParser.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace FileParser
{
    public class Parser
    {
        static readonly string textFileToRead = @"E:\1.csv";
        static readonly string textFileToWrite = @"E:\2.csv";
        static readonly string textFileToLog = @"E:\3.csv";

        public List<string> parsedInfo { get; set; }
        public List<(string, decimal)> validInfo { get; set; }

        public void ReadFromFile()
        {
            using (StreamReader file = new StreamReader(textFileToRead))
            {
                int counter = 0;
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    parsedInfo.Add(line);
                    counter++;
                }
                Console.WriteLine($"{counter} lines read.");
            }
        }
        public void Validate(int stringNum)
        {
            Regex regex = new Regex("^(\".*\"),(\\d*\\.*\\d*)");
            string[] stringPair = new string[2];

            for (int i = stringNum; i < parsedInfo.Count; i++)
            {
                MatchCollection matches = regex.Matches(parsedInfo[i]);

                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        stringPair[0] = match.Groups[1].Value;
                        stringPair[1] = match.Groups[2].Value;
                    }
                }
                else stringPair = parsedInfo[i].Split(",");

                if (stringPair.Length < 2)
                    throw new MissingSeparatorException(i);

                if (stringPair[0] == "" && stringPair[1] == "")
                    throw new EmptyStringException(i);
                else if (stringPair[0] == "")
                    throw new ItemIsEmptyException(i);
                else if (stringPair[1] == "")
                    throw new PriceIsEmptyException(i);

                try
                {
                    validInfo.Add((stringPair[0], Decimal.Parse(stringPair[1])));
                }
                catch (FormatException)
                {
                    throw new PriceIsNotANumberException(i);
                }        
            }
        }
        public void WriteToFile()
        {
            using (StreamWriter file = File.AppendText(textFileToWrite))
            {
                int counter = 0;
                foreach (var info in validInfo)
                {
                    file.WriteLine($"{info.Item1},{info.Item2}");
                    counter++;
                }
                Console.WriteLine($"{counter} lines written.");
            }
        }

        public void Start()
        {
            int stringNum = 0;
            bool isValidated = false;

            try
            {
                ReadFromFile();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("This is a message from Parser.cs. Please stand by while we throw you to Program.Main :)");
                throw;
            }
            catch (IOException)
            {
                Console.WriteLine("This is a message from Parser.cs. Please stand by while we throw you to Program.Main :)");
                throw;
            }

            while (!isValidated)
            {
                try
                {
                    Validate(stringNum);
                    isValidated = true;
                }
                catch (ParsingException e)
                {
                    Console.WriteLine($"{e.Message} at string {e.StringNum + 1}");

                    using (StreamWriter file = File.AppendText(textFileToLog))
                    {
                        file.WriteLine($"{e.Message} at string {e.StringNum + 1}");
                    }

                    stringNum = e.StringNum + 1;
                }
            }

            try
            {
                WriteToFile();
            }
            catch (IOException)
            {
                Console.WriteLine("This is a message from Parser.cs. Please stand by while we throw you to Program.Main :)");
                throw;
            }
        }

        public Parser()
        {
            parsedInfo = new List<string>();
            validInfo = new List<(string, decimal)>();
        }
    }
}
