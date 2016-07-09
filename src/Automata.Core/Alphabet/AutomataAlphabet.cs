using System.Collections.Generic;
using Automata.Core.Interfaces;

namespace Automata.Core.Alphabet
{
    public class AutomataAlphabet : IAutomataAlphabet
    {
        private List<object> _symbols;
        
        public int Length { get { return _symbols.Count; } }

        private AutomataAlphabet(List<object> symbols)
        { 
            _symbols = symbols;
        }

        public AutomataAlphabet()
        { 
            _symbols = new List<object>();
        }
        
        public AutomataAlphabet(params string[] symbols)
            : this()
        {
            for(int i = 0; i < symbols.Length; i++)
                _symbols.Add(symbols[i]);
        }

        public int IndexOfValue(object value)
        {
            return IndexOf(value);
        }

        public int IndexOf(object key)
        {
            if(key != null && key is string)
            {
                for(int i = 0; i < Length; i++)
                    if(_symbols[i].Equals(key)) return i;
            }
            
            return -1;
        }
        
        public AutomataAlphabet Add(string symbol)
        {
            if(symbol == null) 
                throw new System.ArgumentNullException("It's not possible add null symbol to alphabet");
            
            _symbols.Add(symbol);
            return this;
        }
        
        public bool Equals(IAutomataAlphabet other)
        {
            if(!(other is AutomataAlphabet) || other.Length != Length) return false;
            
            var automata = other as AutomataAlphabet;
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