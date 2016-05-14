using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class KeyValuesAutomataAlphabetSymbol<TKey, TValue> : IAutomataAlphabetSymbol<TValue> 
    {
        private List<TValue> _values;
        
        public TKey Key { get; private set; }
        
        public KeyValuesAutomataAlphabetSymbol(TKey key, params TValue[] values)
        {
            Key = key;
            _values = new List<TValue>(values);
        }
        
        public bool HasValue(TValue value)
        {
            for(int i = 0; i < _values.Count; i++)
                if(_values[i].Equals(value)) return true;
            return false;
        }
    }
}