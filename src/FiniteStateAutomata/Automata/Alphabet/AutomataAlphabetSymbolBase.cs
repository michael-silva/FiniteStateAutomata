namespace FiniteStateAutomata.Automata.Alphabet
{
    public abstract class AutomataAlphabetSymbolBase<TKey, TValue> 
    {
        public TKey Key { get; private set; }
        
        protected AutomataAlphabetSymbolBase(TKey key)
        {
            Key = key;
        }
        
        public abstract bool HasValue(TValue value);
    }
}