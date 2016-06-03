using System;
using System.Collections.Generic;

namespace Automata.Core.Interfaces
{
    public interface IAutomata
    {
        IAutomataAlphabet Alphabet { get; }        
        bool Acceptance { get; }        
        bool Empty { get; }        
        bool Totality { get; }
        void AddTransition(int symbolIndex, int fromState, int toState);
        void AcceptState(int index);
        IAutomata Clone();
        bool Equals(IAutomata value);
        //bool Match(string value);

        //bool IsMatch(params T[] values);

        //List<int[]> Matches(params T[] values);
    }
}
