using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LPKO_2_1_Cykly
{
    public sealed class Program
    {
        private static void Main(string[] args)
        {
            var reader = Console.In;
            var writer = Console.Out;
            var graph = Parser.ReadInput(reader);

            var result = FindParties(graph);

            PrintResult(writer, result);
        }

        private static Result FindParties(Graph graph)
        {
            foreach (var node in graph.Nodes)
            {
                var newNodes = graph.Nodes.Where(n => n != node);
                var nodesEdges = graph.Edges.Where(e => e.Node1 != node && e.Node2 != node);

                foreach (var VARIABLE in nodesEdges)
                {

                }

                var rec = FindParties(new Graph(newNodes, newEdges));
            }

            return new Result(0, new List<ResultNode>());
        }

        private static void PrintResult(TextWriter writer, Result result)
        {
            writer.WriteLine($"#OUTPUT: {result.NumberOfParties}");
            foreach (var resultNode in result.Nodes)
            {
                Console.WriteLine($"v_{resultNode.Node.Number}: {resultNode.PartyNumber}");
            }

            writer.WriteLine("#OUTPUT END");
        }
    }
}
