using System;
using System.Linq;
using System.Collections.Generic;
using Automata.Core.Interfaces;
using Automata.Core.Alphabet;

namespace Automata.Core.FiniteState
{
    public class DeterministicAutomata : IAutomata
    {
        private const int ACCEPT = 1;
        private readonly int ACCEPTCOL;
        private readonly int LENGTH;
        
        private int _acceptCount = 0;
        private int _firstAccept = 0; 
        private IAutomataAlphabet _alphabet;
        private List<int?[]> _transitions;
        
        public IAutomataAlphabet Alphabet { get { return _alphabet; } }
        public bool Acceptance { get { return _acceptCount > 0; } }
        public bool Empty { get { return _alphabet.Length > 0; } }
        public bool Totality { get { return _acceptCount == _transitions.Count; } }
        
        public DeterministicAutomata(IAutomataAlphabet alphabet, List<int?[]> transitions)
        {
            ACCEPTCOL = alphabet.Length;
            LENGTH = alphabet.Length + 1;
            
            _alphabet = alphabet;
            _transitions = new List<int?[]>();
            
            if(transitions != null)
                _transitions = transitions;
        }
        
        public DeterministicAutomata(IAutomataAlphabet alphabet)
            : this(alphabet, null)
        { }
        
        public DeterministicAutomata(string symbols)
            : this(new AutomataCharAlphabet(symbols), null)
        { }
        
        public DeterministicAutomata Clone()
        {   
            return new DeterministicAutomata(_alphabet, new List<int?[]>(_transitions.ToArray()));
        }
        
        public DeterministicAutomata State(params int?[] values)
        {
            if(values == null || values.Length != _alphabet.Length) 
                throw new Exception($"A state need to set transitions to all {_alphabet.Length} alphabet symbols");
                
            var temp = new int?[LENGTH];
            for(int j = 0; j < values.Length; j++)
                temp[j] = values[j];
                
            _transitions.Add(temp);
            return this;
        }
        
        public void AddTransition(int symbolIndex, int fromState, int toState)
        {
            if(symbolIndex >= LENGTH)
                throw new Exception("The symbolIndex is out of range");
                
            for(int i = _transitions.Count; i <= Math.Max(fromState, toState); i++)
                _transitions.Add(new int?[LENGTH]);
                
            _transitions[fromState][symbolIndex] = toState;
        }
        
        public void AddTransition(char symbol, int fromState, int toState)
        {
            int index = _alphabet.IndexOf(symbol.ToString());
            AddTransition(index, fromState, toState);
        }
        
        public void AddTransition(string symbol, int fromState, int toState)
        {
            int index = _alphabet.IndexOf(symbol);
            AddTransition(index, fromState, toState);
        }
        
        /*public void AddTransition(IAutomata symbol, int fromState, int toState)
        {
            int index = _alphabet.IndexOf(symbol);
            AddTransition(index, fromState, toState);
        }*/
        
        public void AcceptState(int index)
        {
            if(index < _firstAccept)
                _firstAccept = index;
                
            _transitions[index][ACCEPTCOL] = ACCEPT;
            _acceptCount++;
        }
        
        public void AcceptLast()
        {
            int index = _transitions.Count - 1;
            AcceptState(index);
        }
        
        public NonDeterministicAutomata<IComparable> ToNonDeterministic()
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
        
        public DeterministicAutomata Optimize()
        {
            //inject optimize algorithm
            return this;
        }
        
        public DeterministicAutomata Reverse()
        {
            return this;//ToNonDeterministic().Reverse().ToDeterministic().Optimize();
        }
        
        public DeterministicAutomata Intersection(DeterministicAutomata automata)
        {
            if(Empty || automata.Empty) 
                return this;
                
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                for(int j = 0; j < automata._transitions.Count; j++)
                {    
                    ttable.Add(new int?[LENGTH]);
                    for(int z = 0; z < _alphabet.Length; z++)
                    {
                        ttable[i + j][z] = _transitions[i][z] + automata._transitions[j][z];
                    }
                
                    if(_transitions[i][ACCEPTCOL] == ACCEPT && automata._transitions[j][ACCEPTCOL] == ACCEPT)
                        ttable[i + j][ACCEPTCOL] = ACCEPT;
                }
            }
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        public DeterministicAutomata Union(DeterministicAutomata automata)
        {
            if(Empty || automata.Empty) 
                return this;
                
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                for(int j = 0; j < automata._transitions.Count; j++)
                {    
                    ttable.Add(new int?[LENGTH]);
                    for(int z = 0; z < _alphabet.Length; z++)
                    {
                        ttable[i + j][z] = _transitions[i][z] + automata._transitions[j][z];
                    }
                
                    if(_transitions[i][ACCEPTCOL] == ACCEPT || automata._transitions[j][ACCEPTCOL] == ACCEPT)
                        ttable[i + j][ACCEPTCOL] = ACCEPT;
                }
            }
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        
        public DeterministicAutomata Difference(DeterministicAutomata automata)
        {
            if(Empty || automata.Empty) 
                return this;
                
            var ttable = new List<int?[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                for(int j = 0; j < automata._transitions.Count; j++)
                {    
                    ttable.Add(new int?[LENGTH]);
                    for(int z = 0; z < _alphabet.Length; z++)
                    {
                        ttable[i + j][z] = _transitions[i][z] + automata._transitions[j][z];
                    }
                
                    if(_transitions[i][ACCEPTCOL] == ACCEPT && automata._transitions[j][ACCEPTCOL] != ACCEPT)
                        ttable[i + j][ACCEPTCOL] = ACCEPT;
                }
            }
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        public bool SubsetOf(DeterministicAutomata automata)
        {
            return !Intersection(automata).Empty;
        }
        
        public bool Equals(DeterministicAutomata automata)
        {
            return SubsetOf(automata) && automata.SubsetOf(this);
        }
        
        public DeterministicAutomata Concat(DeterministicAutomata automata)
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
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        public DeterministicAutomata Closure()
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
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        public bool AnyMatch(params string[] values)
        {
            int i = 0, j = 0, curr = 0;
            int start = -1;
            int? temp = null;
            for(i = 0; i < values.Length; i++)
            {   
                j = _alphabet.IndexOf(values[i]);
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
        
        public List<int[]> Matches(params string[] values)
        {
            var matches = new List<int[]>();
            int i = 0, j = 0, curr = 0;
            int start = -1, end = -1;
            int? temp = null;
            for(i = 0; i < values.Length; i++)
            {
                j = _alphabet.IndexOf(values[i]);
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
        
        public bool IsMatch(params char[] values)
        {
            int i = 0, j = 0, curr = 0;
            int? temp = null;
            for(i = 0; i < values.Length; i++)
            {
                j = _alphabet.IndexOf(values[i].ToString());
                if(j == -1) 
                    return false;
                
                temp = _transitions[curr][j];
                if(!temp.HasValue)
                    return false;
                
                curr = temp.Value;
            }
            
            return _transitions[curr][ACCEPTCOL] == ACCEPT;
        }
        
        public bool BeginMatch(params string[] values)
        {
            int i = 0, j = 0, curr = 0;
            int? temp = null;
            for(i = 0; i < values.Length; i++)
            {
                if(_transitions[curr][ACCEPTCOL] == ACCEPT) return true;
                
                j = _alphabet.IndexOf(values[i]);
                if(j == -1) return false;
                
                temp = _transitions[curr][j];
                if(!temp.HasValue) return false;
                
                curr = temp.Value;
            }
            
            return _transitions[curr][ACCEPTCOL] == ACCEPT;
        }
        
        public bool EndMatch(params string[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.Last()[1] == values.Length - 1;
        }
    }
}
