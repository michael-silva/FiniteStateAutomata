using System;
using System.Collections.Generic;
using Automata.Core.FiniteState;
using Automata.Core.Alphabet;
using Automata.Core.Facade;
using Automata.Core.Interfaces;

namespace Automata.Console
{
    public static class AutomataExamples
    {
        public static DeterministicAutomata GetAutomata1()
		{
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet("ba!");
            
            //Instanciação do automata com um alfabeto
            var sheeptalk = new DeterministicAutomata(sheepabcd);
            
            //Definição da tabela de transições
            sheeptalk.State(1, null, null)
                    .State(null, 2, null)
                    .State(null, 2, 3)
                    .State(null, null, null)
                    .AcceptLast();
			
			return sheeptalk;
		}
		
		public static DeterministicAutomata GetAutomata2()
		{
            //Instanciação do automata com string que define um alphabeto de chars
            var sheeptalk = new DeterministicAutomata("ba!");
            
            //Adicionando transições
            sheeptalk.AddTransition('b', 0, 1); 
            sheeptalk.AddTransition('a', 1, 2);
            sheeptalk.AddTransition('a', 2, 2);
            sheeptalk.AddTransition('!', 2, 3);
            
            //Define o 4º estado como aceito
            sheeptalk.AcceptState(3);
			
			return sheeptalk;
		}
		
		public static DeterministicAutomata GetAutomata3()
        {   
            //Criação de alfabeto de caracteres 
            var sheepabcd = new AutomataCharAlphabet("ba!");
            
            //Factory para criação de automatas através de interface fluente 
            var sheepfactory = new AutomataFactory(sheepabcd);
            
            //Definição de modelo de automata com estados e suas transições através de um Fluent Facade 
            var sheepmodel = sheepfactory.Deterministic()
                                .When("b").ToNext()
                                .OnNext()
                                .When("a").ToNext()
                                .OnNext()
                                .When("a").Repeat()
                                .When("!").ToNext()
                                .OnNext().Accept();
                                
            //Getting a automata from model
            return sheepmodel.CreateAutomata();
        }

        public static DeterministicAutomata GetAutomata4()
        {
            var alphabet = new AutomataGroupAlphabet()
                                .Add("one")
                                .Add("two", "three", "four", "five", "six", "seven", "eight", "nine")
                                .Add("ten", "eleven", "twelve", "thirteen", "fourteen", "sixteen", "seventeen", "eighteen", "nineteen")
                                .Add("twenty", "thirty", "fourty", "fifity", "sixty", "seventy", "eighty", "ninety")
                                .Add("cent")
                                .Add("cents")
                                .Add("dollar")
                                .Add("dollars");

            var factory = new AutomataFactory(alphabet);

            var model = factory.Deterministic()
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

            return model.CreateAutomata();
        }
    }
}