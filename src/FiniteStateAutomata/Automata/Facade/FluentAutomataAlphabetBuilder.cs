using System;
using System.Collections.Generic;
using FiniteStateAutomata.Automata.Interfaces;
using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Automata.Facade
{
    public class FluentAutomataAlphabetBuilder<T>
    {
        protected List<FluentAutomataAlphabetSymbol<T>> _alphabet;
        protected Func<AutomataAlphabet<T, T>, IAutomata<T, T>> _instantiate;
        
        public FluentAutomataAlphabetBuilder(T[] symbols, Func<AutomataAlphabet<T, T>, IAutomata<T, T>> func)
        {
            _instantiate = func;
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
        
        public FluentAutomata<T> OnFirstState()
        {
            var alphabet = MakeAlphabet();
            var automata = _instantiate(alphabet);
            return new FluentAutomata<T>(automata);
        }
        
        protected AutomataAlphabet<T, T> MakeAlphabet()
        {
            AutomataAlphabetSymbolBase<T, T>[] symbols = new KeyValuesAutomataAlphabetSymbol<T>[_alphabet.Count];
            int i = 0;
            foreach(var s in _alphabet) symbols[i++] = s.GetSymbol();
            return new AutomataAlphabet<T, T>(symbols);
        }
    }
}