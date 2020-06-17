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
        /// Gets or sets the size of population.
        /// </summary>
        /// <value>The size.</value>
        int Size { get; set; }

        /// <summary>
        /// Gets the individuals.
        /// </summary>
        /// <value>The individuals.</value>
        IList<IIndividual> Individuals { get; set; }

        /// <summary>
        /// Create individual function.
        /// </summary>
        Func<IIndividual> CreateIndividual { get; }

        /// <summary>
        /// Creates the initial population.
        /// </summary>
        void CreatePopulation();

        IIndividual GetBestIndividual();
    }
}
