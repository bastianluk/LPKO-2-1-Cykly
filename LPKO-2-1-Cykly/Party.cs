using System.Collections.Generic;

namespace LPKO_2_1_Cykly
{
    public sealed class Party
    {
        public Party(int number, IEnumerable<Node> members)
        {
            Number = number;
            Members = members;
        }

        public int Number { get; }

        public IEnumerable<Node> Members { get; }
    }
}
