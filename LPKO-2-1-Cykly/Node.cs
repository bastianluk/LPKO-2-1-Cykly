namespace LPKO_2_1_Cykly
{
    public sealed class Node
    {
        public Node(int number)
        {
            Number = number;
        }

        public int Number { get; }

        public override string ToString()
        {
            return Number.ToString();
        }
    }
}
