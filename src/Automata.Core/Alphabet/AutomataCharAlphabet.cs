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
        
        public int IndexOf(string value)
        {
            if(value.Length > 1)
                throw new Exception("The char alphabet don't can get index for string value");
            
            return IndexOf(value[0]);
        }
        
        public int IndexOf(char value)
        {
            for(int i = 0; i < Length; i++)
                if(_symbols[i].Equals(value)) return i;
            
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