using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Randomization;

namespace GeneticAlgorithm.Operators.Mutations
{
    /// <summary>
    /// unifrom mutation operator.
    /// </summary>
    class MutationUniform : Mutation
    {
        /// <summary>
        /// Mutate the specified individual.
        /// </summary>
        /// <param name="individual">The individual to be mutated.</param>
        /// <param name="mutation_probabilty">The mutation probability to mutate each individual.</param>
        public override void Mutate(IIndividual individual, float mutation_probabilty)
        {
            for (int index = 0; index < individual.Length; index++)
            {         
                if (RandomizationRnd.GetDouble() <= mutation_probabilty)
                {
                    individual.ReplaceGene(index, individual.GenerateGene());
                }
            }
        }
    }
}
