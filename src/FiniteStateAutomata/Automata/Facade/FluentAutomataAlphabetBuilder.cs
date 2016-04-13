using System.Collections.Generic;
using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Automata.Facade
{
    public class FluentAutomataAlphabetBuilder<T>
    {
        /*private List<FluentAutomataAlphabetSymbol<T>> _alphabet;
        
        public FluentAutomataAlphabetBuilder(params T[] symbols)
        {
            _alphabet = new List<FluentAutomataAlphabetSymbol<T>>();
            for(int i = 0; i < symbols.Length; i++) 
                _alphabet.Add(new FluentAutomataAlphabetSymbol<T>(symbols[i], this));
        }
        
        public FluentAutomataAlphabetSymbol<T> Set(T key)
        {
            FluentAutomataAlphabetSymbol<T> symbol = null;
            foreach (var s in _alphabet)
            {
                if(s.Key.Equals(key))
                {
                    symbol = s;
                    break;
                }
            }
            
            if(symbol == null)
                symbol = new FluentAutomataAlphabetSymbol<T>(key, this);
            
            _alphabet.Add(symbol);
            return symbol;
        }
        
        public FluentAutomataBase<T> OnFirstState()
        {
            var alphabet = MakeAlphabet();
            return new FluentAutomataBase<T>(alphabet);
        }
        
        private AutomataAlphabet<T> MakeAlphabet()
        {
            var symbols = new AutomataAlphabetSymbol<T>[_alphabet.Count];
            int i = 0;
            foreach(var s in _alphabet) symbols[i++] = s.GetSymbol();
            return new AutomataAlphabet<T>(symbols);
        }*/
    }
}