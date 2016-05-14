using System.Collections.Generic;

namespace FiniteStateAutomata.Automata.Interfaces
{
    public interface IAutomata<T>
    {   
        void AddState();
        
        void AddTransition(T symbol, int fromState, int toState);
        
        void AcceptState(int index);
        
        bool IsMatch(params T[] values);
        
        List<int[]> Matches(params T[] values);
    }
}
