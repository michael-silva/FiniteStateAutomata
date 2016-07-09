using System;
using System.Collections.Generic;
using Automata.Core.FiniteState;
using Automata.Core.Interfaces;

namespace Automata.Core.Facade
{
    public class AutomataModel<T>
        where T : IAutomataTableBuilder
    {
        private IAutomata _automata;
        private List<string> _states;
        private int _currState;
        private int _currSymbol;
        private bool _isEpsilon;
        
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

        public AutomataModel<T> Epsilon()
        {
            if (_automata is NonDeterministicAutomata)
                _isEpsilon = true;
            else
                throw new Exception($"The Epsilon method is only enabled to NonDeterministic Automatas!");
            return this;
        }

        private AutomataModel<T> When(int index, IComparable value)
        {
            if (index == -1)
                throw new Exception($"The symbol '{value}' don't exist in alphabet!");

            _currSymbol = index;
            _isEpsilon = false;
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
            if(_isEpsilon)
                (_automata as NonDeterministicAutomata).AddEpsilon(_currState, _currState + 1);
            else
                (_automata as IAutomataTableBuilder).AddTransition(_currSymbol, _currState, _currState + 1);
            return this;
        }
        
        public AutomataModel<T> ToPrev()
        {
            if(_isEpsilon)
                (_automata as NonDeterministicAutomata).AddEpsilon(_currState, _currState - 1);
            else
                (_automata as IAutomataTableBuilder).AddTransition(_currSymbol, _currState, _currState - 1);
            return this;
        }
        
        public AutomataModel<T> To(string name)
        {
            int to = IndexOfState(name);
            
            return To(to);
        }
        
        public AutomataModel<T> To(int index)
        {
            if(_isEpsilon)
                (_automata as NonDeterministicAutomata).AddEpsilon(_currState, index);
            else
                (_automata as IAutomataTableBuilder).AddTransition(_currSymbol, _currState, index);
            return this;
        }
        
        public AutomataModel<T> ToFirst()
        {
            (_automata as IAutomataTableBuilder).AddTransition(_currSymbol, _currState, 0);
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
            (_automata as IAutomataTableBuilder).AddTransition(_currSymbol, _currState, _currState);
            return this;
        }
        
        public AutomataModel<T> Accept()
        {
            (_automata as IAutomataTableBuilder).AcceptState(_currState);
            return this;
        }
        
        public T CreateAutomata()
        {
            return (T)_automata.Clone();
        }
    }   
}