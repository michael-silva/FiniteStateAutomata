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
        private int _firstAccept = 0; 
        private IAutomataAlphabet<T> _alphabet;
        private List<int?[]> _transitions;
        
        public bool Acceptance { get { return _acceptCount > 0; } }
        public bool Empty { get { return _alphabet.Count > 0; } }
        public bool Totality { get { return _acceptCount == _transitions.Count; } }
        
        public DeterministicAutomata(IAutomataAlphabet<T> alphabet, List<int?[]> transitions)
        {
            ACCEPTCOL = alphabet.Count;
            LENGTH = alphabet.Count + 1;
            _alphabet = alphabet;
            _transitions = new List<int?[]>();
            if(transitions != null)
            {
                for(int i = 0; i < transitions.Count; i++)
                {
                    var temp = new int?[LENGTH];
                    for(int j = 0; j < _alphabet.Count; j++)
                        temp[j] = transitions[i][j];
                        
                    _transitions.Add(temp);
                }
            }
        }
        
        public DeterministicAutomata(IAutomataAlphabet<T> alphabet)
            : this(alphabet, null)
        { }
        
        public void AddState()
        {
            _transitions.Add(new int?[LENGTH]);
        }
        
        public void AddTransition(T symbol, int fromState, int toState)
        {
            int index = _alphabet.IndexByValue(symbol);
            _transitions[fromState][index] = toState;
        }
        
        public void AcceptState(int index)
        {
            if(index < _firstAccept)
                _firstAccept = index;
                
            _transitions[index][ACCEPTCOL] = ACCEPT;
            _acceptCount++;
        }
        
        public NonDeterministicAutomata<T> ToNonDeterministic()
        {
            var ttable = new List<List<int>[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                ttable.Add(new List<int>[LENGTH]);
                for(int j = 0; j < LENGTH; j++)
                {
                    //ttable[i][j] = new List<int>() { _transitions[i][j] };
                }
            }
            
            return null;//new NonDeterministicAutomata(_alphabet, ttable);
        }
        
        public DeterministicAutomata<T> Optimize()
        {
            //inject optimize algorithm
            return this;
        }
        
        public DeterministicAutomata<T> Reverse()
        {
            return this;//ToNonDeterministic().Reverse().ToDeterministic().Optimize();
        }
        
        public DeterministicAutomata<T> Intersection(DeterministicAutomata<T> automata)
        {
            if(Empty || automata.Empty) 
                return this;
                
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                for(int j = 0; j < automata._transitions.Count; j++)
                {    
                    ttable.Add(new int?[LENGTH]);
                    for(int z = 0; z < _alphabet.Count; z++)
                    {
                        ttable[i + j][z] = _transitions[i][z] + automata._transitions[j][z];
                    }
                
                    if(_transitions[i][ACCEPTCOL] == ACCEPT && automata._transitions[j][ACCEPTCOL] == ACCEPT)
                        ttable[i + j][ACCEPTCOL] = ACCEPT;
                }
            }
            
            return new DeterministicAutomata<T>(_alphabet, ttable);
        }
        
        public DeterministicAutomata<T> Union(DeterministicAutomata<T> automata)
        {
            if(Empty || automata.Empty) 
                return this;
                
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                for(int j = 0; j < automata._transitions.Count; j++)
                {    
                    ttable.Add(new int?[LENGTH]);
                    for(int z = 0; z < _alphabet.Count; z++)
                    {
                        ttable[i + j][z] = _transitions[i][z] + automata._transitions[j][z];
                    }
                
                    if(_transitions[i][ACCEPTCOL] == ACCEPT || automata._transitions[j][ACCEPTCOL] == ACCEPT)
                        ttable[i + j][ACCEPTCOL] = ACCEPT;
                }
            }
            
            return new DeterministicAutomata<T>(_alphabet, ttable);
        }
        
        
        public DeterministicAutomata<T> Difference(DeterministicAutomata<T> automata)
        {
            if(Empty || automata.Empty) 
                return this;
                
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                for(int j = 0; j < automata._transitions.Count; j++)
                {    
                    ttable.Add(new int?[LENGTH]);
                    for(int z = 0; z < _alphabet.Count; z++)
                    {
                        ttable[i + j][z] = _transitions[i][z] + automata._transitions[j][z];
                    }
                
                    if(_transitions[i][ACCEPTCOL] == ACCEPT && automata._transitions[j][ACCEPTCOL] != ACCEPT)
                        ttable[i + j][ACCEPTCOL] = ACCEPT;
                }
            }
            
            return new DeterministicAutomata<T>(_alphabet, ttable);
        }
        
        public bool SubsetOf(DeterministicAutomata<T> automata)
        {
            return !Intersection(automata).Empty;
        }
        
        public bool Equals(DeterministicAutomata<T> automata)
        {
            return SubsetOf(automata) && automata.SubsetOf(this);
        }
        
        public DeterministicAutomata<T> Concat(DeterministicAutomata<T> automata)
        {
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                if(_transitions[i][ACCEPTCOL] == ACCEPT)
                {
                    ttable.Add(new int?[LENGTH]);
                    for(int j = 0; j < ACCEPTCOL; j++) 
                        if(ttable[i][j] == null) ttable[i][j] = automata._transitions[0][j];
                }
                else ttable.Add(_transitions[i]);
            }
            
            for(int i = 0; i < automata._transitions.Count; i++)
                ttable.Add(automata._transitions[i]);
            
            return new DeterministicAutomata<T>(_alphabet, ttable);
        }
        
        public DeterministicAutomata<T> Closure()
        {
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                if(_transitions[i][ACCEPTCOL] == ACCEPT)
                {
                    ttable.Add(new int?[LENGTH]);
                    for(int j = 0; j < ACCEPTCOL; j++) 
                        if(ttable[i][j] == null) ttable[i][j] = _transitions[0][j];
                }
                else ttable.Add(_transitions[i]);
            }
            
            return new DeterministicAutomata<T>(_alphabet, ttable);
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
                //else (_transitions[curr][ACCEPTCOL] == ACCEPT) return true;
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
                    start = end = -1;
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
                if(j == -1) 
                    return false;
                
                temp = _transitions[curr][j];
                if(!temp.HasValue)
                    return false;
                
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
