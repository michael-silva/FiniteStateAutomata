using FiniteStateAutomata.Automata.FiniteState;
using FiniteStateAutomata.Automata.Alphabet;

namespace FiniteStateAutomata.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*var factory = new AutomataFactory();
            var dautomata = factory.CreateDeterministic("ba!")
                                .Set('!').Values('!', 'h')
                                .Set('a').Values('a', 'e')
                             .OnFirstState()
                                .When('b').MoveToNext()
                             .OnNext()
                                .When('a').Repeat().MoveToNext()
                             .OnNext()
                                .When('!').MoveToNext()
                             .OnNext().Accept();
                             
            bool match = dautomata.IsMatch();
            System.Console.WriteLine($"{name}");*/
            
            var s1 = new AutomataAlphabetSymbolBase<char, char>[] 
                        { 
                            new AutomataAlphabetSymbol<char>('b'),
                            new AutomataAlphabetSymbol<char>('a'),
                            new AutomataAlphabetSymbol<char>('!'),
                        };
            
            var s2 = new AutomataAlphabetSymbolBase<char, char>[] 
                        { 
                            new KeyValuesAutomataAlphabetSymbol<char>('b'),
                            new KeyValuesAutomataAlphabetSymbol<char>('a', 'e'),
                            new KeyValuesAutomataAlphabetSymbol<char>('!', 'h'),
                        };
            
            var a1 = new AutomataAlphabet<char, char>(s1);
            var a2 = new AutomataAlphabet<char, char>(s2);
            
            var d1 = new DeterministicAutomata<char, char>(a1);
            var d2 = new DeterministicAutomata<char, char>(a2);
            
            d1.AddTransition(0, 0, 1)
                .AddState()
                .AddTransition(1, 1, 2)
                .AddState()
                .AddTransition(2, 1, 2)
                .AddTransition(2, 2, 3)
                .AddState()
                .AcceptState(3);
                
            d2.AddTransition(0, 0, 1)
                .AddState()
                .AddTransition(1, 1, 2)
                .AddState()
                .AddTransition(2, 1, 2)
                .AddTransition(2, 2, 3)
                .AddState()
                .AcceptState(3);
            
            bool m1 = d1.IsMatch('b', 'a', '!');
            bool m2 = d2.IsMatch('b', 'e', 'e', 'h');
            bool m3 = d1.IsMatch('b', 'e', '!');
            bool m4 = d2.IsMatch('b', 'e', '!', 'h');
            
            System.Console.WriteLine($"{m1} - {m2} - {m3} - {m4}");
            
            System.Console.WriteLine("Hello World");
            System.Console.Read();
        }
    }
}
