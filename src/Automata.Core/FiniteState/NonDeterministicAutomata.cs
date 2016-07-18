using System;
using System.Linq;
using System.Collections.Generic;
using Automata.Core.Interfaces;

namespace Automata.Core.FiniteState
{
    public class NonDeterministicAutomata : IAutomata, IAutomataTableBuilder
    {
        #region Properties and Constants
        private const int ACCEPT = 1;
        private readonly int ACCEPTCOL;
        private readonly int EPSILONCOL;
        private readonly int LENGTH;
        
        private int _acceptCount = 0;
        private int _firstAccept = 0; 
        private IAutomataAlphabet _alphabet;
        private List<List<int>[]> _transitions;
        
        public IAutomataOptimizer<List<List<int>[]>> Optimizer { get; set; }
        public int Epsilon { get { return EPSILONCOL; } }
        public IAutomataAlphabet Alphabet { get { return _alphabet; } }
        public bool Acceptance { get { return _acceptCount > 0; } }
        public bool Empty { get { return _alphabet.Length == 0; } }
        public bool Totality { get { return _acceptCount == _transitions.Count; } }
        #endregion

        #region Constructors
        public NonDeterministicAutomata(IAutomataAlphabet alphabet, List<List<int>[]> transitions)
        {
            EPSILONCOL = alphabet.Length;
            ACCEPTCOL = alphabet.Length + 1;
            LENGTH = alphabet.Length + 2;

            _alphabet = alphabet;
            
            _transitions = new List<List<int>[]>();
            if(transitions != null)
            {
                for(int i = 0; i < transitions.Count; i++)
                {
                    // string line = "";
                    if(transitions[i] != null)
                    {
                        _transitions.Add(new List<int>[LENGTH]);
                        for(int j = 0; j < transitions[i].Length; j++)
                        {
                            // line += "[";
                            if(transitions[i][j] != null)
                            {
                                _transitions[i][j] = new List<int>();
                                for(int z = 0; z < transitions[i][j].Count; z++)
                                {
                                    // line += $"{transitions[i][j][z]}";
                                    // if(z < transitions[i][j].Count -1) 
                                    //     line += ",";
                                    _transitions[i][j].Add(transitions[i][j][z]);
                                }
                            }
                            //line += "], ";
                        }
                        //System.Console.WriteLine(line);
                    }
                }
            }
            
            for(int i = 0; i < _transitions.Count; i++)
                if(_transitions[i][ACCEPTCOL] != null && _transitions[i][ACCEPTCOL].Any() && _transitions[i][ACCEPTCOL][0] == ACCEPT) _acceptCount++;
        }
        
        public NonDeterministicAutomata(IAutomataAlphabet alphabet)
            : this(alphabet, new List<List<int>[]>())
        { }
        #endregion

        #region Build Transitions Table methods
        public NonDeterministicAutomata State(params int[][] values)
        {
            if(values == null || values.Length != _alphabet.Length) 
                throw new Exception($"A state need to set transitions to all {_alphabet.Length} alphabet symbols");
            
            var temp = new List<int>[LENGTH];
            for(int j = 0; j < values.Length; j++)
                temp[j] = new List<int>(values[j]);
            temp[ACCEPTCOL] = new List<int>();
            temp[EPSILONCOL] = new List<int>();
                
            _transitions.Add(temp);
            return this;
        }

        public void AddEpsilon(int fromState, int toState)
        {
            for(int i = _transitions.Count; i <= Math.Max(fromState, toState); i++)
                _transitions.Add(new List<int>[LENGTH]);

            if(_transitions[fromState][EPSILONCOL] == null)
                _transitions[fromState][EPSILONCOL] = new List<int>();

            _transitions[fromState][EPSILONCOL].Add(toState);
        }
        
        public void AddTransition(int symbolIndex, int fromState, int toState)
        {
            if(symbolIndex >= LENGTH)
                throw new Exception("The symbolIndex is out of range");
            
            for(int i = _transitions.Count; i <= Math.Max(fromState, toState); i++)
                _transitions.Add(new List<int>[LENGTH]);

            if(_transitions[fromState][symbolIndex] == null)
                _transitions[fromState][symbolIndex] = new List<int>();

            _transitions[fromState][symbolIndex].Add(toState);
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
                
            _transitions[index][ACCEPTCOL] = new List<int>() { ACCEPT };
            _acceptCount++;
        }
        
        public void AcceptLast()
        {
            int index = _transitions.Count - 1;
            AcceptState(index);
        }
        #endregion
        
        #region Operations methods
        private bool SameAlphabet(NonDeterministicAutomata automata)
        {
            return automata.Alphabet.Equals(_alphabet);
        }
        
        public NonDeterministicAutomata Intersection(DeterministicAutomata automata)
        {
            return ToDeterministic().Intersection(automata).ToNonDeterministic();
        }

        public NonDeterministicAutomata Intersection(NonDeterministicAutomata automata)
        {
            var d = automata.ToDeterministic();
            return Intersection(d);
        }
        
        public NonDeterministicAutomata Union(NonDeterministicAutomata automata)
        {
            if(Empty || automata.Empty) 
                return this;
            
            var ttable = new List<List<int>[]>() 
            {
                new List<int>[LENGTH]
            };
            
            ttable[0][EPSILONCOL] = new List<int>() { 1, _transitions.Count + 1 };

            for(int i = 0; i < _transitions.Count; i++)
            {
                if(_transitions[i] != null)
                {
                    ttable.Add(new List<int>[LENGTH]);
                    for(int j = 0; j < _transitions[i].Length - 1; j++)
                    {
                        if(_transitions[i][j] != null)
                        {
                            ttable[i+1][j] = new List<int>();
                            for(int z = 0; z < _transitions[i][j].Count; z++)
                                ttable[i + 1][j].Add(_transitions[i][j][z] + 1);
                        }
                    }
                    
                    if(_transitions[i][ACCEPTCOL] != null)
                        ttable[i + 1][ACCEPTCOL] = new List<int>() { _transitions[i][ACCEPTCOL][0] };
                }
            }
            
            for(int i = 0; i < automata._transitions.Count; i++)
            {
                if(automata._transitions[i] != null)
                {
                    ttable.Add(new List<int>[LENGTH]);
                    for(int j = 0; j < automata._transitions[i].Length; j++)
                    {
                        if(automata._transitions[i][j] != null)
                        {
                            ttable[i + _transitions.Count + 1][j] = new List<int>();
                            for(int z = 0; z < automata._transitions[i][j].Count; z++)
                                ttable[i + _transitions.Count + 1][j].Add(automata._transitions[i][j][z] + _transitions.Count + 1);
                        }
                    }

                    if(automata._transitions[i][ACCEPTCOL] != null)
                        ttable[i + _transitions.Count + 1][ACCEPTCOL] = new List<int>() { automata._transitions[i][ACCEPTCOL][0] };
                }
            }
            
            return new NonDeterministicAutomata(_alphabet, ttable);
        }
        
        public NonDeterministicAutomata Concat(NonDeterministicAutomata automata)
        {
            var ttable = new List<List<int>[]>();
            
            ttable.Add(new List<int>[LENGTH]);
            for(int i = 0; i < _transitions.Count; i++)
            {
                if(_transitions[i] != null)
                {
                    ttable.Add(new List<int>[LENGTH]);
                    for(int j = 0; j < _transitions[i].Length; j++)
                    {
                        if(_transitions[i][j] != null)
                        {
                            ttable[i][j] = new List<int>();
                            if(j == ACCEPTCOL && _transitions[i][j].Any() && _transitions[i][j][0] == ACCEPT) 
                            {
                                if(ttable[i][EPSILONCOL] == null)
                                    ttable[i][EPSILONCOL] = new List<int>();
                                ttable[i][EPSILONCOL].Add(_transitions.Count);
                            }
                            else
                            {   
                                for(int z = 0; z < _transitions[i][j].Count; z++)
                                {
                                    ttable[i][j].Add(_transitions[i][j][z]);
                                }
                            }
                        }
                    }
                }
            }
            
            for(int i = 0; i < automata._transitions.Count; i++)
            {
                if(automata._transitions[i] != null)
                {
                    ttable.Add(new List<int>[LENGTH]);
                    for(int j = 0; j < automata._transitions[i].Length; j++)
                    {
                        if(automata._transitions[i][j] != null)
                        {
                            ttable[i + _transitions.Count][j] = new List<int>();
                            for(int z = 0; z < automata._transitions[i][j].Count; z++)
                                ttable[i + _transitions.Count][j].Add(automata._transitions[i][j][z] + _transitions.Count);
                        }
                    }

                    if(automata._transitions[i][ACCEPTCOL] != null)
                        ttable[i + _transitions.Count][ACCEPTCOL] = new List<int>() { automata._transitions[i][ACCEPTCOL][0] };
                }
            }
            
            return new NonDeterministicAutomata(_alphabet, ttable);
        }
        
        public NonDeterministicAutomata Closure()
        {
            if(Empty) return this;
            
            var ttable = new List<List<int>[]>();
            
            for(int i = 0; i < _transitions.Count; i++)
            {
                if(_transitions[i] != null)
                {
                    ttable.Add(new List<int>[LENGTH]);
                    for(int j = 0; j < _transitions[i].Length; j++)
                    {
                        if(_transitions[i][j] != null)
                        {
                            ttable[i][j] = new List<int>();
                            for(int z = 0; z < _transitions[i][j].Count; z++)
                                ttable[i][j].Add(_transitions[i][j][z]);
                        }
                    }
                }
            }

            if(ttable[ttable.Count - 1][EPSILONCOL] == null)
                ttable[ttable.Count - 1][EPSILONCOL] = new List<int>();
            ttable[ttable.Count - 1][EPSILONCOL].Add(0);
            
            return new NonDeterministicAutomata(_alphabet, ttable);
        }
        #endregion

        #region Utilities methods
        public IAutomata Clone()
        {
            var ttable = new List<List<int>[]>();
            for(int i = 0; i < _transitions.Count; i++)
            {
                if(_transitions[i] != null)
                {
                    ttable.Add(new List<int>[LENGTH]);
                    for(int j = 0; j < _transitions[i].Length; j++)
                        if(_transitions[i][j] != null)
                        {
                            ttable[i][j] = new List<int>();
                            for(int z = 0; z < _transitions[i][j].Count; z++)
                                ttable[i][j].Add(_transitions[i][j][z]);
                        }
                }
            }

            return new NonDeterministicAutomata(_alphabet, ttable);
        }

        public bool SubsetOf(IAutomata automata)
        {
            return automata is NonDeterministicAutomata 
                    && !Intersection(automata as NonDeterministicAutomata).Empty;
        }
        
        public bool Equals(IAutomata other)
        {
            return other is NonDeterministicAutomata 
                    && SubsetOf(other as NonDeterministicAutomata) 
                    && (other as NonDeterministicAutomata).SubsetOf(this);
        }

        public NonDeterministicAutomata Optimize()
        {
            if(Optimizer != null) 
                _transitions = Optimizer.Optimize(this, _transitions);
            return this;
        }

        public DeterministicAutomata ToDeterministic()
        {   
            int i = 0;
            int length = LENGTH - 1;
            var ttable = new List<int?[]>() { new int?[length] };
            var subsets = new List<List<int>>();
            subsets.Add(GetEpsilons(0));
            subsets[0].Add(0);
            while(i < subsets.Count())
            {
                for(int z = 0; z < subsets[i].Count; z++)
                    if(_transitions[subsets[i][z]][ACCEPTCOL] != null && _transitions[subsets[i][z]][ACCEPTCOL][0] == ACCEPT) ttable[i][length - 1] = ACCEPT;

                for(int j = 0; j < _alphabet.Length; j++)
                {
                    var subs = GetSubset(j, subsets[i]);
                    if(subs != null && subs.Any())
                    {
                        int index = -1;
                        for(int x = 0; x < subsets.Count; x++)
                        {
                            if(subs.Count == subsets[x].Count)
                            {
                                for(int z = 0; z < subsets[x].Count && subsets[x][z] == subs[z]; z++)
                                    if(z == subsets[x].Count - 1) index = x;
                                
                                if(index >= 0) break;
                            }
                        }
                        
                        if(index == -1)
                        {
                            index = subsets.Count;
                            subsets.Add(subs);
                        }
                        ttable[i][j] = index;
                    }
                }
                // string line = "";
                // for(int j = 0; j < length; j++)
                //     line += $"[{ttable[i][j]}], ";
                // System.Console.WriteLine(line);
                i++;
                ttable.Add(new int?[length]);
            }
            
            // System.Console.WriteLine("Subsets");
            // for(i = 0; i < subsets.Count; i++) 
            // {
            //     string l = "";
            //     for(int j = 0; j < subsets[i].Count; j++)
            //         l += $"{subsets[i][j]}, ";
            //     System.Console.WriteLine(l);
            // }
            return new DeterministicAutomata(_alphabet, ttable);
        }
        #endregion
        
        #region Match methods
        private List<int> GetSubset(int finded, List<int> subset)
        {
            var moves = new List<int>();
            for(int j = 0; j < subset.Count; j++)
            {
                var aux = _transitions[subset[j]][finded];
                if(aux != null)
                    moves.AddRange(aux);
                
                aux = GetEpsilons(subset[j]);
                if(aux != null)
                {
                    for(int i = 0; i < aux.Count; i++)
                        if(_transitions[aux[i]][finded] != null) moves.AddRange(_transitions[aux[i]][finded]);
                }
            }
            return moves;
        }

        private List<int> GetEpsilons(int state)
        {
            var epsilons = new List<int>();
            
            if(_transitions[state][EPSILONCOL] != null)
                epsilons.AddRange(_transitions[state][EPSILONCOL]);

            for(int j = 0; j < epsilons.Count; j++)
            {
                if(_transitions[epsilons[j]][EPSILONCOL] != null)
                    epsilons.AddRange(_transitions[epsilons[j]][EPSILONCOL]);
            }
            return epsilons;
        }

        private void GetMovesFromEpsilons(int finded, List<int> epsilons, List<int> moves)
        {
            int count = epsilons.Count;
            for(int j = 0; j < count; j++)
            {
                var aux = _transitions[epsilons[j]][EPSILONCOL];
                if(aux != null)
                {
                    count += aux.Count;
                    for(int z = 0; z < aux.Count; z++)
                        epsilons.Add(aux[z]);
                }
                
                aux = _transitions[epsilons[j]][finded];
                if(aux != null)
                {
                    for(int z = 0; z < aux.Count; z++)
                        moves.Add(aux[z]);
                }
            }
        }

        public bool AnyMatch(params string[] values)
        {
            return AnyMatch<string>(values);
        }
        
        public bool AnyMatch(params char[] values)
        {
            return AnyMatch<char>(values);
        }

        public bool AnyMatch<T>(params T[] values)
        {
            if(values.Length < _firstAccept) return false;
            
            int find = 0, temp = 0, i = 0;;
            var backup = new Stack<Stack<int>>();
            var curr = new Stack<int>();
            curr.Push(0);
            
            while(i < values.Length)
            {
                temp = curr.Pop();
                if(_transitions[temp][ACCEPTCOL] != null && _transitions[temp][ACCEPTCOL][0] == ACCEPT) return true;

                find = _alphabet.IndexOfValue(values[i]);
                if(find == -1) 
                {
                    curr.Clear();
                    backup.Clear();
                    curr.Push(0);
                    i++;
                    continue;
                }

                var moves = new List<int>();
                var epsilons = _transitions[temp][EPSILONCOL];
                if(epsilons != null) 
                {
                    GetMovesFromEpsilons(find, epsilons, moves);
                }

                var nexts = _transitions[temp][find];
                if(nexts != null) 
                {
                    for(int j = 0; j < nexts.Count; j++)
                        moves.Add(nexts[j]);
                }

                if(moves.Count > 0) 
                {
                    backup.Push(curr);
                    curr = new Stack<int>(moves);
                    i++;
                }
                else 
                {
                    while(!curr.Any())
                    {
                        if(!backup.Any()) 
                        {
                            curr.Push(0);
                            i++;
                            continue;
                        }
                        curr = backup.Pop();
                        i--;
                    }
                }
            }
            
            if(curr.Count > 0)
                temp = curr.Pop();

            return _transitions[temp][ACCEPTCOL] != null && _transitions[temp][ACCEPTCOL][0] == ACCEPT;
        }
        
        public List<int[]> Matches(params string[] values)
        {
            return Matches<string>(values);
        }
        
        public List<int[]> Matches(params char[] values)
        {
            return Matches<char>(values);
        }
        
        public List<int[]> Matches<T>(params T[] values)
        {
            if(values.Length < _firstAccept) return null;
            
            var matches = new List<int[]>();
            int start = -1, accept = -1;
            
            int find = 0, temp = 0, i = 0;;
            var backup = new Stack<Stack<int>>();
            var curr = new Stack<int>();
            curr.Push(0);
            
            while(i < values.Length)
            {
                temp = curr.Pop();
                if(start == -1)
                    start = i;
                if(_transitions[temp][ACCEPTCOL] != null && _transitions[temp][ACCEPTCOL][0] == ACCEPT)
                    accept = i;

                find = _alphabet.IndexOfValue(values[i]);
                if(find == -1) 
                {
                    curr.Clear();
                    backup.Clear();
                    curr.Push(0);
                    if(accept > 0) 
                    {
                        matches.Add(new [] { start, accept - 1 });
                        i = accept;
                        accept = -1;
                    }
                    else i++;
                    start = -1;
                    continue;
                }

                var moves = new List<int>();
                var epsilons = _transitions[temp][EPSILONCOL];
                if(epsilons != null) 
                {
                    GetMovesFromEpsilons(find, epsilons, moves);
                }

                var nexts = _transitions[temp][find];
                if(nexts != null) 
                {
                    for(int j = 0; j < nexts.Count; j++)
                        moves.Add(nexts[j]);
                }

                if(moves.Count > 0) 
                {
                    backup.Push(curr);
                    curr = new Stack<int>(moves);
                    i++;
                }
                else 
                {
                    while(!curr.Any())
                    {
                        if(!backup.Any()) 
                        {
                            curr.Push(0);
                            if(accept > 0) 
                            {
                                matches.Add(new [] { start, accept - 1 });
                                i = accept;
                                accept = -1;
                            }
                            else i++;
                            start = -1;
                            continue;
                        }
                        curr = backup.Pop();
                        i--;
                    }
                }
            }
            
            if(curr.Count > 0)
                temp = curr.Pop();

            if(_transitions[temp][ACCEPTCOL] != null && _transitions[temp][ACCEPTCOL][0] == ACCEPT)
                matches.Add(new [] { start, values.Length - 1 });
            
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

        public bool IsMatch<T>(params T[] values)
        {
            if(values.Length < _firstAccept) return false;
            
            int find = 0, temp = 0, i = 0;;
            var backup = new Stack<Stack<int>>();
            var curr = new Stack<int>();
            curr.Push(0);
            
            while(i < values.Length)
            {
                //System.Console.WriteLine($"start loop ({i}) with currents {curr.Count}");
                temp = curr.Pop();
                find = _alphabet.IndexOfValue(values[i]);
                if(find == -1) return false;

                //System.Console.WriteLine($"{temp} = {values[i]}({find})");
                var moves = new List<int>();
                var epsilons = _transitions[temp][EPSILONCOL];
                if(epsilons != null) 
                {
                    int count = epsilons.Count;
                    //System.Console.WriteLine($"with epsilons {count}");
                    for(int j = 0; j < count; j++)
                    {
                        var aux = _transitions[epsilons[j]][EPSILONCOL];
                        if(aux != null)
                        {
                            count += aux.Count;
                            //System.Console.WriteLine($"more {count}");
                            for(int z = 0; z < aux.Count; z++)
                                epsilons.Add(aux[z]);
                        }
                        
                        aux = _transitions[epsilons[j]][find];
                        if(aux != null)
                            for(int z = 0; z < aux.Count; z++)
                                moves.Add(aux[z]);
                    }

                    /*System.Console.WriteLine($"moves {moves.Count}");
                    for(int z = 0; z < moves.Count; z++)
                        System.Console.Write($"{moves[z]},");
                    System.Console.WriteLine($"");*/
                }

                var nexts = _transitions[temp][find];
                if(nexts != null) 
                {
                    //System.Console.WriteLine($"with nexts {nexts.Count}");
                    for(int j = 0; j < nexts.Count; j++)
                        moves.Add(nexts[j]);
                }

                if(moves.Count > 0) 
                {
                    /*System.Console.WriteLine($"moves {moves.Count}");
                    for(int z = 0; z < moves.Count; z++)
                        System.Console.Write($"{moves[z]},");
                    System.Console.WriteLine($"");*/
                    backup.Push(curr);
                    //System.Console.WriteLine($"move to next curr {curr.Count} backups {backup.Count}");
                    curr = new Stack<int>(moves);
                    i++;
                }
                else 
                {
                    //System.Console.WriteLine($"hasn't moves curr({curr.Any()}) backup({backup.Any()})");
                    while(!curr.Any())
                    {
                        if(!backup.Any()) return false;
                        curr = backup.Pop();
                        //System.Console.WriteLine($"move to prev with currs {curr.Count} backups {backup.Count}");
                        i--;
                    }
                }
                //System.Console.WriteLine($"loop");
            }
            
            if(curr.Count > 0)
                temp = curr.Pop();

            //System.Console.WriteLine($"finished {temp}");
            return _transitions[temp][ACCEPTCOL] != null && _transitions[temp][ACCEPTCOL][0] == ACCEPT;
        }

        public bool BeginMatch(params string[] values)
        {
            return BeginMatch<string>(values);
        }
        
        public bool BeginMatch(params char[] values)
        {
            return BeginMatch<char>(values);
        }

        public bool BeginMatch<T>(params T[] values)
        {
            if(values.Length < _firstAccept) return false;
            
            int find = 0, temp = 0, i = 0;;
            var backup = new Stack<Stack<int>>();
            var curr = new Stack<int>();
            curr.Push(0);
            
            while(i < values.Length)
            {
                temp = curr.Pop();
                if(_transitions[temp][ACCEPTCOL] != null && _transitions[temp][ACCEPTCOL][0] == ACCEPT) return true;

                find = _alphabet.IndexOfValue(values[i]);
                if(find == -1) return false;

                var moves = new List<int>();
                var epsilons = _transitions[temp][EPSILONCOL];
                if(epsilons != null) 
                {
                    GetMovesFromEpsilons(find, epsilons, moves);
                }

                var nexts = _transitions[temp][find];
                if(nexts != null) 
                {
                    for(int j = 0; j < nexts.Count; j++)
                        moves.Add(nexts[j]);
                }

                if(moves.Count > 0) 
                {
                    backup.Push(curr);
                    curr = new Stack<int>(moves);
                    i++;
                }
                else 
                {
                    while(!curr.Any())
                    {
                        if(!backup.Any()) return false;
                        curr = backup.Pop();
                        i--;
                    }
                }
            }
            
            if(curr.Count > 0)
                temp = curr.Pop();

            return _transitions[temp][ACCEPTCOL] != null && _transitions[temp][ACCEPTCOL][0] == ACCEPT;
        }
        
        public bool EndMatch(params string[] values)
        {
            return EndMatch<string>(values);
        }
        
        public bool EndMatch(params char[] values)
        {
            return EndMatch<char>(values);
        }
        
        public bool EndMatch<T>(params T[] values)
        {
            var matches = Matches(values);
            return matches.Count > 0 && matches.Last()[1] == values.Length - 1;
        }

        /*public bool Match(string input)
        {
            return false;
        }*/
        #endregion
    }
}
