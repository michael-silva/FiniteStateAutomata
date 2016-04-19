using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class AutomataAlphabet<TKey, TValue> : IAutomataAlphabet<TValue>
    {
        private List<IAutomataAlphabetSymbol<TValue>> _symbols;
        
        public int Count { get { return _symbols.Count; } } 
        
        public AutomataAlphabet()
        { }
        
        public AutomataAlphabet(params TValue[] symbols)
        {
            _symbols = new List<IAutomataAlphabetSymbol<TValue>>();
            for(int i = 0; i < symbols.Length; i++) 
                _symbols.Add(new AutomataAlphabetSymbol<TValue>(symbols[i]));
        }
        
        public AutomataAlphabet(params IAutomataAlphabetSymbol<TValue>[] symbols)
        {
            _symbols = new List<IAutomataAlphabetSymbol<TValue>>(symbols);
        }
        
        public int IndexByValue(TValue value)
        {
            for(int i = 0; i < Count; i++)
                if(_symbols[i].HasValue(value)) return i;
            
            return -1;
        }
        
        public AutomataAlphabet<TKey, TValue> Add(IAutomataAlphabetSymbol<TValue> symbol)
        {
            _symbols.Add(symbol);
            return this;
        }
        
        public AutomataAlphabet<TKey, TValue> Add(TKey key, TValue[] values)
        {
            _symbols.Add(new KeyValuesAutomataAlphabetSymbol<TKey, TValue>(key, values));
            return this;
        }
        
        public AutomataAlphabet<TKey, TValue> Add(TValue value)
        {
            _symbols.Add(new AutomataAlphabetSymbol<TValue>(value));
            return this;
        }
    }    
    
    public class AutomataAlphabet<T> : AutomataAlphabet<T, T>
    {
        public AutomataAlphabet<T> Add(params T[] values)
        {
            Add(new KeyValuesAutomataAlphabetSymbol<T, T>(values[0], values));
            return this;
        }
    }
    
    public class AutomataAlphabet : AutomataAlphabet<char>
    { 
        public AutomataAlphabet()
        { }
        
        /*public AutomataAlphabet(string alphabet)
            : base(alphabet.ToCharArray())
        { }*/
    }
}