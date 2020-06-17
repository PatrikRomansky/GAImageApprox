using GeneticAlgorithm.Algorithms;

namespace GeneticAlgorithm.Terminations
{
    /// <summary>
    /// Termination condition for genetic algorithm.
    /// </summary>
    public interface ITermination
    {
        void InitializeTerminationCondition(object terminationCondition);

        /// <summary>
        /// Determines whether the specified genetic algorithm fulfilled the termination condition.
        /// </summary>
        /// <returns>True if termination has been fulfilled otherwise false.</returns>
        /// <param name="geneticAlgorithm">The genetic algorithm.</param>
        bool IsFulfilled(IGeneticlgorithm geneticAlgorithm);
    }
}
