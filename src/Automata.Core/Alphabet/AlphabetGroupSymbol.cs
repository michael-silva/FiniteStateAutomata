using System;

namespace Automata.Core.Alphabet
{
    public class AlphabetGroupSymbol
    {
        private IComparable[] _values;
        public IComparable Key { get; private set; }

        private AlphabetGroupSymbol(string key, int length)
        {
            Key = key;
            _values = new IComparable[length];
        }

        public AlphabetGroupSymbol(string key, params IComparable[] values)
        {
            Key = key;
            _values = values;
        }

        public AlphabetGroupSymbol(string key, params char[] values)
            : this(key, values.Length)
        {
            for (int i = 0; i < _values.Length; i++)
                _values[i] = values[i];
        }

        public AlphabetGroupSymbol(string key, params string[] values)
            : this(key, values.Length)
        {
            for (int i = 0; i < _values.Length; i++)
                _values[i] = values[i];
        }

        /*public AlphabetGroupSymbol(string key, params IAutomata[] values)
        {
            Key = key;
            for (int i = 0; i < _values.Length; i++)
                _values[i] = values[i];
        }*/
                
        public bool HasValue(IComparable value)
        {
            for(int i = 0; i < _values.Length; i++)
                if(_values[i].Equals(value)) return true;
            return false;
        }
    }
}