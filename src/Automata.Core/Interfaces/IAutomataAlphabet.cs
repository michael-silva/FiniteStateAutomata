namespace Automata.Core.Interfaces
{
    public interface IAutomataAlphabet
    {
        int Length { get; }
        
        int IndexOf(string value);
    }
}
