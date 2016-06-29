using Automata.Core.FiniteState;
using Automata.Core.Alphabet;
using Automata.Core.Facade;

namespace Automata.Console.Samples
{   
    public static class NonDeterministics
    {	
		public static NonDeterministicAutomata Sheeptalk1()
		{
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet("ba!");
            
            //Instanciação do automata com um alfabeto
            var sheeptalk = new NonDeterministicAutomata(sheepabcd);
            
            //Adicionando transições
            sheeptalk.AddTransition('b', 0, 1); 
            sheeptalk.AddTransition('a', 1, 2); 
            sheeptalk.AddTransition('a', 2, 3);
            sheeptalk.AddTransition('!', 3, 4);
            sheeptalk.AddEpsilon(3, 2);
            
            //Define o 4º estado como aceito
            sheeptalk.AcceptState(4);
			
			return sheeptalk;
		}
		
		public static NonDeterministicAutomata Sheeptalk2()
        {   
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet();
            
            //Factory para criação de automatas através de interface fluente e string que define um alfabeto de chars
            var sheepfactory = new AutomataFactory("ba!");
            
            //Definição de modelo de automata com estados e suas transições através de um Fluent Facade 
            var sheepmodel = sheepfactory.NonDeterministic()
                                .When('b').ToNext()
                                .OnNext()
                                .When('a').ToNext()
                                .OnNext()
                                .When('a').ToNext()
                                .OnNext()
                                .When('!').ToNext()
                                .Epsilon().ToPrev()
                                .OnNext().Accept();
                                
            //Getting a automata from model
            return sheepmodel.CreateAutomata() as NonDeterministicAutomata;
        }
        
		public static NonDeterministicAutomata StuttererSeeptalk()
        {   
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet();
            
            //Factory para criação de automatas através de interface fluente e string que define um alfabeto de chars
            var sheepfactory = new AutomataFactory("ba!");
            
            //Definição de modelo de automata com estados e suas transições através de um Fluent Facade 
            var stuttererSheepModel = sheepfactory.NonDeterministic()
                                .When('b').ToNext()
                                .OnNext()
                                .When('a').ToNext()
                                .OnNext()
                                .When('a').ToNext()
                                .OnNext()
                                .Epsilon().To(0)
                                .When('a').ToNext()
                                .When('!').To(5)
                                .OnNext()
                                .Epsilon().To(0)
                                .When('a').Repeat()
                                .OnNext().Accept();
                                
            //Getting a automata from model
            return stuttererSheepModel.CreateAutomata() as NonDeterministicAutomata;
        }

        public static NonDeterministicAutomata Money()
        {
            //Criação de alfabeto de caracteres agrupados
            var alphabet = new AutomataGroupAlphabet()
                                .Add("single", new [] { "one" })
                                .Add("plural1", new [] { "two", "three", "four", "five", "six", "seven", "eight", "nine" })
                                .Add("plural2", new[] { "ten", "eleven", "twelve", "thirteen", "fourteen", "sixteen", "seventeen", "eighteen", "nineteen" })
                                .Add("tens", new [] {"twenty", "thirty", "fourty", "fifity", "sixty", "seventy", "eighty", "ninety" })
                                .Add("cent")
                                .Add("cents")
                                .Add("dollar")
                                .Add("dollars");

            //Factory para criação de automatas através de interface fluente
            var factory = new AutomataFactory(alphabet);

            //Definição de modelo de automata com estados e suas transições através de um Fluent Facade
            var model = factory.NonDeterministic()
                            .When("tens").To("q1").To("q2")
                            .When("single").To("q2a")
                            .When("plural2").To("q2")
                            .When("plural1").To("q2")
                        .On("q1")
                            .When("single").To("q2")
                            .When("plural1").To("q2")
                        .On("q2")
                            .When("dollars").To("q3").To("q6")
                            .Epsilon().To("q5")
                        .On("q2a")
                            .When("dollar").To("q3").To("q6")
                            .Epsilon().To("q5a")
                        .On("q3")
                            .Epsilon().To("q4")
                            .When("tens").To("q4").To("q5")
                            .When("single").To("q5a")
                            .When("plural2").To("q5")
                        .On("q4")
                            .When("single").To("q5")
                            .When("plural1").To("q5")
                        .On("q5")
                            .When("cents").To("q6")
                        .On("q5a")
                            .When("cent").To("q6")
                        .On("q6").Accept();

            //Getting a automata from model
            return model.CreateAutomata() as NonDeterministicAutomata;
        }
    }
}