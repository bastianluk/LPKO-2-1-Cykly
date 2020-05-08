using System.Collections.Generic;

namespace LPKO_2_1_Cykly
{
    public sealed class Result
    {
        public Result(int numberOfParties, IEnumerable<ResultNode> nodes)
        {
            NumberOfParties = numberOfParties;
            Nodes = nodes;
        }

        public int NumberOfParties { get; }

        public IEnumerable<ResultNode> Nodes { get; }
    }
}
