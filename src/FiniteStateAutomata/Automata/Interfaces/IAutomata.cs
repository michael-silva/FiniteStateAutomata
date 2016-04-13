using System.Collections.Generic;

namespace FiniteStateAutomata.Automata.Interfaces
{
    public interface IAutomata<TKey, TValue>
    {
        IAutomata<TKey, TValue> AddState();
        
        IAutomata<TKey, TValue> AddTransition(int fromState, int symbol, int toState);
        
        IAutomata<TKey, TValue> AcceptState(int index);
        
        bool IsMatch(params TValue[] values);
        
        Dictionary<int, int> Matches(params TValue[] values);
    }
}
