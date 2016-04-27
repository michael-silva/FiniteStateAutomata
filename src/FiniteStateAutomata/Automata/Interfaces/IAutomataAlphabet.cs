namespace FiniteStateAutomata.Automata.Interfaces
{
    public interface IAutomataAlphabet<T>
    {
        int Count { get; }
        
        int IndexByValue(T value);
    }
}
