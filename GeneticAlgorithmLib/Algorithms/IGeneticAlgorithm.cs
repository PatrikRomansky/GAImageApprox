using System;
using System.Collections.Generic;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Algorithms
{
    /// <summary>
    /// Interface for the genetic algorithm, which describe base functionality.
    /// </summary>
    public interface IGeneticlgorithm
    {        
        /// <summary>
        /// Gets the generations number.
        /// </summary>
        /// <value>The generations number.</value>
        int GenerationsNumber { get; }

        /// <summary>
        /// Run GA.
        /// </summary>
        void Run();

        /// <summary>
        /// Stop GA(evolution);
        /// </summary>
        void Stop();

        /// <summary>
        /// Gets the best chromosome.
        /// </summary>
        /// <value>The best chromosome.</value>
        IIndividual BestIndividual { get; }

        /// <summary>
        /// Gets the time evolving.
        /// </summary>
        /// <value>The time evolving.</value>
        TimeSpan TimeEvolving { get; }
        
        /// <summary>
        /// Pefroms crossover operator.
        /// </summary>
        /// <param name="parents">Parents</param>
        /// <returns>Offspring.</returns>
        IList<IIndividual> Cross(IList<IIndividual> parents);

        /// <summary>
        /// Performs mutation operator.
        /// </summary>
        /// <param name="individials">Individual.</param>
        void Mutate(IList<IIndividual> individials);
    }
}
