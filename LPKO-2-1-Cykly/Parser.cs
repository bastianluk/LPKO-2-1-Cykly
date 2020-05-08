using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;

namespace LPKO_2_1_Cykly
{
    public sealed class Parser
    {
        public static Graph ReadInput(TextReader reader)
        {
            var graphInput = reader.ReadLine();
            var delimiters = new char[] { ' ' };
            var splitGraph = graphInput.Trim().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);

            var isNodeCount = int.TryParse(splitGraph[2], out var nodeCount);
            var isEdgeCount = int.TryParse(splitGraph[3].TrimEnd(':'), out var edgeCount);

            if (!isNodeCount || !isEdgeCount)
            {
                throw new ArgumentException($"Invalid arguments on {nameof(graphInput)}: {graphInput}");
            }

            var nodes = new ConcurrentDictionary<int, Node>();
            var edges = new List<Edge>(edgeCount);

            for (int i = 0; i < edgeCount; i++)
            {
                var edgeInputLine = reader.ReadLine();
                var splitEdgeInput = edgeInputLine.Trim().Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                var isNode1 = int.TryParse(splitEdgeInput[0], out var node1Number);
                var isNode2 = int.TryParse(splitEdgeInput[2], out var node2Number);
                var isWeight = int.TryParse(splitEdgeInput[4].TrimEnd(')'), out var weight);

                if (!isNode1 || !isNode2 || !isWeight || node1Number >= nodeCount || node2Number >= nodeCount || weight < 1)
                {
                    throw new ArgumentException($"Invalid arguments on {nameof(edgeInputLine)}: {edgeInputLine}");
                }

                var node1 = nodes.GetOrAdd(node1Number, number => new Node(number));
                var node2 = nodes.GetOrAdd(node2Number, number => new Node(number));

                var edge = new Edge(node1, node2, weight);
                edges.Add(edge);
            }

            return new Graph(nodes.Values.ToList(), edges);
        }
    }
}
