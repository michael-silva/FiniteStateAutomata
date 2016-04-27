using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class GroupAutomataAlphabetSymbol<T> : IAutomataAlphabetSymbol<T> 
    {
        private T[] _values;
        
        public GroupAutomataAlphabetSymbol(params T[] values)
        {
            _values = values;
        }
        
        public bool HasValue(T value)
        {
            for(int i = 0; i < _values.Length; i++)
                if(_values[i].Equals(value)) return true;
            return false;
        }
    }
}