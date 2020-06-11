using System.Collections.Generic;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Populations;

namespace GeneticAlgorithm.Selections
{
    /// <summary>
    /// Defines a interface for selection.
    /// </summary>
    public interface ISelection
    {
        /// <summary>
        /// Selects the number of individuals from the generation.
        /// </summary>
        /// <param name="number">Number of selected.</param>
        /// <param name="generation">Cur. generation</param>
        /// <returns>Selected individuals.</returns>
        IList<IIndividual> SelectIndividuals(int number, IPopulation generation);
    }
}
