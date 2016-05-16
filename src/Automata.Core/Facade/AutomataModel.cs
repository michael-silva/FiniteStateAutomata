using System;
using System.Collections.Generic;
using Automata.Core.Alphabet;
using Automata.Core.FiniteState;
using Automata.Core.Interfaces;

namespace Automata.Core.Facade
{
    public class AutomataModel
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
        
        public AutomataModel When(string value)
        {
            var index = _automata.Alphabet.IndexOf(value);
            if(index == -1) 
                throw new Exception($"The symbol don't exist in alphabet!");
            
            _currSymbol = index;
            return this;
        }
        
        public AutomataModel ToNext()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState + 1);
            return this;
        }
        
        public AutomataModel ToPrev()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState - 1);
            return this;
        }
        
        public AutomataModel To(string name)
        {
            int to = IndexOfState(name);
            _automata.AddTransition(_currSymbol, _currState, to);
            return this;
        }
        
        public AutomataModel ToFirst()
        {
            _automata.AddTransition(_currSymbol, _currState, 0);
            return this;
        }
        
        public AutomataModel OnNext()
        {
            _currState++;
            return this;
        }
        
        public AutomataModel OnPrev()
        {
            _currState--;
            return this;
        }
        
        public AutomataModel OnFirst()
        {
            _currState = 0;
            return this;
        }
        
        public AutomataModel On(string name)
        {
            int to = IndexOfState(name);
            _currState = to;
            return this;
        }
        
        public AutomataModel Repeat()
        {
            _automata.AddTransition(_currSymbol, _currState, _currState);
            return this;
        }
        
        public AutomataModel Accept()
        {
            _automata.AcceptState(_currState);
            return this;
        }
        
        public IAutomata CreateAutomata()
        {
            return _automata;
        }
    }   
}