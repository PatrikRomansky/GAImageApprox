﻿using GeneticAlgorithm.Fitnesses;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Mutations;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Xover;
using System.Collections.Generic;

namespace GeneticAlgorithm.Executor
{
    /// <summary>
    /// Performs the following operations on the population (evuluate fitness, mutation, crossover)
    /// </summary>
    public interface IExecutor
    {   
        /// <summary>
        /// Fitness executor.
        /// </summary>
        /// <param name="fitness">Fitness.</param>
        /// <param name="population">Input population</param>
        void EvaluateFitness(IFitness fitness, IPopulation population);

        /// <summary>
        /// Mutation executor.
        /// </summary>
        /// <param name="mutation">Mutation operator.</param>
        /// <param name="mutationProbability">Mutation probability</param>
        /// <param name="individuals">Individual</param>
        void Mutate(IMutation mutation, float mutationProbability, IList<IIndividual> individuals);

        /// <summary>
        /// Crossover executor.
        /// </summary>
        /// <param name="population">Input population.</param>
        /// <param name="xover">Crossover operator.</param>
        /// <param name="xoverProbability">Crossover probability</param>
        /// <param name="parents">Parents list</param>
        /// <returns>Childten(individuals)</returns>
        IList<IIndividual> Cross(IPopulation population, IXover xover, float xoverProbability, IList<IIndividual> parents);
    }
}
