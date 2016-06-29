using System;
using System.Linq;
using System.Collections.Generic;
using Automata.Core.Interfaces;

namespace Automata.Core.FiniteState
{
    public class NonDeterministicAutomata : IAutomata
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
        
        public int Epsilon { get { return EPSILONCOL; } }
        public IAutomataAlphabet Alphabet { get { return _alphabet; } }
        public bool Acceptance { get { return _acceptCount > 0; } }
        public bool Empty { get { return _alphabet.Length == 0; } }
        public bool Totality { get { return _acceptCount == _transitions.Count; } }
        #endregion

        #region Constructors
        private NonDeterministicAutomata(IAutomataAlphabet alphabet, List<List<int>[]> transitions)
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
                    if(transitions[i] != null)
                    {
                        _transitions.Add(new List<int>[LENGTH]);
                        for(int j = 0; j < transitions[i].Length; j++)
                        {
                            System.Console.Write("[");
                            if(transitions[i][j] != null)
                            {
                                _transitions[i][j] = new List<int>();
                                for(int z = 0; z < transitions[i][j].Count; z++)
                                {
                                    System.Console.Write($"{transitions[i][j][z]}");
                                    _transitions[i][j].Add(transitions[i][j][z]);
                                }
                            }
                            System.Console.Write("], ");
                        }
                        System.Console.WriteLine("");
                    }
                }

                if(_transitions.Count > 0 && _transitions[0][EPSILONCOL] != null && _transitions[0][EPSILONCOL].Count > 0)   
                    System.Console.WriteLine($"{_transitions[0][EPSILONCOL][0]} {_transitions[0][EPSILONCOL].Count}");
            }
            
            for(int i = 0; i < _transitions.Count; i++)
                if(_transitions[i][ACCEPTCOL] != null && _transitions[i][ACCEPTCOL].Any() && _transitions[i][ACCEPTCOL][0] == ACCEPT) _acceptCount++;

            if(_transitions.Count > 0 && _transitions[0][EPSILONCOL] != null && _transitions[0][EPSILONCOL].Count > 0)   
                System.Console.WriteLine($"{_transitions[0][EPSILONCOL][0]} {_transitions[0][EPSILONCOL].Count}");
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
        
        public NonDeterministicAutomata Reverse()
        {
            var ttable = new List<List<int>[]>();
            /*var alphabet = _alphabet.GetNew();
            
            for(int i = 0; i < alphabet.Length; i++)
                alphabet.Add(_alphabet.Length - i);
            
            for(int i = 0; i < _transitions.Count; i++)
            {
                var j = _transitions.Count - i;
                ttable[j][ACCEPTCOL] = ACCEPT - ttable[j][ACCEPTCOL];
            }
            
            return new NonDeterministicAutomata(alphabet, ttable);*/
            return null;
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

        public bool SubsetOf(NonDeterministicAutomata automata)
        {
            return !Intersection(automata).Empty;
        }
        
        public bool Equals(IAutomata other)
        {
            return other is NonDeterministicAutomata 
                    && SubsetOf(other as NonDeterministicAutomata) 
                    && (other as NonDeterministicAutomata).SubsetOf(this);
        }

        public NonDeterministicAutomata Optimize()
        {
            //inject optimize algorithm
            return this;
        }

        public DeterministicAutomata ToDeterministic()
        {
            /*
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
            
            return new DeterministicAutomata(_alphabet, ttable);*/
            return null;
        }
        #endregion
        
        #region Match methods
        
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

        public bool Match(string input)
        {
            return false;
        }
        #endregion
    }
}
