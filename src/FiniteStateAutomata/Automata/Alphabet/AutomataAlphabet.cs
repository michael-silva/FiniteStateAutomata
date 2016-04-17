using System.Collections.Generic;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class AutomataAlphabet<TKey, TValue>
    {
        private AutomataAlphabetSymbolBase<TKey, TValue>[] _symbols;
        
        public int Length { get { return _symbols.Length; } } 
        
        public AutomataAlphabet(params AutomataAlphabetSymbolBase<TKey, TValue>[] symbols)
        {
            _symbols = symbols;
        }
        
        public int IndexByValue(TValue value)
        {
            for(int i = 0; i < Length; i++)
                if(_symbols[i].HasValue(value)) return i;
            
            return -1;
        }
    }    
}