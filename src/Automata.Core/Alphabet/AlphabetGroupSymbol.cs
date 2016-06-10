using System;

namespace Automata.Core.Alphabet
{
    public class AlphabetGroupSymbol
    {
        private object[] _values;
        public string Key { get; private set; }

        private AlphabetGroupSymbol(string key, int length)
        {
            Key = key;
            _values = new IComparable[length];
        }

        public AlphabetGroupSymbol(string key, params object[] values)
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
        
        public bool HasValue(object value)
        {
            for(int i = 0; i < _values.Length; i++)
                if(_values[i].Equals(value)) return true;
            return false;
        }
        
        public string ValueInStartOf(string value)
        {
            for(int i = 0; i < _values.Length; i++)
                if(value.StartsWith(_values[i].ToString())) return _values[i].ToString();
            return string.Empty;
        }
    }
}