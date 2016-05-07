using System;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Alphabet;
using FiniteStateAutomata.Automata.FiniteState;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Facade
{
    public class FluentAutomata<T>
    {
        private IAutomata<T> _automata;
        private List<string> _states;
        private int _currState;
        private T _currSymbol;
        
        private void CreateState(string name)
        {
            _automata.AddState();
            _states.Add(name);
        }
        
        private int IndexOfState(string name)
        {
            for(int i = 0; i < _states.Count; i++)
                if(_states[i].Equals(name)) return i;
                
            CreateState(name);
            return _states.Count -1;
        }
        
        public FluentAutomata(IAutomata<T> automata)
        {
            _automata = automata;
            _currState = 0;
            _currSymbol = default(T);
            _states = new List<string>();
            CreateState("");
        }
        
        public FluentAutomata<T> When(T value)
        {
            if(value == null) throw new Exception($"The symbol don't exist in alphabet!");
            
            _currSymbol = value;
            return this;
        }
        
        public FluentAutomata<T> ToNext()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState + 1);
            return this;
        }
        
        public FluentAutomata<T> ToPrev()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState - 1);
            return this;
        }
        
        public FluentAutomata<T> To(string name)
        {
            int to = IndexOfState(name);
            _automata.AddTransition(_currSymbol, _currState, to);
            return this;
        }
        
        public FluentAutomata<T> ToFirst()
        {
            _automata.AddTransition(_currSymbol, _currState, 0);
            return this;
        }
        
        public FluentAutomata<T> OnNext()
        {
            _automata.AddState();
            _currState++;
            return this;
        }
        
        public FluentAutomata<T> OnPrev()
        {
            _currState--;
            return this;
        }
        
        public FluentAutomata<T> OnFirst()
        {
            _currState = 0;
            return this;
        }
        
        public FluentAutomata<T> On(string name)
        {
            int to = IndexOfState(name);
            _currState = to;
            return this;
        }
        
        public FluentAutomata<T> Repeat()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState);
            return this;
        }
        
        public FluentAutomata<T> Accept()
        {
            _automata.AcceptState(_currState);
            return this;
        }
        
        public bool IsMatch(params T[] values)
        {
            return _automata.IsMatch(values);
        }
        
        public List<int[]> Matches(params T[] values)
        {
            return _automata.Matches(values);
        }
    }   
}