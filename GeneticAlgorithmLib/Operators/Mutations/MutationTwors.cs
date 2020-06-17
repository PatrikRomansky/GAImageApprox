using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Randomization;

namespace GeneticAlgorithm.Operators.Mutations
{
    /// <summary>
    /// Twors mutation operator.
    /// </summary>
    public class MutationTwors : MutationSwap
    {

        /// <summary>
        /// Mutate the specified individual in population.
        /// </summary>
        /// <param name="individual">The individual to be mutated.</param>
        /// <param name="mut_probability">The mutation probability to mutate each individual.</param>
        public override void Mutate(IIndividual individual, float mutation_probabilty)
        {
            if (RandomizationRnd.GetDouble() <= mutation_probabilty)
            {
                var indexes = RandomizationRnd.GetUniqueInts(2, 0, individual.Length);
                SwapGenes(indexes[0], indexes[1], individual);
            }
        }
    }
}
