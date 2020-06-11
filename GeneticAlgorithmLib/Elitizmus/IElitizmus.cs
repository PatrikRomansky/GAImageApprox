using System.Collections.Generic;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Elitizmus
{
    /// <summary>
    /// Interface for elitizmus genetic operator.
    /// Reinsert the number of individuals from the previous generation.
    /// </summary>
    public interface IElitizmus
    {
        /// <summary>
        /// Percentage of survivors.
        /// </summary>
        double ElitePercentage { get; }

        /// <summary>
        /// Reinsert the number of individuals from the previous generation.
        /// </summary>
        /// <param name="popSize">Population size.</param>
        /// <param name="offspring">New individuals.</param>
        /// <param name="parents">The Parents.</param>
        /// <returns>The elite population.</returns>
        IList<IIndividual> EliteIndividuals(int popSize, IList<IIndividual> offspring, IList<IIndividual> parents);
    }
}
