using GeneticAlgorithm.Algorithms;

namespace GeneticAlgorithm.Terminations
{
    /// <summary>
    /// Termination condition for genetic algorithm.
    /// The condition is the maximum number of generations
    /// </summary>
    public class TerminationMaxNumberGeneration : ITermination
    {
        private int expectedMaxGeneration;


        public TerminationMaxNumberGeneration()
        {
            expectedMaxGeneration = 1000;
        }

        public void InitializeTerminationCondition(object termination)
        {
            expectedMaxGeneration = (int)termination;
        }

        /// <summary>
        /// Determines whether the specified genetic algorithm fulfilled the termination condition.
        /// </summary>
        /// <param name="geneticAlgorithm">The genetic algorithm.</param>
        /// <returns>True if termination has been fulfilled, otherwise false.</returns>
        public bool IsFulfilled(IGeneticlgorithm geneticAlgorithm)
        {
            if (geneticAlgorithm.CurrentGenerationsNumber < expectedMaxGeneration)
            {
                return false;
            }

            return true;
        }
    }
}
