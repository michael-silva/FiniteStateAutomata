using FiniteStateAutomata.Automata.Facade;
using FiniteStateAutomata.Automata.FiniteState;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata
{
    public class AutomataFactory : IAutomataFactory
    {
        public StringAutomataAlphabetBuilder<char> CreateDeterministic(string text)
        {
            return new StringAutomataAlphabetBuilder<char>(text.ToCharArray(), x => new DeterministicAutomata<char, char>(x)); 
        }
        
        public StringAutomataAlphabetBuilder<char> CreateNonDeterministic(string text)
        {
            return new StringAutomataAlphabetBuilder<char>(text.ToCharArray(), x => new NonDeterministicAutomata<char, char>(x)); 
        }
        
        public FluentAutomataAlphabetBuilder<T> CreateDeterministic<T>(params T[] symbols)
        {
            return new FluentAutomataAlphabetBuilder<T>(symbols, x => new DeterministicAutomata<T, T>(x)); 
        }
        
        public FluentAutomataAlphabetBuilder<T> CreateNonDeterministic<T>(params T[] symbols)
        {
            return new FluentAutomataAlphabetBuilder<T>(symbols, x => new NonDeterministicAutomata<T, T>(x));
        }
    }
}