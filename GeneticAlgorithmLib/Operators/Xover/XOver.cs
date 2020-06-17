using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Xover;
using System.Collections.Generic;

namespace GeneticAlgorithm.Operators.Xover
{
    /// <summary>
    /// Base crossover class.
    /// Sets base property.
    /// </summary>
    public abstract class Xover : IXover
    {
        public int ParentsNumber{ get; protected set; }
        public int ChildrenNumber { get; protected set; }
        public abstract IList<IIndividual> Cross(IList<IIndividual> parents);
    }
}
