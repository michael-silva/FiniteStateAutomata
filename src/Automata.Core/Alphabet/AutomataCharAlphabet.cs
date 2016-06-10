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

        public int IndexOfValue(object value)
        {
            return IndexOf(value);
        }

        public int IndexOf(object key)
        {
            if (key != null && key is char)
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
        
        public bool Equals(IAutomataAlphabet other)
        {
            if(!(other is AutomataCharAlphabet) || other.Length != Length) return false;
            
            var automata = other as AutomataCharAlphabet;
            for(int i = 0; i < Length; i++)
                if(!_symbols[i].Equals(automata._symbols[i]))
                    return false;
                
            return true;
        }

        public List<string> ValuesFrom(string value)
        {
            var list = new List<string>();
            
            for(int i = 0; i < Length; i++)
                if(value.StartsWith(_symbols[i].ToString())) list.Add(_symbols[i].ToString());
            
            return list;
        }
    }
}