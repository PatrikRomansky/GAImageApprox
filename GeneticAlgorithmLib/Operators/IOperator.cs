namespace GeneticAlgorithm.Operators
{
    /// <summary>
    /// The operator for genetic algorithm.
    /// </summary>
    public interface IOperator
    {
        /// <summary>
        /// Gets a value indicating whether the operator is ordered (if can keep the individual order).
        /// </summary>
        bool IsOrdered { get; }
    }
}
