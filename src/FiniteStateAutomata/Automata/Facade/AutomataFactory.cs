using System;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.FiniteState;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Facade
{
    public class AutomataFactory<T>
    {
        private IAutomataAlphabet<T> _alphabet;
        
        public AutomataFactory(IAutomataAlphabet<T> alphabet)
        {
            _alphabet = alphabet;
        }
        
        public FluentAutomata<T> Deterministic()
        {
            var a = new DeterministicAutomata<T>(_alphabet);
            return new FluentAutomata<T>(a);
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