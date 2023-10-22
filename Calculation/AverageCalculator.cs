using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineagesComparison.Calculation
{
    internal static class AverageCalculator
    {
        public static string Execute(string filePath)
        {
            StringBuilder builder = new StringBuilder();
            string[] ParseToSamplesArray(string input) =>
                input
                .Split(new char[] { ' ' })
                .ToArray();

            Dictionary<string, string[]> _lineagesForSamples = new Dictionary<string, string[]>()
            {
                ["L0"] = ParseToSamplesArray("ArmAMD81 TseyAMD1 TseyAMD4 TseyAMD6 TseyAMD7 TseyAMD8 TseyAMD11 TseyAMD13 WCaSAV251 WCaSAV256"),
                ["L1"] = ParseToSamplesArray("CCaAMD25 ArmAMD77 ArmAMD78 ArmAMD79 CriAMD123 CriAMD119 CriAMD120 CriAMD122 CriAMD111 CriAMD112 GeoAMD35 GeoAMD31 GeoAMD39 GeoAMD40 GeoAMD41 KarSAV301 KazSDA50 KazSDA51 KazSDA40 KazSDA41 KazSDA60 StKrAMD149 StKrAMD151 StKrAMD150 ASTRAMD160 ASTRAMD159 ASTRAMD161 DAGAMD171 DAGAMD169 DAGAMD170 DAGAMD175 DAGAMD176 DAGAMD179 DAGAMD180 DAGAMD181 DAGAMD183 DAGAMD182 RostAMD187 RostAMD189 RostAMD186 RostAMD190 RostAMD191 RostAMD192 StavAMD195 KrKrAMD200 KrKrAMD202 KrKrAMD203 WCaSAV277 HovSAV21 HovSAV22 VorSAV67 HovSAV23 OrehSAV10 OrehSAV9 OrehSAV26 HovSAV24 PrVeSAV18 HovSAV20"),
                ["L2"] = ParseToSamplesArray("BelAMD209 BelAMD205 BelAMD206 BelAMD207 KarSAV305 KarSAV314 KarSAV315 KarSAV336 KarSAV338 VladAMD213 BalSAV181 BalSAV182 ShakSAV95 ShakSAV105 VDNHSAV12 BalSAV180 BitSAV172"),
                ["L3"] = ParseToSamplesArray("CriAMD131 AthSAV226 AthSAV227"),
                ["L4"] = ParseToSamplesArray("KBRAMD24 KarSAV303 WCaSAV252 WCaSAV253 WCaSAV272 WCaSAV275 LosiSAV206 BitSAV167 ShakSAV114 ShakSAV116 ShakSAV115 VorSAV66 VorSAV70"),
                ["L5"] = ParseToSamplesArray("AzeAMD19 AzeAMD20 AzeAMD22 AzeAMD23 AzeAMD21 Iran"),
                ["L6"] = ParseToSamplesArray("CriAMD89 CriAMD91 CriAMD92 CriAMD90"),
                ["L7"] = ParseToSamplesArray("KBRAMD27 KBRAMD28 KBRAMD30"),
                ["L8"] = ParseToSamplesArray("GeoAMD32 GeoAMD34 GeoAMD38"),
                ["L9"] = ParseToSamplesArray("SocSAV406"),
                ["L10"] = ParseToSamplesArray("HRPn1"),
            };

            string LineageForSample(string sample) =>
                _lineagesForSamples.First(x => x.Value.Any(y => sample.Contains(y))).Key;

            List<string> fileLines = File.ReadAllLines(filePath).ToList();

            string[] samplesNames = fileLines.First().Split(',').Skip(1).ToArray();
            fileLines.RemoveAt(0);

            List<float[]> comparisonValues = new List<float[]>();

            foreach (string fileLine in fileLines)
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

            List<SampleComparison> sampleComparisons = new List<SampleComparison>();

            for (int i = 0; i < comparisonValues.Count; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    SampleComparison comparison = new SampleComparison();

                    comparison.Sample1 = samplesNames[i];
                    comparison.Sample2 = samplesNames[j];
                    comparison.Lineage1 = LineageForSample(samplesNames[i]);
                    comparison.Lineage2 = LineageForSample(samplesNames[j]);
                    comparison.Value = comparisonValues[i][j];

                    sampleComparisons.Add(comparison);
                }
            }

            string[] lineages = _lineagesForSamples.Keys.OrderBy(x => x).ToArray();

            builder.AppendLine("Внутри линий");

            foreach (string lineage in lineages)
            {
                SampleComparison[] toCount = sampleComparisons
                    .Where(x => x.Lineage1 == lineage && x.Lineage2 == lineage)
                    .ToArray();

                float average = (float)Math.Round(toCount.Sum(x => x.Value) / toCount.Length, 2);
                float percentage = average * 100f;

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

                    float average = (float)Math.Round(toCount.Sum(x => x.Value) / toCount.Length, 2);
                    float percentage = average * 100f;

                    builder.AppendLine($"Линии {firstLineage} и {secondLineage}, среднее = {average}, процент = {percentage}");
                }
            }

            return builder.ToString();
        }
    }
}
