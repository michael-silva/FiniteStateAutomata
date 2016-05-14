using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class AutomataCharAlphabet : IAutomataAlphabet<char>
    {
        private List<char> _symbols;
        
        public int Count { get { return _symbols.Count; } } 
        
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
        
        public int IndexByValue(char value)
        {
            for(int i = 0; i < Count; i++)
                if(_symbols[i] == value) return i;
            
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