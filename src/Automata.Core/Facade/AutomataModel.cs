using System;
using System.Collections.Generic;
using Automata.Core.Alphabet;
using Automata.Core.FiniteState;
using Automata.Core.Interfaces;

namespace Automata.Core.Facade
{
    public class AutomataModel<T>
        where T : IAutomata
    {
        private IAutomata _automata;
        private List<string> _states;
        private int _currState;
        private int _currSymbol;
        
        private void CreateState(string name)
        {
            _states.Add(name);
        }
        
        private int IndexOfState(string name)
        {
            for(int i = 0; i < _states.Count; i++)
                if(_states[i].Equals(name)) return i;
                
            CreateState(name);
            return _states.Count - 1;
        }
        
        public AutomataModel(IAutomata automata)
        {
            _automata = automata;
            _currState = 0;
            _states = new List<string>();
            CreateState("");
        }

        private AutomataModel<T> When(int index, IComparable value)
        {
            if (index == -1)
                throw new Exception($"The symbol '{value}' don't exist in alphabet!");

            _currSymbol = index;
            return this;
        }
        public AutomataModel<T> When(string value)
        {
            var index = _automata.Alphabet.IndexOf(value);
            return When(index, value);
        }

        public AutomataModel<T> When(char value)
        {
            var index = _automata.Alphabet.IndexOf(value);
            return When(index, value);
        }

        public AutomataModel<T> ToNext()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState + 1);
            return this;
        }
        
        public AutomataModel<T> ToPrev()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState - 1);
            return this;
        }
        
        public AutomataModel<T> To(string name)
        {
            int to = IndexOfState(name);
            
            return To(to);
        }
        
        public AutomataModel<T> To(int index)
        {
            _automata.AddTransition(_currSymbol, _currState, index);
            return this;
        }
        
        public AutomataModel<T> ToFirst()
        {
            _automata.AddTransition(_currSymbol, _currState, 0);
            return this;
        }
        
        public AutomataModel<T> OnNext()
        {
            _currState++;
            return this;
        }
        
        public AutomataModel<T> OnPrev()
        {
            _currState--;
            return this;
        }
        
        public AutomataModel<T> OnFirst()
        {
            _currState = 0;
            return this;
        }
        
        public AutomataModel<T> On(string name)
        {
            int to = IndexOfState(name);
            _currState = to;
            return this;
        }
        
        public AutomataModel<T> Repeat()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState);
            return this;
        }
        
        public AutomataModel<T> Accept()
        {
            _automata.AcceptState(_currState);
            return this;
        }
        
        public T CreateAutomata()
        {
            return (T)_automata.Clone();
        }
    }   
}