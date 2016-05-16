
using System.Collections.Generic;

namespace Automata.Core.Interfaces
{
    public interface IAutomata //: IComparable
    {
        IAutomata Clone();

        IAutomataAlphabet Alphabet { get; }
        
        bool Acceptance { get; }
        
        bool Empty { get; }
        
        bool Totality { get; }
        
        void AddTransition(int symbolIndex, int fromState, int toState);
        
        void AcceptState(int index);
        
        //bool Match(string value);
        
        //bool Equals(IComparable value);
        
        //bool IsMatch(params T[] values);
        
        //List<int[]> Matches(params T[] values);
    }
}