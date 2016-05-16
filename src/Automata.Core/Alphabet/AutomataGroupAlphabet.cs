using System;
using System.Collections.Generic;
using Automata.Core.Interfaces;

namespace Automata.Core.Alphabet
{
    public class AutomataGroupAlphabet : IAutomataAlphabet
    {
        private List<AlphabetGroupSymbol> _symbols;
        
        public int Length { get { return _symbols.Count; } } 
        
        public AutomataGroupAlphabet()
        { 
            _symbols = new List<AlphabetGroupSymbol>();
        }
        
        public AutomataGroupAlphabet(params AlphabetGroupSymbol[] symbols)
        {
            _symbols = new List<AlphabetGroupSymbol>(symbols);
        }
        
        public int IndexOf(string value)
        {
            for(int i = 0; i < Length; i++)
                if(_symbols[i].HasValue(value)) return i;
            
            return -1;
        }
        
        public AutomataGroupAlphabet Add(AlphabetGroupSymbol symbol)
        {
            _symbols.Add(symbol);
            return this;
        }
        
        public AutomataGroupAlphabet Add(params string[] values)
        {
            return Add(new AlphabetGroupSymbol(values));
        }
        
        /*
        public AutomataGroupAlphabet Add(params Automata[] values)
        {
            return Add(new GroupAutomataAlphabetSymbol<T>(values));
        }*/
    }
}