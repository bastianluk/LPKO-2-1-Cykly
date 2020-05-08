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

            var result = FindViolations(graph);

            PrintResult(writer, result);
        }

        private static Result FindViolations(Graph graph)
        {
            // Find the edges to remove all cycles smaller than 4 in size
            foreach (var node in graph.Nodes)
            {
                var newNodes = graph.Nodes.Where(n => n != node);
                var nodesEdges = graph.Edges.Where(e => e.Node1 != node && e.Node2 != node);

                foreach (var VARIABLE in nodesEdges)
                {

                }

                // var rec = FindViolations(new Graph(newNodes, newEdges));
            }

            return new Result(0, new List<Edge>());
        }

        private static void PrintResult(TextWriter writer, Result result)
        {
            writer.WriteLine($"#OUTPUT: {result.WeightSum}");
            foreach (var edge in result.Edges)
            {
                Console.WriteLine($"{edge.Node1.Number} --> {edge.Node1.Number}");
            }

            writer.WriteLine("#OUTPUT END");
        }
    }
}
