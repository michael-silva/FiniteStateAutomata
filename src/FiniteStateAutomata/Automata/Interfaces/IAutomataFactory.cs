using System.Collections.Generic;
using FiniteStateAutomata.Automata.Facade;

namespace FiniteStateAutomata.Automata.Interfaces
{
    public interface IAutomataFactory
    {
        FluentAutomataAlphabetBuilder<char> CreateDeterministic(string text);
        FluentAutomataAlphabetBuilder<T> CreateDeterministic<T>(params T[] symbols);
        FluentAutomataAlphabetBuilder<T> CreateNonDeterministic<T>(params T[] symbols);
    }
}