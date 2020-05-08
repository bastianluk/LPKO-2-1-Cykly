namespace LPKO_2_1_Cykly
{
    public sealed class ResultNode
    {
        public ResultNode(Node node, int partyNumber)
        {
            Node = node;
            PartyNumber = partyNumber;
        }

        public Node Node { get; }

        public int PartyNumber { get; }
    }
}
