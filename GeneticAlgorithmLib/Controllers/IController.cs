using System;
using GeneticAlgorithm.Fitnesses;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Xover;
using GeneticAlgorithm.Mutations;
using GeneticAlgorithm.Selections;
using GeneticAlgorithm.Terminations;
using GeneticAlgorithm.Algorithms;
using GeneticAlgorithm.Elitizmus;
using GeneticAlgorithm.Executor;

namespace GeneticAlgorithm.Controllers
{
    /// <summary>
    /// Interface for setting the properties of solved problems.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        void Initialize(Object target);

        /// <summary>
        /// Initialize this instance operator components.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Initializes possible elitizmuses for this instance.
        /// </summary>
        void InitializeElitizmuses();

        /// <summary>
        /// Initializes possible crossoversfor this instance.
        /// </summary>
        void InitializeCrossovers();

        /// <summary>
        /// Initializes possible mutations for this instance.
        /// </summary>
        void InitializeMutations();

        /// <summary>
        /// Initializes possible selections for this instance.
        /// </summary>
        void InitializeSelections();

        /// <summary>
        /// Initializes possible terminations for this instance.
        /// </summary>
        void InitializeTermination();


        /// <summary>
        /// Configure the Genetic Algorithm.
        /// </summary>
        /// <param name="ga">The genetic algorithm.</param>
        void ConfigGATermination(GA ga);

        /// <summary>
        /// Creates the individual.
        /// </summary>
        /// <returns>The individual.</returns>
        IIndividual CreateIndividual();

        /// <summary>
        /// Draws the sample.
        /// </summary>
        /// <param name="bestIndividual">The current best individual.</param>
        Object ShowBestIndividual(IIndividual bestIndividual);

        /// <summary>
        /// Creates the fitness.
        /// </summary>
        /// <returns>The fitness.</returns>
        IFitness CreateFitness();

        /// <summary>
        /// Creates the termination.
        /// </summary>
        /// <param name="name">Termination name</param>
        /// <param name="param"></param>
        /// <returns>The termination</returns>
        ITermination CreateTermination(string name, object param);

        /// <summary>
        /// Creates the crossover.
        /// </summary>
        /// <param name="name">Crossover name.</param>
        /// <returns>The croosover</returns>
        IXover CreateCrossover(string name);

        /// <summary>
        /// Creates the mutation.
        /// </summary>
        /// <param name="name">Mutation name.</param>
        /// <returns>The mutation.</returns>
        IMutation CreateMutation(string name);


        /// <summary>
        /// Creates the selection.
        /// </summary>
        /// <param name="name">Selection name.</param>
        /// <returns>The selection.</returns>
        ISelection CreateSelection(string name);

        /// <summary>
        /// Creates the elitizmus.
        /// </summary>
        /// <param name="name">Elitizmus name.</param>
        /// <returns>The elitizmus.</returns>
        IElitizmus CreateElitizmus(string name, double perc);

        /// <summary>
        ///  Create the executor.
        /// </summary>
        /// <returns>The executor.</returns>
        IExecutor CreateExecutor();

        /// <summary>
        /// Gets possible elitizmuses for this instance.
        /// </summary>
        /// <returns>Possible elitizmuses.</returns>
        string[] PossibleElitizmuses();

        /// <summary>
        /// Gets possible crossover for this instance.
        /// </summary>
        /// <returns>Possible crossovers.</returns>
        string[] PossibleCrossovers();

        /// <summary>
        /// Gets possible mutations for this instance.
        /// </summary>
        /// <returns>Possible mutations.</returns>
        string[] PossibleMutations();

        /// <summary>
        /// Gets possible selections for this instance.
        /// </summary>
        /// <returns>Possible selections.</returns>
        string[] PossibleSelections();

        /// <summary>
        /// Gets possible terminations for this instance.
        /// </summary>
        /// <returns>Posiible terminations.</returns>
        string[] PossibleTerminations();
    }
}
