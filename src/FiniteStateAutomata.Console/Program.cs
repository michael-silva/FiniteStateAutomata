using FiniteStateAutomata.Automata.FiniteState;
using FiniteStateAutomata.Automata.Alphabet;
using FiniteStateAutomata.Automata;

namespace FiniteStateAutomata.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var alphabet = new AutomataAlphabet()
                                .Add('b');
                                //.AddGroup('!', 'h')
                                //.AddGroup('a', 'e');
                                
            /*var dautomata = new DAutomata(alphabet)
                             .OnFirstState()
                                .When('b').MoveToNext()
                             .OnNext()
                                .When('a').Repeat().MoveToNext()
                             .OnNext()
                                .When('!').MoveToNext()
                             .OnNext().Accept();
                             
            bool m1 = dautomata.IsMatch("baaaah");
            bool m2 = dautomata.IsMatch("beeeaa!");
            System.Console.WriteLine($"{m1} - {m2}");*/
            
            /*var s1 = new AutomataAlphabetSymbolBase<char, char>[] 
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
            
            var n1 = new NonDeterministicAutomata<char, char>(a1);
            var n2 = new NonDeterministicAutomata<char, char>(a2);
            
            n1.AddTransition(0, 0, 1)
                .AddState()
                .AddTransition(1, 1, 1)
                .AddTransition(1, 1, 2)
                .AddState()
                .AcceptState(2);
                
            n2.AddTransition(0, 0, 1)
                .AddState()
                .AddTransition(1, 1, 1)
                .AddTransition(1, 1, 2)
                .AddState()
                .AcceptState(2);
            
            bool m5 = n1.IsMatch('b', 'a', '!');
            bool m6 = n2.IsMatch('b', 'e', 'e', 'h');
            bool m7 = n1.IsMatch('b', 'e', '!');
            bool m8 = n2.IsMatch('b', 'e', '!', 'h');
            
            System.Console.WriteLine($"{m5} - {m6} - {m7} - {m8}");
            */
            System.Console.WriteLine("Hello World");
            System.Console.Read();
        }
    }
}
