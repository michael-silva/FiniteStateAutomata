using System;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Alphabet;
using FiniteStateAutomata.Automata.FiniteState;

namespace FiniteStateAutomata.Automata.Facade
{
    public class FluentAutomataBase<T>
    {
        /*private AutomataBase<T> _automata;
        private int _currState, _currSymbol;
        
        public FluentAutomataBase(AutomataAlphabet<T> alphabet)
        {
            _automata = new DeterministicAutomata<T>(alphabet);
            _currState = 0;
            _currSymbol = 0;
        }
        
        public FluentAutomataBase<T> When(T value)
        {
            int i = _automata.IndexOf(value);
            if(i == -1) throw new Exception("The {value} don't exist in alphabet!");
            
            _currSymbol = i;
            return this;
        }
        
        public FluentAutomataBase<T> MoveToNext()
        {
            _automata.AddTransition(_currState, _currSymbol, _currState + 1);
            return this;
        }
        
        public FluentAutomataBase<T> OnNext()
        {
            _automata.AddState();
            return this;
        }
        
        public FluentAutomataBase<T> Repeat()
        {
            _automata.AddTransition(_currState, _currSymbol, _currState);
            return this;
        }
        
        public FluentAutomataBase<T> Accept()
        {
            _automata.AcceptState(_currState);
            return this;
        }
        
        public bool IsMatch(params T[] values)
        {
            return _automata.IsMatch(values);
        }
        
        public Dictionary<int, int> Matches(params T[] values)
        {
            return _automata.Matches(values);
        }*/
    }   
}