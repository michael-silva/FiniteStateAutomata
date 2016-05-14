using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class AutomataGroupAlphabet<T> : IAutomataAlphabet<T>
    {
        private List<GroupAutomataAlphabetSymbol<T>> _symbols;
        
        public int Count { get { return _symbols.Count; } } 
        
        public AutomataGroupAlphabet()
        { 
            _symbols = new List<GroupAutomataAlphabetSymbol<T>>();
        }
        
        public AutomataGroupAlphabet(params GroupAutomataAlphabetSymbol<T>[] symbols)
        {
            _symbols = new List<GroupAutomataAlphabetSymbol<T>>(symbols);
        }
        
        public int IndexByValue(T value)
        {
            for(int i = 0; i < Count; i++)
                if(_symbols[i].HasValue(value)) return i;
            
            return -1;
        }
        
        public AutomataGroupAlphabet<T> Add(GroupAutomataAlphabetSymbol<T> symbol)
        {
            _symbols.Add(symbol);
            return this;
        }
        
        public AutomataGroupAlphabet<T> Add(params T[] values)
        {
            return Add(new GroupAutomataAlphabetSymbol<T>(values));
        }
    }
}