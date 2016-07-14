using System;

namespace Automata.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                /** Exemplo 1 **/
                //Método de exemplo para criação de automata
                var sheeptalk1 = Samples.Deterministics.Sheeptalk1();

                //Testando o automata
                bool accept1 = sheeptalk1.IsMatch("baaa!"); //Match consecutive string
                bool reject1 = sheeptalk1.IsMatch("bbaa!");

                //Exibir resultado 
                System.Console.WriteLine($"Exemplo 1");
                System.Console.WriteLine($"'baaa!' is {accept1}");
                System.Console.WriteLine($"'bbaa!' is {reject1}");

                /** Exemplo 2 **/
                //Método de exemplo para criação de automata
                var sheeptalk2 = Samples.Deterministics.Sheeptalk2();

                //Testando o automata
                var accept2 = sheeptalk2.IsMatch("baaa!"); ;
                var reject2 = sheeptalk2.IsMatch("bbaa!"); ;

                //Exibir resultado 
                System.Console.WriteLine($"Exemplo 2");
                System.Console.WriteLine($"'baaa!' is {accept2}");
                System.Console.WriteLine($"'bbaa!' is {reject2}");

                /** Exemplo 3 **/
                //Método de exemplo para criação de automata
                var sheeptalk3 = Samples.Deterministics.Sheeptalk3();

                //Testando o automata
                var accept3 = sheeptalk3.IsMatch("baaa!"); ;
                var reject3 = sheeptalk3.IsMatch("bbaa!"); ;

                //Exibir resultado 
                System.Console.WriteLine($"Exemplo 3");
                System.Console.WriteLine($"'baaa!' is {accept3}");
                System.Console.WriteLine($"'bbaa!' is {reject3}");

                /** Exemplo 4 **/
                //Método de exemplo para criação de automata
                var moneycheck = Samples.Deterministics.Money();

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
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            System.Console.WriteLine("Finished!");
        }
    }
}
