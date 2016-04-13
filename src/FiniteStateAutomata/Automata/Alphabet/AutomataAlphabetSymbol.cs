using System.Collections.Generic;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class AutomataAlphabetSymbol<T> : AutomataAlphabetSymbolBase<T, T>
    {   
        public AutomataAlphabetSymbol(T key)
            : base(key)
        { }
        
        public override bool HasValue(T value)
        {
            return Key.Equals(value);
        }
    }    
}