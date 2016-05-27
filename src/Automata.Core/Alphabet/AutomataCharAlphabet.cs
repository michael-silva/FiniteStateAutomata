using System.Collections.Generic;
using Automata.Core.Interfaces;
using System;

namespace Automata.Core.Alphabet
{
    public class AutomataCharAlphabet : IAutomataAlphabet
    {
        private List<char> _symbols;
        
        public int Length { get { return _symbols.Count; } } 
        
        public AutomataCharAlphabet()
        {
            _symbols = new List<char>();
        }
        
        public AutomataCharAlphabet(params char[] symbols)
        {
            _symbols = new List<char>(symbols);
        }
        
        public AutomataCharAlphabet(string symbols)
            : this(symbols.ToCharArray())
        { }

        public int IndexOfValue(IComparable value)
        {
            return IndexOf(value);
        }

        public int IndexOf(IComparable key)
        {
            if (key != null)
            {
                char keyChar = Convert.ToChar(key);
                for (int i = 0; i < Length; i++)
                    if(_symbols[i].Equals(keyChar)) return i;
            }

            return -1;
        }
        
        public AutomataCharAlphabet Add(char symbol)
        {
            _symbols.Add(symbol);
            return this;
        }
        
        public AutomataCharAlphabet Add(string symbols)
        {
            for(int i = 0; i < symbols.Length; i++)
                _symbols.Add(symbols[i]);
            return this;
        }
    }
}