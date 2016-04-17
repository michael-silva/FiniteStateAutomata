using System;
using System.Linq;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;
using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Automata.FiniteState
{
    public class DeterministicAutomata<TKey, TValue> : IAutomata<TKey, TValue>
    {
        private AutomataAlphabet<TKey, TValue> _alphabet;
        private List<int[]> _transitions;
        private List<int> _accepts;
        
        public DeterministicAutomata(AutomataAlphabet<TKey, TValue> alphabet)
        {
            _alphabet = alphabet;
            _transitions = new List<int[]>() { new int[_alphabet.Length] };
            _accepts = new List<int>();
        }
        
        public IAutomata<TKey, TValue> AddState()
        {
            _transitions.Add(new int[_alphabet.Length]);   
            return this;
        }
        
        public IAutomata<TKey, TValue> AddTransition(int fromState, int symbol, int toState)
        {
            _transitions[fromState][symbol] = toState;
            return this;
        }
        
        public IAutomata<TKey, TValue> AcceptState(int index)
        {
            _accepts.Add(index);
            return this;
        }
        
        public int IndexOf(TValue value)
        {
            return _alphabet.IndexByValue(value);
        }
        
        public bool IsMatchExact(params TValue[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.ContainsKey(0) && matches[matches.Keys.Max()] == values.Length - 1;
        }
        
        public bool IsMatchExactEnd(params TValue[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches[matches.Keys.Max()] == values.Length - 1;
        }
        
        public bool IsMatchExactStart(params TValue[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.ContainsKey(0);
        }
        
        public bool IsMatch(params TValue[] values)
        {
            return Matches(values).Count > 0;
        }
        
        public Dictionary<int, int> Matches(params TValue[] values)
        {
            var matches = new Dictionary<int, int>();
            int start = -1, accept = -1, curr = 0;
            for(int i = 0; i < values.Length; i++)
            {
                int find = _alphabet.IndexByValue(values[i]);
                if(find >= 0)
                {
                    int temp = _transitions[curr][find];
                    if(temp >= 0)
                    {
                        if(start < 0)
                            start = temp;
                        else if(_accepts.Any(x => x == temp))
                            accept = temp;
                        curr = temp;
                    }
                    else find = -1;
                }
                if(find == -1)
                {
                    if(accept >= 0) matches.Add(start, accept);
                    start = accept = -1;
                    curr = 0;
                }
                
            }
            
            if(accept >= 0) matches.Add(start, accept);
            return matches;
        }
    }
}
