namespace LPKO_2_1_Cykly
{
    public sealed class Edge
    {
        public Edge(Node node1, Node node2, int weight)
        {
            Node1 = node1;
            Node2 = node2;
            Weight = weight;
        }

        public Node Node1 { get; }

        public Node Node2 { get; }

        public int Weight { get; }
    }
}
