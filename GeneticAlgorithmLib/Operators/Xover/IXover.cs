﻿using System.Collections.Generic;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Operators;

namespace GeneticAlgorithm.Xover
{
    /// <summary>
    /// Defines a interface for a crossover genetic operator.
    /// In genetic algorithms, xover is a genetic operator used to vary the programming of an individual
    /// or individuals from one generation to the next. 
    /// </summary>
    public interface IXover
    {
        /// <summary>
        /// Gets the number of parents need for cross.
        /// </summary>
        /// <value>The parents number.</value>
        int ParentsNumber { get;}

        /// <summary>
        /// Gets the number of children generated by cross.
        /// </summary>
        /// <value>The children number.</value>
        int ChildrenNumber { get; }

        /// <summary>
        /// Cross the specified parents generating the children.
        /// </summary>
        /// <param name="parents">The parents.</param>
        /// <returns>The offspring (children) of the parents.</returns>
        IList<IIndividual> Cross(IList<IIndividual> parents);
    }
}
