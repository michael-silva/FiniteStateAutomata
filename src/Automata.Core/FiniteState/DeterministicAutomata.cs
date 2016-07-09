using System;
using System.Linq;
using System.Collections.Generic;
using Automata.Core.Interfaces;

namespace Automata.Core.FiniteState
{
    public class DeterministicAutomata : IAutomata, IAutomataTableBuilder
    {
        #region Properties and Constants
        private const int ACCEPT = 1;
        private readonly int ACCEPTCOL;
        private readonly int LENGTH;
        
        private int _acceptCount = 0;
        private int _firstAccept = 0; 
        private IAutomataAlphabet _alphabet;
        private List<int?[]> _transitions;
        
        public IAutomataOptimizer<List<int?[]>> Optimizer { get; set; }
        public IAutomataAlphabet Alphabet { get { return _alphabet; } }
        public bool Acceptance { get { return _acceptCount > 0; } }
        public bool Empty { get { return _alphabet.Length == 0; } }
        public bool Totality { get { return _acceptCount == _transitions.Count; } }
        #endregion

        #region Constructors
        internal DeterministicAutomata(IAutomataAlphabet alphabet, List<int?[]> transitions)
        {
            ACCEPTCOL = alphabet.Length;
            LENGTH = alphabet.Length + 1;
            
            _alphabet = alphabet;
            _transitions = new List<int?[]>();
            
            if(transitions != null)
                _transitions = transitions;
                
            for(int i = 0; i < _transitions.Count; i++)
                if(IsAcceptState(i)) _acceptCount++;
        }
        
        public DeterministicAutomata(IAutomataAlphabet alphabet)
            : this(alphabet, null)
        { }
        #endregion

        #region Build Transitions Table methods
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
            int index = _alphabet.IndexOf(symbol);
            AddTransition(index, fromState, toState);
        }
        
        public void AddTransition(string symbol, int fromState, int toState)
        {
            int index = _alphabet.IndexOf(symbol);
            AddTransition(index, fromState, toState);
        }
                
        public void AcceptState(int index)
        {
            if(index < 0 || index >= _transitions.Count)
                throw new Exception("The Index is out of transitions range ");
                
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
        #endregion

        #region Operations methods
        private bool IsAcceptState(int index)
        {
            return _transitions[index][ACCEPTCOL] == ACCEPT;
        }

        private bool SameAlphabet(DeterministicAutomata automata)
        {
            return automata.Alphabet.Equals(_alphabet);
        }
                
        public DeterministicAutomata Intersection(DeterministicAutomata automata)
        {
            if(!SameAlphabet(automata))
                throw new Exception("The Intersection method need a automata with the same alphabet");
                
            if(Empty || automata.Empty) 
                return this;
                
            var ttable = new List<int?[]>();
            for(int i = 0; i <= _transitions.Count; i++)
            {
                for(int j = 0; j <= automata._transitions.Count; j++)
                {    
                    ttable.Add(new int?[LENGTH]);
                    for(int z = 0; z < _alphabet.Length; z++)
                    {
                        if((i == _transitions.Count || _transitions[i][z].HasValue) 
                            || (j == automata._transitions.Count || automata._transitions[j][z].HasValue))
                        {
                            int to = (i < _transitions.Count && _transitions[i][z].HasValue ? _transitions[i][z].Value : _transitions.Count) * (automata._transitions.Count + 1);
                            to += (j < automata._transitions.Count && automata._transitions[j][z].HasValue ? automata._transitions[j][z].Value : automata._transitions.Count);
                            ttable[(i * (automata._transitions.Count + 1)) + j][z] = to;
                        }
                    }
                
                    if((i < _transitions.Count && IsAcceptState(i)) 
                        && (j < automata._transitions.Count && automata._transitions[j][ACCEPTCOL] == ACCEPT))
                    {
                        //System.Console.WriteLine($"{(i * (automata._transitions.Count + 1)) + j}");
                        ttable[(i * (automata._transitions.Count + 1)) + j][ACCEPTCOL] = ACCEPT;
                    }
                    
                    /*System.Console.Write($"{(i * (automata._transitions.Count + 1)) + j}-");
                    for(int z = 0; z < LENGTH; z++)
                        System.Console.Write($"{ttable[(i * (automata._transitions.Count + 1)) + j][z] ?? 0}, ");
                    System.Console.WriteLine();*/
                }
            }
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        public DeterministicAutomata Union(DeterministicAutomata automata)
        {
            if(!SameAlphabet(automata))
                throw new Exception("The Union method need a automata with the same alphabet");
            
            if(Empty || automata.Empty) 
                return this;
            
            var ttable = new List<int?[]>();
            for(int i = 0; i <= _transitions.Count; i++)
            {
                for(int j = 0; j <= automata._transitions.Count; j++)
                {
                    ttable.Add(new int?[LENGTH]);
                    for(int z = 0; z < _alphabet.Length; z++)
                    {
                        if((i == _transitions.Count || _transitions[i][z].HasValue) 
                            || (j == automata._transitions.Count || automata._transitions[j][z].HasValue))
                        {
                            int to = (i < _transitions.Count && _transitions[i][z].HasValue ? _transitions[i][z].Value : _transitions.Count) * (automata._transitions.Count + 1);
                            to += (j < automata._transitions.Count && automata._transitions[j][z].HasValue ? automata._transitions[j][z].Value : automata._transitions.Count);
                            ttable[(i * (automata._transitions.Count + 1)) + j][z] = to;
                        }
                    }
                
                    if((i < _transitions.Count && _transitions[i][ACCEPTCOL] == ACCEPT) 
                        || (j < automata._transitions.Count && automata._transitions[j][ACCEPTCOL] == ACCEPT))
                        ttable[(i * (automata._transitions.Count + 1)) + j][ACCEPTCOL] = ACCEPT;
                       
                    /*for(int z = 0; z < LENGTH; z++)
                        System.Console.Write($"{ttable[(i * (automata._transitions.Count + 1)) + j][z] ?? 0}, ");
                    System.Console.WriteLine();*/
                }
            }
            
            return new DeterministicAutomata(_alphabet, ttable);
        }

        public DeterministicAutomata Concat(DeterministicAutomata automata)
        {
            if(!SameAlphabet(automata))
                throw new Exception("The Concat method need a automata with the same alphabet");
                
            var ttable = new List<int?[]>();
            for (int i = 0; i < _transitions.Count; i++)
            {
                if (_transitions[i][ACCEPTCOL] == ACCEPT)
                {
                    ttable.Add(new int?[LENGTH]);
                    for (int j = 0; j < ACCEPTCOL; j++)
                        if (_transitions[i][j] == null) ttable[i][j] = _transitions.Count + automata._transitions[0][j];
                        else ttable[i][j] = _transitions[i][j]; 
                }
                else ttable.Add(_transitions[i]);
            }

            for (int i = 0; i < automata._transitions.Count; i++)
            {
                ttable.Add(new int?[LENGTH]);
                for (int j = 0; j < LENGTH; j++)
                    if(automata._transitions[i][j].HasValue)
                        ttable.Last()[j] = (ACCEPTCOL > j ? _transitions.Count : 0) + automata._transitions[i][j].Value;
            }
            
            /*for(int i = 0; i < ttable.Count; i++)
            {
                for (int j = 0; j < ACCEPTCOL; j++)
                    System.Console.Write($"{ttable[i][j]}, ");
                System.Console.WriteLine();
            }*/
            return new DeterministicAutomata(_alphabet, ttable);
        }

        public DeterministicAutomata Closure()
        {
            var ttable = new List<int?[]>();
            for (int i = 0; i < _transitions.Count; i++)
            {
                if (_transitions[i][ACCEPTCOL] == ACCEPT)
                {
                    ttable.Add(new int?[LENGTH]);
                    for (int j = 0; j < ACCEPTCOL; j++)
                        if (_transitions[i][j] == null) ttable[i][j] = _transitions[0][j];
                    ttable[i][ACCEPTCOL] = ACCEPT;
                }
                else ttable.Add(_transitions[i]);
            }

            return new DeterministicAutomata(_alphabet, ttable);
        }
        #endregion

        #region Utilities methods
        public IAutomata Clone()
        {
            return new DeterministicAutomata(_alphabet, new List<int?[]>(_transitions.ToArray()));
        }

        public bool SubsetOf(IAutomata automata)
        {
            return automata is DeterministicAutomata 
                    && !Intersection(automata as DeterministicAutomata).Empty;
        }
        
        public bool Equals(IAutomata other)
        {
            return SubsetOf(other) && other.SubsetOf(this);
        }

        public DeterministicAutomata Optimize()
        {
            if(Optimizer != null) 
                _transitions = Optimizer.Optimize(this, _transitions);
            return this;
        }

        public NonDeterministicAutomata ToNonDeterministic()
        {
            var ttable = new List<List<int>[]>();
            for (int i = 0; i < _transitions.Count; i++)
            {
                ttable.Add(new List<int>[LENGTH+1]);
                for (int j = 0; j < LENGTH-1; j++)
                {
                    if(_transitions[i][j].HasValue)
                        ttable[i][j] = new List<int> { _transitions[i][j].Value };
                }
                if(_transitions[i][LENGTH-1].HasValue)
                    ttable[i][LENGTH] = new List<int> { _transitions[i][LENGTH-1].Value };
            }

            return new NonDeterministicAutomata(_alphabet, ttable);
        }
        #endregion

        #region Match methods
        public bool AnyMatch(params string[] values)
        {
            return AnyMatch<string>(values);
        }
        
        public bool AnyMatch(params char[] values)
        {
            return AnyMatch<char>(values);
        }
        
        private bool AnyMatch<T>(ICollection<T> values)
        {
            int i = 0, j = 0, curr = 0;
            int start = -1;
            int? temp = null;
            for(i = 0; i < values.Count; i++)
            {   
                j = _alphabet.IndexOfValue(values.ElementAt(i));
                if(j == -1 || !(temp = _transitions[curr][j]).HasValue)
                {
                    start = -1;
                    curr = 0;
                    continue;
                }
                
                curr = temp.Value;
                if(start == -1) start = i;
                else if (_transitions[curr][ACCEPTCOL] == ACCEPT) return true;
            }
            
            return false;
        }
        
        public List<int[]> Matches(params string[] values)
        {
            return Matches<string>(values);
        }
        
        public List<int[]> Matches(params char[] values)
        {
            return Matches<char>(values);
        }
        
        private List<int[]> Matches<T>(ICollection<T> values)
        {
            var matches = new List<int[]>();
            int i = 0, j = 0, curr = 0;
            int start = -1, end = -1;
            int? temp = null;
            for(i = 0; i < values.Count; i++)
            {
                j = _alphabet.IndexOfValue(values.ElementAt(i));
                if(j == -1 || !(temp = _transitions[curr][j]).HasValue)
                {
                    if(end > -1) 
                    {
                        i = end - 1;
                        matches.Add(new [] { start, end });
                    }
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
        
        public bool SplitMatch(string value, char separator)
        {
            return IsMatch(value.Split(separator));
        }
        
        public bool IsMatch(params string[] values)
        {
            return IsMatch<string>(values);
        }
        
        public bool IsMatch(params char[] values)
        {
            return IsMatch<char>(values);
        }
        
        private bool IsMatch<T>(ICollection<T> values)
        {
            int i = 0, j = 0, curr = 0;
            int? temp = null;
            for (i = 0; i < values.Count; i++)
            {
                j = _alphabet.IndexOfValue(values.ElementAt(i));
                if (j == -1)
                    return false;

                temp = _transitions[curr][j];
                if (!temp.HasValue)
                    return false;

                curr = temp.Value;
            }

            return _transitions[curr][ACCEPTCOL] == ACCEPT;
        }

        public bool BeginMatch(params string[] values)
        {
            return BeginMatch<string>(values);
        }
        
        public bool BeginMatch(params char[] values)
        {
            return BeginMatch<char>(values);
        }
        
        private bool BeginMatch<T>(ICollection<T> values)
        {
            int i = 0, j = 0, curr = 0;
            int? temp = null;
            for(i = 0; i < values.Count; i++)
            {
                if(_transitions[curr][ACCEPTCOL] == ACCEPT) return true;
                
                j = _alphabet.IndexOfValue(values.ElementAt(i));
                if(j == -1) return false;
                
                temp = _transitions[curr][j];
                if(!temp.HasValue) return false;
                
                curr = temp.Value;
            }
            
            return _transitions[curr][ACCEPTCOL] == ACCEPT;
        }
        
        public bool EndMatch(params string[] values)
        {
            return EndMatch<string>(values);
        }
        
        public bool EndMatch(params char[] values)
        {
            return EndMatch<char>(values);
        }
        
        public bool EndMatch<T>(ICollection<T> values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.Last()[1] == values.Count - 1;
        }
        
        /*public bool Match(string input)
        {
            int i = 0, j = 0, curr = 0, aux = 0;
            int? temp = null;
            while (i < input.Length)
            {
                var nexts = _alphabet.ValuesFrom(input.Substring(i));
                if (!nexts.Any())
                    return false;
                
                for(j = 0; j < nexts.Count; j++)
                {
                    System.Console.WriteLine($"{j} {nexts[j]} {nexts.Count}");
                    aux = _alphabet.IndexOfValue(nexts[j]);
                    temp = _transitions[curr][aux];
                    if (temp.HasValue) 
                    {
                        curr = temp.Value;
                        i += nexts[j].Length;
                        break;
                    }
                }
                
                if(j < nexts.Count -1)
                    return false;
            }

            return _transitions[curr][ACCEPTCOL] == ACCEPT;
        }*/
        #endregion
    }
}
