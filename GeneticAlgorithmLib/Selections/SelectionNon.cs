using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Populations;
using System.Collections.Generic;

namespace GeneticAlgorithm.Selections
{
    /// <summary>
    /// The Selection operator that does not make a selection.
    /// </summary>
    class SelectionNon : ISelection
    {
        /// <summary>
        /// Do nothing
        /// </summary>
        /// <param name="number">Number of selected.</param>
        /// <param name="generation">Cur. generation</param>
        /// <returns>Selected individuals.</returns>
        public IList<IIndividual> SelectIndividuals(int number, IPopulation generation)
        {
            return generation.Individuals;
        }
    }
}
