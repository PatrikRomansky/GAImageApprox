using System;
using System.Collections.Generic;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Populations
{
    /// <summary>
    /// Defines an interface for a population of candidate solutions (individuals).
    /// </summary>
    public interface IPopulation
    {
        /// <summary>
        /// Gets the creation date.
        /// </summary>
        DateTime CreationPopulationDate { get; }

        /// <summary>
        /// Gets the creation date of current generation.
        /// </summary>
        DateTime CreationGenerationDate { get; }

        /// <summary>
        /// Gets the total number of generations executed.
        /// <remarks>
        /// Use this information to know how many generations have been executed, because Generations.Count can vary depending of the IGenerationStrategy used.
        /// </remarks>
        /// </summary>
        int CurrentGenerationNumber { get;}

        /// <summary>
        /// Gets or sets the size of population.
        /// </summary>
        /// <value>The size.</value>
        int Size { get; set; }

        /// <summary>
        /// Gets the best individual.
        /// </summary>
        /// <value>The best individual.</value>
        IIndividual BestIndividual { get; }

        /// <summary>
        /// Gets the individuals.
        /// </summary>
        /// <value>The individuals.</value>
        IList<IIndividual> Individuals { get; set; }

        /// <summary>
        /// Creates the initial population.
        /// </summary>
        void CreateInitialPopulation();

        /// <summary>
        /// Creates a new generation.
        /// </summary>
        /// <param name="individuals"></param>
        void CreateNewGeneration(IList<IIndividual> individuals);

        /// <summary>
        /// Ends the current generation.
        /// </summary>        
        void EndCurrentGeneration();
    }
}
