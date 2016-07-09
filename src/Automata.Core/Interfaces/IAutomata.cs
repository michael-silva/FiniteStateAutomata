using System.Collections.Generic;

namespace Automata.Core.Interfaces
{
    public interface IAutomata
    {
        IAutomataAlphabet Alphabet { get; }        
        bool Acceptance { get; }        
        bool Empty { get; }        
        bool Totality { get; }
        IAutomata Clone();
        
        bool Equals(IAutomata value);
        bool SubsetOf(IAutomata automata);
        bool AnyMatch(params string[] values);
        bool AnyMatch(params char[] values);
        List<int[]> Matches(params string[] values);
        List<int[]> Matches(params char[] values);
        bool IsMatch(params string[] values);
        bool IsMatch(params char[] values);
        bool EndMatch(params string[] values);
        bool EndMatch(params char[] values);
    }
}
