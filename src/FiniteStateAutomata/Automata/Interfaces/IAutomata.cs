using System.Collections.Generic;

namespace FiniteStateAutomata.Automata.Interfaces
{
    public interface IAutomata<T>
    {   
        IAutomata<T> AddState();
        
        IAutomata<T> AddTransition(T symbol, int fromState, int toState);
        
        IAutomata<T> AcceptState(int index);
        
        IAutomata<T> Concat(IAutomata<T> automata);
        
        IAutomata<T> Union(IAutomata<T> automata);
        
        IAutomata<T> Closure();
        
        bool IsMatch(params T[] values);
        
        List<int[]> Matches(params T[] values);
        
        
    }
}
