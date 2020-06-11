using GeneticAlgorithm.Algorithms;

namespace GeneticAlgorithm.Terminations
{
    /// <summary>
    /// Termination condition for genetic algorithm.
    /// The condition is the maximum number of generations
    /// </summary>
    public class TerminationMaxNumberGeneration : ITermination
    {
        /// <summary>
        /// Maximum number of generation.
        /// </summary>
        int ExpectedGenerationsNumber { get;}

        /// <summary>
        /// Constructor termination: Maximum number of generation.
        /// </summary>
        /// <param name="expectedGenerationNumber">Maximum number of generation.</param>
        public TerminationMaxNumberGeneration(int expectedGenerationNumber)
        {
            ExpectedGenerationsNumber = expectedGenerationNumber;
        }

        /// <summary>
        /// Determines whether the specified genetic algorithm fulfilled the termination condition.
        /// </summary>
        /// <param name="geneticAlgorithm">The genetic algorithm.</param>
        /// <returns>True if termination has been fulfilled, otherwise false.</returns>
        public bool IsFulfilled(IGeneticlgorithm geneticAlgorithm)
        {
            if (geneticAlgorithm.GenerationsNumber < ExpectedGenerationsNumber)
            {
                return false;
            }

            return true;
        }
    }
}
