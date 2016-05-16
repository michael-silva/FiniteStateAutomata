using System;
using System.Collections.Generic;
using Automata.Core.Interfaces;

namespace Automata.Core.Alphabet
{
    public class AlphabetGroupSymbol 
    {
        private IComparable[] _values;
        
        public AlphabetGroupSymbol(params string[] values)
        {
            _values = values;
        }
        
        /*public AlphabetGroupSymbol(params IAutomata[] values)
        {
            _values = values;
        }*/
        
        public bool HasValue(IComparable value)
        {
            for(int i = 0; i < _values.Length; i++)
                if(_values[i].Equals(value)) return true;
            return false;
        }
    }
}