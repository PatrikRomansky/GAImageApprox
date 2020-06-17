using GeneticAlgorithm.Algorithms;
using GeneticAlgorithm.Controllers;
using GeneticAlgorithm.Elitizmus;
using GeneticAlgorithm.Mutations;
using GeneticAlgorithm.Selections;
using GeneticAlgorithm.Terminations;
using GeneticAlgorithm.Xover;
using GeneticAlgorithmLib.Controllers.Img;
using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Populations;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace GaImageApproxConsole
{
    class Program
    {
        /// <summary>
        /// Selects all non-abstract classes implementing the interface TInterface.
        /// </summary>
        /// <typeparam name="TInterface">Interfface.</typeparam>
        /// <returns>Non-abstract classe.</returns>
        public static Dictionary<string, Type> selectType<TInterface>()
        {

            var result = new Dictionary<string, Type>();

            var type = typeof(TInterface);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p) & p.IsClass & !p.IsAbstract).ToArray();

            foreach (var t in types)
            {
                result.Add(t.Name, t);
            }

            return result;
        }

        /// <summary>
        /// Displays the controllers and the user can choose the controller.
        /// The selection is made by a serial number.
        /// </summary>
        /// <returns>Controller type.</returns>
        private static Type SetControllers()
        {
            Type controller;

            try
            {               
                Dictionary<string, Type> controllers = new Dictionary<string, Type>();
                
                // Possible controllers
                controllers = selectType<IController>();
                Console.WriteLine("Controlers: ");
                
                // show controllers
                int i = 1;
                foreach (var c in controllers.Keys.ToArray())
                    Console.WriteLine((i++) + ". " + c);

                Console.Write("Controlers number: ");

                // choose number of controller
                var StringNUm = Console.ReadLine();
                int number;
                number = int.Parse(StringNUm);

                controller = controllers.Values.ToList()[number - 1];
            }
            catch
            {
                Console.WriteLine("Chosse controller.");
                controller = SetControllers();
            }

            return controller;
        }

        /// <summary>
        /// Load and save in int variable population size.
        /// </summary>
        /// <returns>Population size.</returns>
        private static int SetPopulationSize()
        {
           return SetIntParameter("Pop size");
        }

        /// <summary>
        /// Reading a parameter that should be int.
        /// Loading until it succeeds.
        /// </summary>
        /// <param name="name">Parameter name.</param>
        /// <returns>Parameter value.</returns>
        private static int SetIntParameter(string name)
        {
            Console.Write(name + ": ");
            int parameter;
            try
            {
                var stringNum = Console.ReadLine();

                parameter = int.Parse(stringNum);

            }
            catch
            {
                Console.WriteLine(name +  " is integer.");
                parameter = SetPopulationSize();
            }
            return parameter;
        }

        /// <summary>
        /// Reading a parameter that should be float.
        /// Loading until it succeeds.
        /// </summary>
        /// <param name="name">Parameter name</param>
        /// <returns>Parameter value.</returns>
        private static float SetFloatParameter(string name)
        {
            Console.Write(name + ": ");
            float parameter;
            try
            {
                var stringNum = Console.ReadLine();

                parameter = float.Parse(stringNum);

            }
            catch
            {
                Console.WriteLine("Wrong " + name + " parameter.");
                parameter = SetFloatParameter(name);
            }
            return parameter;
        }

        /// <summary>
        /// Displays the operators and the user can choose the one of them.
        /// The selection is made by a serial number.
        /// </summary>
        /// <param name="operators">Possible operators for the selected type of operator and controller./param>
        /// <param name="operatorName">Operator type name.</param>
        /// <returns></returns>
        private static string PrintSelectedOperators(string[] operators, string operatorName)
        {
            Console.WriteLine(operatorName);
            int i = 1;

            foreach (var o in operators)
                Console.WriteLine((i++) + ". " + o);

            Console.Write("Selected: ");
            string slectedOperatorName;
            try
            {
                var stringNum = Console.ReadLine();
                int number;
                number = int.Parse(stringNum);
                slectedOperatorName = operators[number - 1];
            }
            catch
            {
                Console.WriteLine("Wrong " + operatorName + ".");
                slectedOperatorName = PrintSelectedOperators(operators, operatorName);
            }

            return slectedOperatorName;

        }

        /// <summary>
        /// Displays the crossovers and the user can choose the one of them.
        /// The selection is made by a serial number.
        /// </summary>
        /// <param name="sampleController">Problem controller.</param>
        /// <returns>Selected crossover.</returns>
        private static IXover SetXover(IController sampleController)
        {
            var operators = sampleController.PossibleCrossovers();

            var name = PrintSelectedOperators(operators, "Crossover");

            return sampleController.CreateCrossover(name);
        }

        /// <summary>
        /// Displays the elitizmuses and the user can choose the one of them.
        /// The selection is made by a serial number.
        /// </summary>
        /// <param name="sampleController">Problem controller.</param>
        /// <returns>Selected elitizmus.</returns>
        private static IElitizmus SetElitizmus(IController sampleController)
        {
            var operators = sampleController.PossibleElitizmuses();

            var name = PrintSelectedOperators(operators, "Elitizmus");


            var paramter = SetFloatParameter("Elite percentage") / 100;

            return sampleController.CreateElitizmus(name, paramter);
        }

        /// <summary>
        /// Displays the mutations and the user can choose the one of them.
        /// The selection is made by a serial number.
        /// </summary>
        /// <param name="sampleController">Problem controller.</param>
        /// <returns>Selected mutation.</returns>
        private static IMutation SetMutation(IController sampleController)
        {
            var mutations = sampleController.PossibleMutations();

            var mutName = PrintSelectedOperators(mutations, "Mutation");


            return sampleController.CreateMutation(mutName);
        }

        /// <summary>
        /// Displays the selections and the user can choose the one of them.
        /// </summary>
        /// <param name="sampleController">Problem controller.</param>
        /// <returns>Selected selection.</returns>
        private static ISelection SetSelection(IController sampleController)
        {
            var operators = sampleController.PossibleSelections();

            var name = PrintSelectedOperators(operators, "Selection");

            return sampleController.CreateSelection(name);
        }

        /// <summary>
        /// Displays the terminations and the user can choose the one of them.
        /// </summary>
        /// <param name="sampleController">Problem controller.</param>
        /// <returns>Selected termination.</returns>
        private static ITermination SetTermination(IController sampleController)
        {
            var operators = sampleController.PossibleTerminations();

            // possible terminations
            var name = PrintSelectedOperators(operators, "Termination");

            // end point of the condition
            var parameter = SetIntParameter("Termination parameter");

            return sampleController.CreateTermination(name, parameter);
        }


        static void printInt(object i)
        {
            int a = (int)i;
            Console.WriteLine(a);
        }

        static void Main(string[] args)
        {
            printInt(10);



            // input image
            Console.WriteLine("Input image: ");
            var inputImageFile = Console.ReadLine();
            inputImageFile = inputImageFile.Replace("\\", "/");

            // Image controller name
            var controllerName = SetControllers();
            var consoleSampleController = Activator.CreateInstance(controllerName) as IControllerImage;
            consoleSampleController.Initialize();

            // Scale img
            var paramterScale = SetFloatParameter("Scale");

            consoleSampleController.InitializeScale(paramterScale);
            consoleSampleController.Initialize(inputImageFile);
     
            var popSize = SetPopulationSize();

            // new operators
            var fitness = consoleSampleController.CreateFitness();
            var crossover = SetXover(consoleSampleController);
            var xoverProb = SetFloatParameter("Xover Probability");

            var elitizmus = SetElitizmus(consoleSampleController);
            
            var mutation = SetMutation(consoleSampleController);
            var mutationProb = SetFloatParameter("Mutation Probability");

            var selection = SetSelection(consoleSampleController);
            var termination = SetTermination(consoleSampleController);
            var executor = consoleSampleController.CreateExecutor();

            // creates population with curr. popSize
            var population = new Population(popSize, consoleSampleController.CreateIndividual);

            // new genetic algorithm
            var ga = new GA(population, fitness, selection, crossover, mutation, elitizmus, termination, executor);
            ga.XoverProbability = xoverProb / 100; ;
            ga.MutationProbability = mutationProb / 100; ;

            ga.GenerationInfo += delegate
            {
                var bestIndividual = ga.Population.BestIndividual;
                Console.WriteLine("Generations: {0}", ga.Population.CurrentGenerationNumber);
                Console.WriteLine("Fitness: {0,10}", bestIndividual.Fitness);
                Console.WriteLine("Time: {0}", ga.TimeEvolving);

                var speed = ga.TimeEvolving.TotalSeconds / ga.Population.CurrentGenerationNumber;
                Console.WriteLine("Speed (gen/sec): {0:0.0000}", speed);

                var best = consoleSampleController.ShowBestIndividual(bestIndividual);
        
            };

            consoleSampleController.ConfigGATermination(ga);
            ga.Run();


            Console.WriteLine("EVOLVED");
            Console.ReadKey();
        }
    }
}
