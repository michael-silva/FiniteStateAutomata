using System;
using System.Linq;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;
using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Automata.FiniteState
{
    public class NonDeterministicAutomata<T> : IAutomata<T>
    {
        private const int ACCEPT = 1;
        private readonly int ACCEPTCOL;
        private readonly int EPSILONCOL;
        
        private IAutomataAlphabet<T> _alphabet;
        private List<List<int>[]> _transitions;
        
        private NonDeterministicAutomata(IAutomataAlphabet<T> alphabet, List<List<int>[]> transitions)
        {
            EPSILONCOL = alphabet.Length;
            ACCEPTCOL = alphabet.Length + 1;
            _alphabet = alphabet;
            _transitions = transitions;
        }
        
        public NonDeterministicAutomata(IAutomataAlphabet<T> alphabet)
            : this(alphabet, new List<List<int>[]>())
        { }
        
        public IAutomata<T> AddState()
        {
            _transitions.Add(new List<int>[_alphabet.Count + 2]);
            return this;
        }
        
        public IAutomata<T> AddTransition(T symbol, int fromState, int toState)
        {
            int index = _alphabet.IndexByValue(symbol);
            if(_transitions[fromState][index] == null)
                _transitions[fromState][index] = new List<int>();
            _transitions[fromState][index].Add(toState);
            return this;
        }
        
        public IAutomata<T> AcceptState(int index)
        {
            _transitions[index][ACCEPTCOL] = new List<int>() { ACCEPT };
            return this;
        }
        
        public IAutomata<T> Concat(IAutomata<T> automata)
        {
            int length = _alphabet.Count + 2;
            var ttable = new List<List<int>[]>();
            
            ttable.Add(new List<int>[length]);
            for(int i = 0; i < _transitions.Count; i++)
            {
                ttable.Add(new List<int>[length]);
                ttable.Add(_transitions[i]);
                if(ttable[i][ACCEPTCOL] != null && ttable[i][ACCEPTCOL].Count > 0)
                {
                    ttable[i][ACCEPTCOL] = null;
                    ttable[i][EPSILONCOL] = new List<int>() { _transitions.Count };
                }
            }
            
            for(int i = 0; i < automata._transitions.Count; i++)
            {
                ttable.Add(new List<int>[length]);
                ttable.Add(automata._transitions[i]);
            }
            
            return new NonDeterministicAutomata(_alphabet, ttable);
        }
        
        public IAutomata<T> Union(IAutomata<T> automata)
        {
            int length = _alphabet.Count + 2;
            var ttable = new List<List<int>[]>();
            
            ttable.Add(new List<int>[length]);
            ttable[0][EPSILONCOL].Add(1);
            ttable[0][EPSILONCOL].Add(_transitions.Count + 1);
            
            for(int i = 0; i < _transitions.Count; i++)
            {
                ttable.Add(new List<int>[length]);
                ttable.Add(_transitions[i]);
            }
            
            for(int i = 0; i < automata._transitions.Count; i++)
            {
                ttable.Add(new List<int>[length]);
                ttable.Add(automata._transitions[i]);
            }
            
            return new NonDeterministicAutomata(_alphabet, ttable);
        }
        
        public IAutomata<T> Closure()
        {
            int length = _alphabet.Count + 2;
            var ttable = new List<List<int>[]>();
            
            ttable.Add(new List<int>[length]);
            ttable[0][EPSILONCOL].Add(1);
            
            for(int i = 0; i < _transitions.Count; i++)
            {
                ttable.Add(new List<int>[length]);
                ttable.Add(_transitions[i]);
            }
               
            ttable.Add(new List<int>[length]);
            ttable[ttable.Count - 1][EPSILONCOL].Add(0);
            
            return new NonDeterministicAutomata(_alphabet, ttable);
        }
        
        public bool IsMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.ContainsKey(0) && matches[0] == values.Length - 1;
        }
        
        public bool EndMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches[matches.Keys.Max()] == values.Length - 1;
        }
        
        public bool BeginMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.ContainsKey(0);
        }
        
        public bool AnyMatch(params T[] values)
        {
            return Matches(values).Count > 0;
        }
        
        public Dictionary<int, int> Matches(params T[] values)
        {
            var matches = new Dictionary<int, int>();
            int col = _transitions[0].Length - 1;
            int start = -1, accept = -1, curr = 0;
            var backup = new Queue<int>();
            for(int i = 0; i < values.Length; i++)
            {
                int find = _alphabet.IndexByValue(values[i]);
                if(find >= 0)
                {
                    var temp = _transitions[curr][find];
                    if(temp == null || !temp.Any()) find = -1;
                    else 
                    {
                        for(int j = 0; j < temp.Count; j++)
                        {
                            if(temp[j] >= 0)
                            {
                                if(start < 0)
                                    start = i;
                                if(_transitions[temp[j]][col] != null && _transitions[temp[j]][col][0] == ACCEPT)
                                    accept = i;
                                backup.Enqueue(curr);
                                curr = temp[j];
                            }
                        }
                    }
                }
                
                if(i == values.Length -1 || find == -1)
                {
                    if(accept >= 0) matches.Add(start, accept);
                    start = accept = -1;
                    curr = backup.Count > 0 ? backup.Dequeue() : 0;
                }
                
            }
            
            return matches;
        }
    }
}
