using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPKO_2_1_Cykly
{
    public sealed class GlpkProgramGenerator
    {
        public static GlpkProgram PrepareProgram(Graph graph)
        {
            var lines = new List<string>();

            var sets = GetSetLines(graph.NodeCount);
            lines.AddRange(sets);

            var parameters = GetParamLines();
            lines.AddRange(parameters);

            var variables = GetVariableLines();
            lines.AddRange(variables);

            var function = GetFunctionLine();
            lines.AddRange(function);

            var conditions = GetConditionLines();
            lines.AddRange(conditions);

            var outputPrinter = GetOutputPrinter();
            lines.AddRange(outputPrinter);

            var data = GetDataLines(graph.Edges);
            lines.AddRange(data);

            return new GlpkProgram(lines);
        }

        /// <summary>
        /// Generates the lines representing the different sets (Nodes and Edges) needed in this linear program.
        /// </summary>
        private static IEnumerable<string> GetSetLines(int nodeCount)   
        {
            return new List<string>
            {
                $"set Nodes := 0..{nodeCount-1};",
                "set Edges, within Nodes cross Nodes;"
            };
        }

        /// <summary>
        /// Generates the line representing parameters (weight of an Edge) of the linear program.
        /// </summary>
        private static IEnumerable<string> GetParamLines()
        {
            return new List<string> { "param weight{(i,j) in Edges};" };
        }

        /// <summary>
        /// Generates the line representing the variable (indicator isRemoved) of the linear program.
        /// </summary>
        private static IEnumerable<string> GetVariableLines()
        {
            return new List<string> { "var isRemoved{(i,j) in Edges}, binary;" };
        }

        /// <summary>
        /// Generates the line representing the function that will be minimized.
        /// </summary>
        private static IEnumerable<string> GetFunctionLine()
        {
            return new List<string>{ "minimize total: sum{(i,j) in Edges} weight[i,j] * isRemoved[i,j];" };
        }

        /// <summary>
        /// Generates the lines representing the two conditions needed for this linear program - they "ban" cycles of lenght 4 or 3 in the final graph.
        /// </summary>
        private static IEnumerable<string> GetConditionLines()
        {
            return new List<string>
            {
                "s.t. condition_circle4{i in Nodes, j in Nodes, k in Nodes, l in Nodes: not(i == j or j == k or k == l or l == i)}:",
                "  ( if ((i,j) in Edges and (j,k) in Edges and (k,l) in Edges and (l,i) in Edges) then (isRemoved[i,j] + isRemoved[j,k] + isRemoved[k,l] + isRemoved[l,i]) else 1 ) >= 1;",
                "s.t. condition_circle3{i in Nodes, j in Nodes, k in Nodes: not(i == j or j == k or k == i)}:",
                "  ( if ((i,j) in Edges and (j,k) in Edges and (k,i) in Edges) then (isRemoved[i,j] + isRemoved[j,k] + isRemoved[k,i]) else 1 ) >= 1;"
            };
        }
        
        /// <summary>
        /// Generates the lines that represent the correct output format of the linear program.
        /// </summary>
        private static IEnumerable<string> GetOutputPrinter()
        {
            return new List<string>
            {
                "solve;",
                "printf \"#OUTPUT: %d\\n\", sum{(i,j) in Edges} weight[i,j] * isRemoved[i,j];",
                "for {(i,j) in Edges: i != j}",
                "{",
                "  printf (if isRemoved[i,j] = 1 then \"%d --> %d\\n\" else \"\"), i, j;",
                "}",
                "printf \"#OUTPUT END\\n\";",
            };
        }

        /// <summary>
        /// Generates the lines that represent the data input for the linear program.
        /// </summary>
        private static IEnumerable<string> GetDataLines(IEnumerable<Edge> edges)
        {
            var data = new List<string>
            {
                "data;"
            };

            var weightBuilder = new StringBuilder();
            weightBuilder.Append("param : Edges : weight := ");
            var first = edges.First();
            weightBuilder.AppendLine($"{first.Node1} {first.Node2} {first.Weight}");
            foreach (var edge in edges.Skip(1))
            {
                weightBuilder.AppendLine($"                          {edge.Node1} {edge.Node2} {edge.Weight}");
            }
            weightBuilder.Remove(weightBuilder.Length - 2, 2); //Remove the trailing "\r\n"
            weightBuilder.Append(";");
            data.Add(weightBuilder.ToString());

            data.Add("end;");
            return data;
        }
    }
}