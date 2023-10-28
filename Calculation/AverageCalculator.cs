using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LineagesComparison.Calculation
{
    internal static class AverageCalculator
    {
        public static string Execute(string filePath, SamplesPerLineages samplesPerLineages)
        {
            StringBuilder builder = new StringBuilder();
            
            List<string> fileLines = File.ReadAllLines(filePath).ToList();
            List<float[]> comparisonValues = new List<float[]>();
            string[] samplesOrder = fileLines.First().Split(',').Skip(1).ToArray();

            foreach (string fileLine in fileLines.Skip(1))
            {
                List<string> values = fileLine
                    .Split(',')
                    .Skip(1)
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .ToList();

                values.Add("0");

                comparisonValues
                    .Add(values
                        .Select(x => float.Parse(x))
                        .ToArray());
            }

            List<SampleComparison> sampleComparisons = new List<SampleComparison>(15000);

            for (int i = 0; i < comparisonValues.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    SampleComparison comparison = new SampleComparison();

                    comparison.Sample1 = samplesOrder[i];
                    comparison.Sample2 = samplesOrder[j];
                    comparison.Lineage1 = samplesPerLineages.LineageFor(samplesOrder[i]);
                    comparison.Lineage2 = samplesPerLineages.LineageFor(samplesOrder[j]);
                    comparison.Value = comparisonValues[i][j];

                    sampleComparisons.Add(comparison);
                }
            }

            string[] lineages = samplesPerLineages.Lineages();

            builder.AppendLine("Внутри линий");

            foreach (string lineage in lineages)
            {
                SampleComparison[] toCount = sampleComparisons
                    .Where(x => x.Lineage1 == lineage && x.Lineage2 == lineage)
                    .ToArray();

                float average = toCount.Sum(x => x.Value) / toCount.Length;
                float percentage = (float)Math.Round(average * 100f, 2);

                builder.AppendLine($"{lineage}, среднее = {average}, процент = {percentage}");
            }

            builder.AppendLine("Между линиями");


            for (int i = 0; i < lineages.Length; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    string firstLineage = lineages[i];
                    string secondLineage = lineages[j];

                    SampleComparison[] toCount = sampleComparisons
                        .Where(x => x.Lineage1 == firstLineage && x.Lineage2 == secondLineage
                            || x.Lineage2 == firstLineage && x.Lineage1 == secondLineage)
                        .ToArray();

                    float average = toCount.Sum(x => x.Value) / toCount.Length;
                    float percentage = (float)Math.Round(average * 100f, 2);

                    builder.AppendLine($"Линии {firstLineage} и {secondLineage}, среднее = {average}, процент = {percentage}");
                }
            }

            return builder.ToString();
        }
    }
}
