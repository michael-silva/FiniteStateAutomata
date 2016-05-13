using System.Collections.Generic;
using FiniteStateAutomata.Automata.FiniteState;
using FiniteStateAutomata.Automata.Alphabet;
using FiniteStateAutomata.Automata.Facade;

namespace FiniteStateAutomata.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /** Exemplo 1 **/
            //Alfabeto de caracteres 
            var sheepabcd1 = new AutomataCharAlphabet("ba!");
            
            //Transition table é uma matriz de inteiros
            var sheepttable = new List<int?[]>() 
                                { 
                                    new int?[] { 1, null, null },
                                    new int?[] { null, 2, null },
                                    new int?[] { null, 2, 3 },
                                    new int?[] { null, null, null } 
                                };
            //Instanciação do automata com um alfabeto e uma trasition table
            var sheeptalk1 = new DeterministicAutomata<char>(sheepabcd1, sheepttable);
            
            //Define o 4º estado como aceito
            sheeptalk1.AcceptState(3);
            
            //Testando o automata
            var accept1 = sheeptalk1.IsMatch("baaa!".ToCharArray());
            var reject1 = sheeptalk1.IsMatch("bbaa!".ToCharArray());
            
            //Exibir resultado 
            System.Console.WriteLine($"Exemplo 1");
            System.Console.WriteLine($"'baaa!' is {accept1}");
            System.Console.WriteLine($"'bbaa!' is {reject1}");
            
            
            /** Exemplo 2 **/
            //Alfabeto de caracteres 
            var sheepabcd2 = new AutomataCharAlphabet("ba!");
            
            //Instanciação do automata sem transition table
            var sheeptalk2 = new DeterministicAutomata<char>(sheepabcd2);
            
            //Adicionando estados
            sheeptalk2.AddState();
            sheeptalk2.AddState();
            sheeptalk2.AddState();
            sheeptalk2.AddState();
            
            //Adicionando transições
            sheeptalk2.AddTransition('b', 0, 1);
            sheeptalk2.AddTransition('a', 1, 2);
            sheeptalk2.AddTransition('a', 2, 2);
            sheeptalk2.AddTransition('!', 2, 3);
            
            //Define o 4º estado como aceito
            sheeptalk2.AcceptState(3);
            
            //Testando o automata
            var accept2 = sheeptalk2.IsMatch("baaa!".ToCharArray());
            var reject2 = sheeptalk2.IsMatch("bbaa!".ToCharArray());
            
            //Exibir resultado 
            System.Console.WriteLine($"Exemplo 2");
            System.Console.WriteLine($"'baaa!' is {accept2}");
            System.Console.WriteLine($"'bbaa!' is {reject2}");
            
            /** Exemplo 3 **/
            //Alfabeto de caracteres 
            var sheepabcd3 = new AutomataCharAlphabet("ba!");
            
            //Factory para criação de automatas através de interface fluente 
            var sheepfactory = new AutomataFactory<char>(sheepabcd3);
            
            //Definição dos estados e suas transições através de um Fluent Facade 
            var sheeptalk3 = sheepfactory.Deterministic()
                                .When('b').ToNext()
                                .OnNext()
                                .When('a').ToNext()
                                .OnNext()
                                .When('a').Repeat()
                                .When('!').ToNext()
                                .OnNext().Accept();
            
            //Testando o automata
            var accept3 = sheeptalk3.IsMatch("baaa!".ToCharArray());
            var reject3 = sheeptalk3.IsMatch("bbaa!".ToCharArray());
            
            //Exibir resultado 
            System.Console.WriteLine($"Exemplo 3");
            System.Console.WriteLine($"'baaa!' is {accept3}");
            System.Console.WriteLine($"'bbaa!' is {reject3}");
                               
            var alphabet = new AutomataGroupAlphabet<string>()
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
            
            /*var a2 = factory.NonDeterministic()
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
            
            System.Console.WriteLine($"{n1}, {n2}, {n3}, {n4}, {n5}, {n6}, {n7}");*/
            
            System.Console.WriteLine("Hello World");
            System.Console.Read();
        }
    }
}
