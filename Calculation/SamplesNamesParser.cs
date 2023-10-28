using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LineagesComparison.Calculation
{
    internal static class SamplesPerLineagesParser
    {
        public static SamplesPerLineages Execute(string fileName)
        {
            Dictionary<string, string[]> output = new Dictionary<string, string[]>();
            string[] fileLines = File.ReadAllLines(fileName);

            foreach (string line in fileLines)
            {
                string[] split = line.Split(',');
                output.Add(
                    split[0], 
                    split
                        .Skip(1)
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .ToArray());
            }

            return new SamplesPerLineages(output);
        }
    }
}
