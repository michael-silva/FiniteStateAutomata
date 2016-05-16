using System.Collections.Generic;
using Automata.Core.FiniteState;
using Automata.Core.Alphabet;
using Automata.Core.Interfaces;

namespace Automata.Core.Facade
{
    public class AutomataFactory
    {
        private IAutomataAlphabet _alphabet;
        
        public AutomataFactory(IAutomataAlphabet alphabet)
        {
            _alphabet = alphabet;
        }
        
        public AutomataFactory(string symbols)
            : this(new AutomataCharAlphabet(symbols))
        { }
        
        public AutomataModel Deterministic()
        {
            var a = new DeterministicAutomata(_alphabet);
            return new AutomataModel(a);
        }
        
        /*public FluentAutomata<T> NonDeterministic()
        {
            var a =  new NonDeterministicAutomata<T>(_alphabet);
            return new FluentAutomata<T>(a);
        }
        
        /*public FluentAutomata<T> RegularExpression()
        {
            var a = new RegularExpressionAutomata<T>(_alphabet);
            return new FluentAutomata<T>(a);
        }*/
    }   
}