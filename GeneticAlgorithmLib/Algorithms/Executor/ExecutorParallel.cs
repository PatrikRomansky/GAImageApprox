﻿using GeneticAlgorithm.Fitnesses;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Mutations;
using GeneticAlgorithm.Populations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Executor
{
    /// <summary>
    /// Parallel performs the following operations on the population (evuluate fitness, mutation, crossover)
    /// </summary>
    public class ExecutorParallel:ExecutorLinear
    {
        /// <summary>
        /// Parallel fitness evaluation.
        /// </summary>
        /// <param name="fitness">Fitness.</param>
        /// <param name="population">Input Population</param>
        public override void EvaluateFitness(IFitness fitness, IPopulation population)
        {
            Parallel.ForEach(population.Individuals, ind =>
            {
                if (!ind.Fitness.HasValue)
                {
                    ind.Fitness = fitness.Evaluate(ind);
                }
            });

            population.Individuals = population.Individuals.OrderByDescending(c => c.Fitness.Value).ToList();
        }

        /// <summary>
        /// Parallel mutaion execution.
        /// </summary>
        /// <param name="mutation">Mutation operator.</param>
        /// <param name="mutationProbability">Probability of mutaiton</param>
        /// <param name="individuals">Individual</param>
        public override void Mutate(IMutation mutation, float mutationProbability, IList<IIndividual> individuals)
        {
            Parallel.ForEach(individuals, ind =>
            {
                mutation.Mutate(ind, mutationProbability);
            });
        }
    }
}
