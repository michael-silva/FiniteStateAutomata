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
        private readonly int LENGTH;
        
        private int _acceptCount = 0;
        private int _firstAccept = 0; 
        private IAutomataAlphabet<T> _alphabet;
        private List<int?[]> _transitions;
        
        public bool Acceptance { get { return _acceptCount > 0; } }
        public bool Empty { get { return _alphabet.Count > 0; } }
        public bool Totality { get { return _acceptCount == _transitions.Length; } }
        
        private NonDeterministicAutomata(IAutomataAlphabet<T> alphabet, List<List<int>[]> transitions)
        {
            EPSILONCOL = alphabet.Length;
            ACCEPTCOL = alphabet.Length + 1;
            LENGTH = alphabet.Length + 2;
            _alphabet = alphabet;
            _transitions = transitions;
        }
        
        public NonDeterministicAutomata(IAutomataAlphabet<T> alphabet)
            : this(alphabet, new List<List<int>[]>())
        { }
        
        private List<int?> GetSubset(int?[] subset, int symbolIndex)
        {
            var result = new List<int?>();
            for(int i = 0; i < subset.Length; i++)
            {
                result.Add(subset[i][symbolIndex]);
                for(int i = 0; i < subset[i][EPSILONCOL].Count; i++)
                {
                    result.AddRange(getEpsilon(subset[i][epsilon_col]))
                }
            }

            return result;
        }
        
        private List<int> GetEpsilon(List<int> subset)
        {
            var result = new List<int?>();
            for(int i = 0; i < subset.Count; i++)
            {
                result.Add(subset[i]);

                if(!subset[EPSILONCOL].HasValue())
                    result.AddRange(subset[EPSILONCOL]);
            }

            return result;
        }
        
        public IAutomata<T> AddState()
        {
            _transitions.Add(new List<int>[LENGTH]);
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
            if(index < _firstAccept)
                _firstAccept = index;
                
            _transitions[index][ACCEPTCOL] = new List<int>() { ACCEPT };
            _acceptCount++;
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
        
        public DeterministicAutomata<T> ToDeterministic()
        {
            List<List<int>[]> _transitions;
            var ttable = new List<int?[]>();
            int i = 0;
            var list = new List<int?>() { i };
            list.AddRange(GetEpsilon(_transitions[0][EPSILONCOL]));
            
            while(i < ttable.Count)
            {
                for(int j = 0; j < _alphabet.Count; j++)
                {
                    var next = GetSubset(list[i], j);

                    if(next.Count > 0)
                    {
                        temp = list.index(next);

                        if(temp == -1)
                        {
                            temp = list.Count;
                            
                            list.Add(next);
                            ttable.Add(new int?[LENGTH]);
                        }

                        ttable[i][j] = temp;
                    }
                }
            }
            
            return new DeterministicAutomata(_alphabet, ttable);
        }
        
        public IAutomata<T> Reverse()
        {
            var ttable = new List<List<int>[]>();
            var alphabet = _alphabet.GetNew();
            
            for(int i = 0; i < alphabet.Count; i++)
                alphabet.Add(_alphabet.Count - i);
            
            for(int i = 0; i < _transitions.Count; i++)
            {
                var j = transitions.length - i
                ttable[j][accept_col] = ACCEPT - ttable[j][accept_col]
            }
            
            return new NonDeterministicAutomata(alphabet, ttable);
        }
        
        public IAutomata<T> Concat(IAutomata<T> automata)
        {
            var ttable = new List<List<int>[]>();
            
            ttable.Add(new List<int>[LENGTH]);
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
                ttable.Add(new List<int>[LENGTH]);
                ttable.Add(automata._transitions[i]);
            }
            
            return new NonDeterministicAutomata(_alphabet, ttable);
        }
        
        public IAutomata<T> Difference(IAutomata<T> automata)
        {
            return ToDeterministic().Difference().ToNonDeterministic();
        }
        
        public IAutomata<T> Intersection(IAutomata<T> automata)
        {
            return ToDeterministic().Intersection().ToNonDeterministic();
        }
        
        public IAutomata<T> Union(IAutomata<T> automata)
        {
            if(Empty || automata.Empty) 
                return this;
                
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
            if(Empty || automata.Empty) 
                return this;
                
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
            if(values.Length < _firstAccept) return false;
            
            int find = 0, temp = 0, i = 0;;
            var backup = new Queue<Queue<int>>();
            var curr = new Queue<int>();
            
            while(i <= values.Length)
            {
                if(i + 1 == values.length && _transitions[curr][ACCEPTCOL] == ACCEPT) 
                    return true;
                
                if(!curr.Count() > 0) 
                {
                    if(backup.Count() > 0)
                    {
                        curr = backup.Dequeue();
                        i--;
                    }
                    else return false;
                }
                
                temp = curr.Dequeue();
                find = _alphabet.IndexByValue(values[i]);
                if(find == -1) return false;

                var epsilons = _transitions[temp][EPSILONCOL];
                var nexts = _transitions[temp][find]; 
                for(int j = 0; j < epsilons.Count; j++)
                {
                    nexts.Add(_transitions[temp][epsilons[i]]);
                    if(nexts.Count() == 0) continue;
                }
                
                backup.Enqueue(new Queue<int>(nexts));
                i++;
            }
            
            return false;
        }
        
        public bool EndMatch(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches[matches.Keys.Max()] == values.Length - 1;
        }
        
        public bool BeginMatch(params T[] values)
        {
            if(values.Length < _firstAccept) return false;
            
            int find = 0, temp = 0, i = 0;;
            var backup = new Queue<Queue<int>>();
            var curr = new Queue<int>();
            
            while(i <= values.Length)
            {
                if(_transitions[curr][ACCEPTCOL] == ACCEPT) 
                    return true;
                
                if(curr.Count() == 0) 
                {
                    if(backup.Count() > 0)
                    {
                        curr = backup.Dequeue();
                        i--;
                    }
                    else return false;
                }
                
                temp = curr.Dequeue();
                find = _alphabet.IndexByValue(values[i]);
                if(find == -1) return false;

                var epsilons = _transitions[temp][EPSILONCOL];
                var nexts = _transitions[temp][find];
                for(int j = 0; j < epsilons.Count; j++)
                {
                    nexts.Add(_transitions[temp][epsilons[i]]);
                    if(nexts.Count() == 0) continue;
                }
                
                backup.Enqueue(new Queue<int>(nexts));
                i++;
            }
            
            return false;
        }
        
        public bool AnyMatch(params T[] values)
        {
            if(values.Length < _firstAccept) return false;
            
            int find = 0, temp = 0, i = 0;;
            var backup = new Queue<Queue<int>>();
            var curr = new Queue<int>();
            
            while(i <= values.Length)
            {
                if(_transitions[curr][ACCEPTCOL] == ACCEPT) 
                    return true;
                
                if(!curr.Count() > 0) 
                {
                    if(backup.Count() > 0)
                    {
                        curr = backup.Dequeue();
                        i--;
                    }
                    else
                    {
                        i++;
                        continue;
                    }
                }
                
                temp = curr.Dequeue();
                find = _alphabet.IndexByValue(values[i]);
                if(find == -1) return false;

                var epsilons = _transitions[temp][EPSILONCOL];
                var nexts = _transitions[temp][find];
                for(int j = 0; j < epsilons.Count; j++)
                {
                    nexts.Add(_transitions[temp][epsilons[i]]);
                    if(nexts.Count() == 0) continue;
                }
                
                backup.Enqueue(new Queue<int>(nexts));
                i++;
            }
            
            return false;
        }
        
        public List<int[]> Matches(params T[] values)
        {
            if(values.Length < _firstAccept) return false;
            
            var matches = new List<int[]>();
            int start = -1, accept = -1;
            int find = 0, temp = 0, i = 0;;
            var backup = new Queue<Queue<int>>();
            var curr = new Queue<int>();
            
            while(i <= values.Length)
            {
                if(_transitions[curr][ACCEPTCOL] == ACCEPT) 
                    accept = i;
                
                if(!curr.Count() > 0) 
                {
                    if(backup.Count() > 0)
                    {
                        curr = backup.Dequeue();
                        i--;
                    }
                    else
                    {
                        i++;
                        continue;
                    }
                }
                
                temp = curr.Dequeue();
                find = _alphabet.IndexByValue(values[i]);
                if(find == -1) return false;

                var epsilons = _transitions[temp][EPSILONCOL];
                var nexts = _transitions[temp][find];
                for(int j = 0; j < epsilons.Count; j++)
                {
                    nexts.Add(_transitions[temp][epsilons[i]]);
                    if(nexts.Count() == 0) continue;
                }
                
                backup.Enqueue(new Queue<int>(nexts));
                i++;
            }
            
            return matches;
        }
    }
}
