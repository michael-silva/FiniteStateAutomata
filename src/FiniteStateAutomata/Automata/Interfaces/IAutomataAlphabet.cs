namespace FiniteStateAutomata.Automata.Interfaces
{
    public interface IAutomataAlphabet<T>
    {
        int IndexByValue(T value);
    }
}
