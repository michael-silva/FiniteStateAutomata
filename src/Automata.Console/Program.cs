using System;

namespace Automata.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                /*
                Exemplo de alphabet com FSA de conjugação do verbo
                Um Exemplo de cada operação
                Todos os exemplos em NonDeterminitic
                Exemplos de regex comum como validação de email e cpf
                Exemplos de Cast entre os 3 automatas
                */

                /** Exemplo 1 **/
                //Método de exemplo para criação de automata
                var sheeptalk1 = Samples.Sheeptalk1();

                //Testando o automata
                bool accept1 = sheeptalk1.IsMatch("baaa!"); //Match consecutive string
                bool reject1 = sheeptalk1.IsMatch("bbaa!");

                //Exibir resultado 
                System.Console.WriteLine($"Exemplo 1");
                System.Console.WriteLine($"'baaa!' is {accept1}");
                System.Console.WriteLine($"'bbaa!' is {reject1}");

                /** Exemplo 2 **/
                //Método de exemplo para criação de automata
                var sheeptalk2 = Samples.Sheeptalk2();

                //Testando o automata
                var accept2 = sheeptalk2.IsMatch("baaa!"); ;
                var reject2 = sheeptalk2.IsMatch("bbaa!"); ;

                //Exibir resultado 
                System.Console.WriteLine($"Exemplo 2");
                System.Console.WriteLine($"'baaa!' is {accept2}");
                System.Console.WriteLine($"'bbaa!' is {reject2}");

                /** Exemplo 3 **/
                //Método de exemplo para criação de automata
                var sheeptalk3 = Samples.Sheeptalk3();

                //Testando o automata
                var accept3 = sheeptalk3.IsMatch("baaa!"); ;
                var reject3 = sheeptalk3.IsMatch("bbaa!"); ;

                //Exibir resultado 
                System.Console.WriteLine($"Exemplo 3");
                System.Console.WriteLine($"'baaa!' is {accept3}");
                System.Console.WriteLine($"'bbaa!' is {reject3}");

                /** Exemplo 4 **/
                //Método de exemplo para criação de automata
                var moneycheck = Samples.Money();

                //Testando o automata
                var a1 = moneycheck.SplitMatch("sixty two dollars", ' ');
                var a2 = moneycheck.SplitMatch("one cent", ' ');
                var r1 = moneycheck.SplitMatch("sixty ten dollars", ' ');
                var r2 = moneycheck.SplitMatch("one cents", ' ');

                //Exibir resultado 
                System.Console.WriteLine($"Exemplo 4");
                System.Console.WriteLine($"'sixty two dollars' is {a1}");
                System.Console.WriteLine($"'one cent' is {a2}");
                System.Console.WriteLine($"'sixty ten dollars' is {r1}");
                System.Console.WriteLine($"'one cents' is {r2}");

                /*var m1 = moneycheck.IsMatch("one dollar", ' ');
                var m2 = moneycheck.IsMatch("two dollars", ' ');
                var m3 = moneycheck.IsMatch("two cents", ' ');
                var m4 = moneycheck.IsMatch("sixty two dollars", ' ');
                var m5 = moneycheck.IsMatch("eleven cents", ' ');
                var m6 = moneycheck.IsMatch("five dollars", ' ');
                var m7 = moneycheck.IsMatch("thirteen dollars one cent", ' ');

                System.Console.WriteLine($"{m1}, {m2}, {m3}, {m4}, {m5}, {m6}, {m7}");*/

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

            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            System.Console.WriteLine("Finished!");
        }
    }
}
