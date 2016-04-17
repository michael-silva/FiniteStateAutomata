using System;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;
using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Automata.Facade
{
    public class StringAutomataAlphabetBuilder : FluentAutomataAlphabetBuilder<char> 
    {
        public StringAutomataAlphabetBuilder(char[] symbols, Func<AutomataAlphabet<char, char>, IAutomata<char, char>> func)
            : base(symbols, func)
        { }
        
        public new FluentAutomata<char> OnFirstState()
        {
            var alphabet = MakeAlphabet();
            var automata = _instantiate(alphabet);
            return new StringAutomata(automata);
        }
    }
}