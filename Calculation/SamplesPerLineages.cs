using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LineagesComparison.Calculation
{
    internal class SamplesPerLineages
    {
        private Dictionary<string, string[]> _samplesPerLineage = new Dictionary<string, string[]>();

        public SamplesPerLineages(Dictionary<string, string[]> output)
        {
            _samplesPerLineage = output;
        }

        public string LineageFor(string forSample) =>
            _samplesPerLineage
                .First(x => x.Value.Any(y => forSample.ToLower().Contains(y.ToLower())))
                .Key;

        internal string[] Samples() =>
            _samplesPerLineage
                .SelectMany(x => x.Value)
                .ToArray();

        internal string[] Lineages() =>
            _samplesPerLineage.Keys.OrderBy(x => x).ToArray();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            foreach (string lineage in _samplesPerLineage.Keys)
            {
                builder.Append($"{lineage}: ");

                foreach (string value in _samplesPerLineage[lineage])
                    builder.Append($"{value} ");

                builder.AppendLine("");
                builder.AppendLine("");
            }

            return builder.ToString();
        }
    }
}
