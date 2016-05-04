using System;
using System.Linq;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;
using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Automata.FiniteState
{
    public class DeterministicAutomata<T> : IAutomata<T>
    {
        private const int ACCEPT = 1;
        private readonly int ACCEPTCOL;
        private readonly int LENGTH;
        
        private int _acceptCount = 0; 
        private IAutomataAlphabet<T> _alphabet;
        private List<int?[]> _transitions;
        
        public bool Acceptance { get { return _acceptCount > 0; } }
        public bool Empty { get { return _alphabet.Count > 0; } }
        public bool Totality { get { return _acceptCount == _transitions.Length; } }
        
        private DeterministicAutomata(IAutomataAlphabet<T> alphabet, List<int?[]> transitions)
        {
            ACCEPTCOL = alphabet.Length;
            LENGTH = alphabet.Count + 1;
            _alphabet = alphabet;
            _transitions = transitions;
        }
        
        public DeterministicAutomata(IAutomataAlphabet<T> alphabet)
            : this(alphabet, new List<int?[]>())
        { }
        
        public IAutomata<T> AddState()
        {
            _transitions.Add(new int?[LENGTH]);   
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
            _transitions[index][ACCEPTCOL] = ACCEPT;
            _acceptCount++;
            return this;
        }
        
        public IAutomata<T> Intersection(IAutomata<T> automata)
        {
            if(Empty || automata.Empty) 
                return this;
                
            return this;
        }
        
        public bool SubsetOf(IAutomata<T> automata)
        {
            return !Intersection(automata).Empty;
        }
        
        public bool Equals(IAutomata<T> automata)
        {
            return SubsetOf(automata) && automata.SubsetOf(this);
        }
        
        public IAutomata<T> Concat(IAutomata<T> automata)
        {
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                if(_transitions[i][ACCEPTCOL] == ACCEPT)
                {
                    ttable.Add(new int?[LENGTH]);
                    for(int j = 0; j < ACCEPTCOL; j++) 
                        if(ttable[i][j] == null) ttable[i][j] = automata._transitions[0][j]
                }
                else ttable.Add(_transitions[i]);
            }
            
            for(int i = 0; i < automata._transitions.Count; i++)
                ttable.Add(automata._transitions[i]);
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        public IAutomata<T> Union(IAutomata<T> automata)
        {
            //if(this.Equals(automata._alphabet))
            //    throw new Exception("");
            var a = new DeterministicAutomata<T>(_alphabet);
            for(int i = 0; i < _transitions.Count; i++)
            {
                for(int j = 0; j < automata._transitions.Count; j++)
                {
                    for(int z = 0; z < _alphabet.Count; z++)
                    {
                        a.AddTransition(_alphabet[z], i + j, _transitions[i] + automata._transitions[j]);
                    }
                }
            }
            
            return this;
        }
        
        public IAutomata<T> Closure()
        {
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                if(_transitions[i][ACCEPTCOL] == ACCEPT)
                {
                    ttable.Add(new int?[LENGTH]);
                    for(int j = 0; j < ACCEPTCOL; j++) 
                        if(ttable[i][j] == null) ttable[i][j] = _transitions[0][j]
                }
                else ttable.Add(_transitions[i]);
            }
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        public bool AnyMatch(params T[] values)
        {
            int i = 0, j = 0, curr = 0;
            int start = -1;
            int? temp = null;
            for(i = 0; i < values.Length; i++)
            {   
                j = _alphabet.IndexByValue(values[i]);
                if(j == -1 || !(temp = _transitions[curr][j]).HasValue)
                {
                    start = -1;
                    curr = 0;
                    continue;
                }
                
                curr = temp.Value;
                if(start == -1) start = i;
                else (_transitions[curr][ACCEPTCOL] == ACCEPT) return true;
            }
            
            return false;
        }
        
        public List<int[]> Matches(params T[] values)
        {
            var matches = new List<int[]>();
            int i = 0, j = 0, curr = 0;
            int start = -1, end = -1;
            int? temp = null;
            for(i = 0; i < values.Length; i++)
            {
                j = _alphabet.IndexByValue(values[i]);
                if(j == -1 || !(temp = _transitions[curr][j]).HasValue)
                {
                    if(end > -1) matches.Add(new [] { start, end });
                    start = end = -1
                    curr = 0;
                    continue;
                }
                
                if(start == -1) start = i;
                curr = temp.Value;
                if(_transitions[curr][ACCEPTCOL] == ACCEPT) end = i;
            }
            
            if(end > -1) matches.Add(new [] { start, end });
            return matches;
        }
        
        public bool IsMatch(params T[] values)
        {
            int i = 0, j = 0, curr = 0;
            int? temp = null;
            for(i = 0; i < values.Length; i++)
            {
                j = _alphabet.IndexByValue(values[i]);
                if(j == -1) return false;
                
                temp = _transitions[curr][j];
                if(!temp.HasValue) return false;
                
                curr = temp.Value;
            }
            
            return _transitions[curr][ACCEPTCOL] == ACCEPT;
        }
        
        public bool BeginMatch(params T[] values)
        {
            int i = 0, j = 0, curr = 0;
            int? temp = null;
            for(i = 0; i < values.Length; i++)
            {
                if(_transitions[curr][ACCEPTCOL] == ACCEPT) return true;
                
                j = _alphabet.IndexByValue(values[i]);
                if(j == -1) return false;
                
                temp = _transitions[curr][j];
                if(!temp.HasValue) return false;
                
                curr = temp.Value;
            }
            
            return _transitions[curr][ACCEPTCOL] == ACCEPT;
        }
        
        public bool EndMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.Last()[1] == values.Length - 1;
        }
    }
}
