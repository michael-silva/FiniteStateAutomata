using System;
using System.Linq;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;
using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Automata.FiniteState
{
    public class RegularExpressionAutomata<T> : IAutomata<T>
    {
        private const int ACCEPT = 1;
        
        private IAutomataAlphabet<T> _alphabet;
        private List<int?[]> _transitions;
        
        public RegularExpressionAutomata(IAutomataAlphabet<T> alphabet)
        {
            _alphabet = alphabet;
            _transitions = new List<int?[]>();
        }
        
        public IAutomata<T> AddState()
        {
            _transitions.Add(new int?[_alphabet.Count + 1]);   
            return this;
        }
        
        public IAutomata<T> AddTransition(T symbol, int fromState, int toState)
        {
            int index = _alphabet.IndexByValue(symbol);
            _transitions[fromState][index] = toState;
            return this;
        }
        
        public IAutomata<T> AcceptState(int index)
        {
            int col = _transitions[index].Length - 1;
            _transitions[index][col] = ACCEPT;
            return this;
        }
        
        public IAutomata<T> Concat(IAutomata<T> automata)
        {
            return this;
        }
        
        public IAutomata<T> Union(IAutomata<T> automata)
        {
            return this;
        }
        
        public IAutomata<T> Closure()
        {
            return this;
        }
        
        public bool IsMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.ContainsKey(0) && matches[0] == _transitions.Count - 1;
        }
        
        public bool EndMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches[matches.Keys.Max()] == _transitions.Count - 1;
        }
        
        public bool BeginMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.ContainsKey(0);
        }
        
        public bool AnyMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0;
        }
        
        public Dictionary<int, int> Matches(params T[] values)
        {
            var matches = new Dictionary<int, int>();
            int col = _transitions[0].Length - 1;
            int start = -1, accept = -1, curr = 0, find = 0;
            int? temp = null;
            for(int i = 0; i < values.Length; i++)
            {
                find = _alphabet.IndexByValue(values[i]);
                if(find >= 0)
                {
                    temp = _transitions[curr][find];
                    if(temp.HasValue)
                    {
                        if(start < 0)
                            start = curr;
                        else if(_transitions[temp.Value][col].HasValue && _transitions[temp.Value][col].Value == ACCEPT)
                            accept = temp.Value;
                        
                        curr = temp.Value;
                    }
                }
                
                if(i == values.Length - 1 || !temp.HasValue || find == -1)
                {
                    if(accept >= 0) matches.Add(start, accept);
                    start = accept = -1;
                    curr = 0;
                }
            }
            
            return matches;
        }
    }
}
