namespace Automata.Core.Interfaces
{
    public interface IAutomataTableBuilder
    {
        void AddTransition(int symbolIndex, int fromState, int toState);
        void AcceptState(int index);
        void AcceptLast();
    }
}
