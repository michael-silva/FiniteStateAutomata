namespace Automata.Core.Interfaces
{
    public interface IAutomataOptimizer<TTable>
    {
        TTable Optimize(IAutomata automata, TTable transitions);
    }
}
