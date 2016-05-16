using System.Collections.Generic;
using Automata.Core.FiniteState;
using Automata.Core.Alphabet;
using Automata.Core.Facade;

namespace Automata.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
			Separar exemplos de teste, onte exemplo tem um accept e um reject e teste tem muuuitos accept e muiiitos reject
			Exemplo de alphabet com FSA de conjugação do verbo
			Um Exemplo de cada operação
			Todos os exemplos em NonDeterminitic
			Exemplos de regex comum como validação de email e cpf
			Exemplos de Cast entre os 3 automatas
			*/
			            
			/** Exemplo 1 **/
			//Método de exemplo para criação de automata
            var sheeptalk1 = AutomataExamples.GetAutomata1();
			
            //Testando o automata
            bool accept1 = sheeptalk1.IsMatch("baaa!".ToCharArray()); //Match consecutive string
            bool reject1 = sheeptalk1.IsMatch("bbaa!".ToCharArray());
            
            //Exibir resultado 
            System.Console.WriteLine($"Exemplo 1");
            System.Console.WriteLine($"'baaa!' is {accept1}");
            System.Console.WriteLine($"'bbaa!' is {reject1}");            
            
            /** Exemplo 2 **/            
			//Método de exemplo para criação de automata
            var sheeptalk2 = AutomataExamples.GetAutomata2();
            
            //Testando o automata
            var accept2 = sheeptalk2.IsMatch("baaa!".ToCharArray());;
            var reject2 = sheeptalk2.IsMatch("baaa!".ToCharArray());;
            
            //Exibir resultado 
            System.Console.WriteLine($"Exemplo 2");
            System.Console.WriteLine($"'baaa!' is {accept2}");
            System.Console.WriteLine($"'bbaa!' is {reject2}");
            
            /** Exemplo 3 **/            
			//Método de exemplo para criação de automata
            var sheeptalk3 = AutomataExamples.GetAutomata3();
            
            //Testando o automata
            var accept3 = sheeptalk3.IsMatch("baaa!".ToCharArray());;
            var reject3 = sheeptalk3.IsMatch("baaa!".ToCharArray());;
            
            //Exibir resultado 
            System.Console.WriteLine($"Exemplo 3");
            System.Console.WriteLine($"'baaa!' is {accept3}");
            System.Console.WriteLine($"'bbaa!' is {reject3}");
            
            /*
            /** Exemplo 3 **
            //Alfabeto de caracteres 
            var sheepabcd3 = new AutomataCharAlphabet("ba!");
            
            
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
