using System.Collections.Generic;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class KeyValuesAutomataAlphabetSymbol<TKey, TValue> : AutomataAlphabetSymbolBase<TKey, TValue> 
    {
        protected List<TValue> _values;
        
        public KeyValuesAutomataAlphabetSymbol(TKey key, params TValue[] values)
            : base(key)
        {
            _values = new List<TValue>();
            
            if(values != null)
            {
                for(int i = 0; i < values.Length; i++)
                    _values.Add(values[i]);
            }
        }
        
        public override bool HasValue(TValue value)
        {
            for(int i = 0; i < _values.Count; i++)
                if(_values[i].Equals(value)) return true;
            return false;
        }
    }
    
    public class KeyValuesAutomataAlphabetSymbol<T> : KeyValuesAutomataAlphabetSymbol<T, T>
    {   
        public KeyValuesAutomataAlphabetSymbol(T key, params T[] values)
            : base(key, values)
        { 
            _values.Add(key);
        }
        
        public KeyValuesAutomataAlphabetSymbol(T key)
            : base(key, key)
        { }
    }
       
}