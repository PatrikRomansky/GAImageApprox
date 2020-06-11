using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Operators;

namespace GeneticAlgorithm.Mutations
{
    /// <summary>
    /// Defines an interface for mutation function.
    /// Mutation is a genetic operator used to maintain genetic diversity.
    /// </summary>
    public interface IMutation:IOperator
    {
        /// <summary>
        /// Adaptation of mutation properties.
        /// </summary>
        void Adaptive();

        /// <summary>
        /// Mutate the specified individual in population.
        /// </summary>
        /// <param name="individual">The individual to be mutated.</param>
        /// <param name="mut_probability">The mutation probability to mutate each individual.</param>
        void Mutate(IIndividual individual, float mut_probability);
    }
}
