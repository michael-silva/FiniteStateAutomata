using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class AutomataKeyValuesAlphabet<TKey, TValue> : IAutomataAlphabet<TValue>
    {
        private List<KeyValuesAutomataAlphabetSymbol<TKey, TValue>> _symbols;
        
        public int Count { get { return _symbols.Count; } } 
        
        public AutomataKeyValuesAlphabet()
        { 
            _symbols = new List<KeyValuesAutomataAlphabetSymbol<TKey, TValue>>();
        }
        
        public AutomataKeyValuesAlphabet(params KeyValuesAutomataAlphabetSymbol<TKey, TValue>[] symbols)
        {
            _symbols = new List<KeyValuesAutomataAlphabetSymbol<TKey, TValue>>(symbols);
        }
        
        public int IndexByValue(TValue value)
        {
            for(int i = 0; i < Count; i++)
                if(_symbols[i].HasValue(value)) return i;
            
            return -1;
        }
        
        public AutomataKeyValuesAlphabet<TKey, TValue> Add(KeyValuesAutomataAlphabetSymbol<TKey, TValue> symbol)
        {
            _symbols.Add(symbol);
            return this;
        }
        
        public AutomataKeyValuesAlphabet<TKey, TValue> Add(TKey key, TValue[] values)
        {
            return Add(new KeyValuesAutomataAlphabetSymbol<TKey, TValue>(key, values));
        }
    }
}