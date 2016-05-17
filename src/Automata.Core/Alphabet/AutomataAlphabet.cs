using System;
using System.Collections.Generic;
using Automata.Core.Interfaces;

namespace Automata.Core.Alphabet
{
    public class AutomataAlphabet : IAutomataAlphabet
    {
        private List<IComparable> _symbols;
        
        public int Length { get { return _symbols.Count; } } 
        
        public AutomataAlphabet()
        { 
            _symbols = new List<IComparable>();
        }
        
        public AutomataAlphabet(params string[] symbols)
            : this()
        {
            for(int i = 0; i < symbols.Length; i++)
                _symbols.Add(symbols[i]);
        }

        public int IndexOfValue(IComparable value)
        {
            return IndexOf(value);
        }

        public int IndexOf(IComparable key)
        {
            if(key != null)
            {
                for(int i = 0; i < Length; i++)
                    if(_symbols[i].Equals(key)) return i;
            }
            
            return -1;
        }
        
        public AutomataAlphabet Add(string symbol)
        {
            return Add(symbol);
        }
        
        private AutomataAlphabet Add(IComparable symbol)
        {
            if(symbol == null) 
                throw new System.ArgumentNullException("It's not possible add null symbol to alphabet");
            
            _symbols.Add(symbol);
            return this;
        }
    }
}