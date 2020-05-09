using System.Collections.Generic;

namespace LPKO_2_1_Cykly
{
    public sealed class GlpkProgram
    {
        public GlpkProgram(IEnumerable<string> programLines)
        {
            ProgramLines = programLines;
        }

        public IEnumerable<string> ProgramLines { get; }
    }
}
