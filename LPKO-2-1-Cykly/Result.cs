using System.Collections.Generic;

namespace LPKO_2_1_Cykly
{
    public sealed class Result
    {
        public Result(int weightSum, IEnumerable<Edge> edges)
        {
            WeightSum = weightSum;
            Edges = edges;
        }

        public int WeightSum { get; }

        public IEnumerable<Edge> Edges { get; }
    }
}
