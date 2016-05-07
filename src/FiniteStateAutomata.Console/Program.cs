using FiniteStateAutomata.Automata.FiniteState;
using FiniteStateAutomata.Automata.Alphabet;
using FiniteStateAutomata.Automata.Facade;

namespace FiniteStateAutomata.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var sheepabcd = new AutomataCharAlphabet("ba!");
            
            var sheepttable = new int [][] 
                                { 
                                    new [] { 1, 0, 0 },
                                    new [] { 0, 1, 0 },
                                    new [] { 0, 1, 1 },
                                    new [] { 0, 0, 0 } 
                                };
            var sheeptalk1 = new DeterministicAutomata(sheepabcd, sheepttable);
            sheeptalk1.AcceptState(3);
            
            
        IAutomata<T> AddState();
        
        IAutomata<T> AddTransition(T symbol, int fromState, int toState);
            
            var sheeptalk2 = new DeterministicAutomata(sheepabcd);
            sheeptalk2.AddState();
            sheeptalk2.AddState();
            sheeptalk2.AddState();
            sheeptalk2.AddState();
            sheeptalk2.AddTransition('b', 0, 1);
            sheeptalk2.AddTransition('a', 1, 2);
            sheeptalk2.AddTransition('a', 2, 2);
            sheeptalk2.AddTransition('!', 2, 3);
            sheeptalk2.AcceptState(3);
            
            var sheepfactory = new AutomataFactory(sheepabcd);
            var sheeptalk3 = sheepfactory.Deterministic()
                                .When('b').MoveNext()
                                .OnNext()
                                .When('a').MoveNext()
                                .OnNext()
                                .When('a').Repeat()
                                .When('!').MoveNext()
                                .OnNext().Accept();
                                
            var sheeptalk4 = sheepfactory.NonDeterministic()
                                .When('b').MoveNext()
                                .OnNext()
                                .When('a').MoveNext()
                                .OnNext()
                                .When('a').EpsilonToPrev()
                                .When('!').MoveNext()
                                .OnNext().Accept();
            
            var alphabet = new AutomataGroupAlphabet()
                                .Add("one")
                                .Add("two", "three", "four", "five", "six", "seven", "eight", "nine")
                                .Add("ten", "eleven", "twelve", "thirteen", "fourteen", "sixteen", "seventeen", "eighteen", "nineteen")
                                .Add("twenty", "thirty", "fourty", "fifity", "sixty", "seventy", "eighty", "ninety")
                                .Add("cent")
                                .Add("cents")
                                .Add("dollar")
                                .Add("dollars");
                                
            var factory = new AutomataFactory<string>(alphabet);
                
            var a1 = factory.Deterministic()
                            .When("one").To("q2a")
                            .When("two").To("q2")
                            .When("ten").To("q2")
                            .When("twenty").To("q1")
                        .On("q1").Accept()
                            .When("one").To("q2")
                            .When("two").To("q2")
                        .On("q2a")
                            .When("cent").To("q7")
                            .When("dollar").To("q4")
                        .On("q2")
                            .When("cents").To("q7")
                            .When("dollars").To("q4")
                        .On("q4").Accept()
                            .When("one").To("q6a")
                            .When("two").To("q6")
                            .When("ten").To("q6")
                            .When("twenty").To("q5")
                        .On("q5").Accept()
                            .When("one").To("q6")
                            .When("two").To("q6")
                        .On("q6a")
                            .When("cent").To("q7")
                        .On("q6")
                            .When("cents").To("q7")
                        .On("q7").Accept();
            
            var m1 = a1.IsMatch("one dollar".Split(' '));
            var m2 = a1.IsMatch("two dollars".Split(' '));
            var m3 = a1.IsMatch("two cents".Split(' '));
            var m4 = a1.IsMatch("sixty two dollars".Split(' '));
            var m5 = a1.IsMatch("eleven cents".Split(' '));
            var m6 = a1.IsMatch("five dollars".Split(' '));
            var m7 = a1.IsMatch("thirteen dollars one cent".Split(' '));
            
            System.Console.WriteLine($"{m1}, {m2}, {m3}, {m4}, {m5}, {m6}, {m7}");
            
            var a2 = factory.NonDeterministic()
                            .When("one").To("q2a")
                            .When("two").To("q2")
                            .When("ten").To("q2")
                            .When("twenty").To("q1")
                        .On("q1").Accept()
                            .When("one").To("q2")
                            .When("two").To("q2")
                        .On("q2a")
                            .When("cent").To("q7")
                            .When("dollar").To("q4")
                        .On("q2")
                            .When("cents").To("q7")
                            .When("dollars").To("q4")
                        .On("q4").Accept()
                            .When("one").To("q6a")
                            .When("two").To("q6")
                            .When("ten").To("q6")
                            .When("twenty").To("q5")
                        .On("q5").Accept()
                            .When("one").To("q6")
                            .When("two").To("q6")
                        .On("q6a")
                            .When("cent").To("q7")
                        .On("q6")
                            .When("cents").To("q7")
                        .On("q7").Accept();
            
            var n1 = a2.IsMatch("one dollar".Split(' '));
            var n2 = a2.IsMatch("two dollars".Split(' '));
            var n3 = a2.IsMatch("two cents".Split(' '));
            var n4 = a2.IsMatch("sixty two dollars".Split(' '));
            var n5 = a2.IsMatch("eleven cents".Split(' '));
            var n6 = a2.IsMatch("five dollars".Split(' '));
            var n7 = a2.IsMatch("thirteen dollars one cent".Split(' '));
            
            System.Console.WriteLine($"{n1}, {n2}, {n3}, {n4}, {n5}, {n6}, {n7}");
            
            System.Console.WriteLine("Hello World");
            System.Console.Read();
        }
    }
}
