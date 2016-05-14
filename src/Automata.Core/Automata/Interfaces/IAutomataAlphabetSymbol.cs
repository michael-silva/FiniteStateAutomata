namespace FiniteStateAutomata.Automata.Interfaces
{
    public interface IAutomataAlphabetSymbol<T>
    {
        bool HasValue(T value);
    }
}
