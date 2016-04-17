using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Automata.Facade
{
    public class FluentAutomataAlphabetSymbol<T>
    {
        private FluentAutomataAlphabetBuilder<T> _alphabetBuilder;
        private T[] _values;
        
        public T Key { get; private set; }
        
        public FluentAutomataAlphabetSymbol(T key, FluentAutomataAlphabetBuilder<T> alphabetBuilder)
        {
            Key = key;
            _alphabetBuilder = alphabetBuilder;
        }
        
        public FluentAutomataAlphabetBuilder<T> Values(params T[] values)
        {
            _values = values;
            return _alphabetBuilder;
        }
        
        internal AutomataAlphabetSymbolBase<T, T> GetSymbol()
        {
            return new KeyValuesAutomataAlphabetSymbol<T>(Key, _values);
        }
    }
}