using System.Collections.Generic;

namespace LPKO_2_1_Cykly
{
    public sealed class Graph
    {
        public Graph(int nodeCount, IEnumerable<Edge> edges)
        {
            NodeCount = nodeCount;
            Edges = edges;
        }

        public int NodeCount { get; }

        public IEnumerable<Edge> Edges { get; }
    }
}
