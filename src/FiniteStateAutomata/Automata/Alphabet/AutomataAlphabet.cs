using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class AutomataAlphabet<T> : IAutomataAlphabet<T>
    {
        private List<T> _symbols;
        
        public int Count { get { return _symbols.Count; } } 
        
        public AutomataAlphabet()
        { 
            _symbols = new List<T>();
        }
        
        public AutomataAlphabet(params T[] symbols)
        {
            _symbols = new List<T>(symbols);
        }
        
        public int IndexByValue(T value)
        {
            if(value != null)
            {
                for(int i = 0; i < Count; i++)
                    if(_symbols[i].Equals(value)) return i;
            }
            
            return -1;
        }
        
        public AutomataAlphabet<T> Add(T symbol)
        {
            if(symbol == null) 
                throw new System.ArgumentNullException("It's not possible add null symbol to alphabet");
            
            _symbols.Add(symbol);
            return this;
        }
    }
}