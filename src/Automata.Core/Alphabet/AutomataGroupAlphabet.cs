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

        public int IndexOf(object key)
        {
            if (key != null && key is string)
            {
                for (int i = 0; i < Length; i++)
                    if (_symbols[i].Key.Equals(key)) return i;
            }

            return -1;
        }

        public int IndexOfValue(object value)
        {
            if (value != null && (value is char || value is string))
            {
                for (int i = 0; i < Length; i++)
                    if (_symbols[i].HasValue(value)) return i;
            }

            return -1;
        }
        
        public AutomataGroupAlphabet Add(AlphabetGroupSymbol symbol)
        {
            _symbols.Add(symbol);
            return this;
        }
        
        public AutomataGroupAlphabet Add(string key, string[] values)
        {
            return Add(new AlphabetGroupSymbol(key, values));
        }

        public AutomataGroupAlphabet Add(string key, char[] values)
        {
            return Add(new AlphabetGroupSymbol(key, values));
        }

        public AutomataGroupAlphabet Add(string key)
        {
            return Add(new AlphabetGroupSymbol(key, new[] { key }));
        }

        public bool Equals(IAutomataAlphabet other)
        {
            if(!(other is AutomataGroupAlphabet) || other.Length != Length) return false;
            
            var automata = other as AutomataGroupAlphabet;
            for(int i = 0; i < Length; i++)
                if(!_symbols[i].Equals(automata._symbols[i]))
                    return false;
                
            return true;
        }
    }
}