using GeneticAlgorithm.Mutations;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Operators.Mutations
{
    /// <summary>
    /// Base mutation abstract class.
    /// </summary>
    public abstract class Mutation: IMutation
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operator is ordered (if can keep the chromosome order).
        /// </summary>
        public bool IsOrdered { get; protected set; }

        /// <summary>
        /// Adaptation of mutation properties.
        /// </summary>
        public virtual void Adaptive() { }

        /// <summary>
        /// Mutate the specified individual.
        /// </summary>
        /// <param name="individual">The individual.</param>
        /// <param name="mutation_probabilty">The probability to mutate each indiviudal.</param>
        public abstract void Mutate(IIndividual individual, float mutation_probabilty);
    }
}
