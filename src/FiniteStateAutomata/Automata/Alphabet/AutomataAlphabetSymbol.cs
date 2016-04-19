using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;

namespace FiniteStateAutomata.Automata.Alphabet
{
    public class AutomataAlphabetSymbol<T> : IAutomataAlphabetSymbol<T>
    {   
        public T Key { get; private set; }
        
        public AutomataAlphabetSymbol(T key)
        { 
            Key = key;
        }
        
        public bool HasValue(T value)
        {
            return Key.Equals(value);
        }
    }    
}