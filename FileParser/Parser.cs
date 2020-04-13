using FileParser.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileParser
{
    public class Parser
    {
        static readonly string textFileToRead = @"E:\1.csv";
        static readonly string textFileToWrite = @"E:\2.csv";
        static readonly string textFileToLog = @"E:\3.csv";

        public List<string> parsedInfo { get; set; }
        public List<(string, decimal)> validInfo { get; set; }
        public int StringNum { get; set; }
        public bool IsValidated { get; set; }

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
            for (int i = stringNum; i < parsedInfo.Count; i++)
            {
                string[] stringPair = parsedInfo[i].Split(",");

                if (stringPair.Length != 2)
                    throw new MissingSeparatorException(i);
                else {
                    if (stringPair[0] == "" && stringPair[1] == "")
                        throw new EmptyStringException(i);
                    else if (stringPair[0] == "")
                        throw new ItemIsEmptyException(i);
                    else if (stringPair[1] == "")
                        throw new PriceIsEmptyException(i);
                    else
                    {
                        try
                        {
                            validInfo.Add((stringPair[0], Decimal.Parse(stringPair[1])));
                        }
                        catch (FormatException e)
                        {
                            throw new PriceIsNotANumberException(i);
                        }
                    }
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
                    string writableInfo = $"{info.Item1},{info.Item2}";
                    file.WriteLine(writableInfo);
                    counter++;
                }
                Console.WriteLine($"{counter} lines written.");
            }
        }

        public void Start()
        {
            try
            {
                ReadFromFile();
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("This is a message from Parser.cs. Please stand by while we throw you to Program.Main :)");
                throw;
            }
            catch (IOException e)
            {
                Console.WriteLine("This is a message from Parser.cs. Please stand by while we throw you to Program.Main :)");
                throw;
            }

            while (!IsValidated)
            {
                try
                {
                    Validate(StringNum);
                    IsValidated = true;
                }
                catch (ParsingException e)
                {
                    Console.WriteLine($"{e.Message} at string {e.StringNum + 1}");

                    using (StreamWriter file = File.AppendText(textFileToLog))
                    {
                        file.WriteLine($"{e.Message} at string {e.StringNum + 1}");
                    }

                    StringNum = e.StringNum + 1;
                }
            }

            try
            {
                WriteToFile();
            }
            catch (IOException e)
            {
                Console.WriteLine("This is a message from Parser.cs. Please stand by while we throw you to Program.Main :)");
                throw;
            }
        }

        public Parser()
        {
            parsedInfo = new List<string>();
            validInfo = new List<(string, decimal)>();
            IsValidated = false;
            StringNum = 0;
        }
    }
}
