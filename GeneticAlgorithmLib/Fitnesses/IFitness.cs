using GeneticAlgorithm.Genes;
using GeneticAlgorithm.Individuals;
using System.ComponentModel;

namespace GeneticAlgorithm.Fitnesses
{
    /// <summary>
    ///  Defines an interface for fitness function.
    /// </summary>
    public interface IFitness
    {
        
        Gene[] TargetGenes { get; }

        int TargetSize { get; }

        void Initialize(object target);

        /// <summary>
        /// Performs the evaluation against the specified individual.
        /// </summary>
        /// <param name="individual">The individual to be evaluated.</param>
        /// <returns>The fitness of the individual.</returns>
        double Evaluate(IIndividual individual);
    }
}
