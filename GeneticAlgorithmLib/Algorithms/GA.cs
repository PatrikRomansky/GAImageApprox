﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Populations;
using GeneticAlgorithm.Fitnesses;
using GeneticAlgorithm.Mutations;
using GeneticAlgorithm.Xover;
using GeneticAlgorithm.Selections;
using GeneticAlgorithm.Terminations;
using GeneticAlgorithm.Elitizmus;
using GeneticAlgorithm.Executor;

namespace GeneticAlgorithm.Algorithms
{
    /// <summary>
    /// A genetic algorithm is an algorithm that imitates the process of natural selection.
    /// They help solve optimization and search problems. Genetic algorithms are part 
    /// of the bigger class of evolutionary algorithms. Genetic algorithms imitate natural biological processes,
    /// such as inheritance, mutation, selection and crossover.
    /// </summary>
    public class GA : IGeneticlgorithm
    {
        protected IFitness fitness;

        /// <summary>
        ///  Operators
        /// </summary>
        protected IXover xover;
        protected IMutation mutation;
        protected ISelection selection;
        protected ITermination termination;
        protected IElitizmus elitizmus;

        // Executor
        protected IExecutor executor;

        protected IPopulation Population;

        private float xoverProbability;
        private float mutationProbability;

        /// <summary>
        /// timer
        /// </summary>
        protected Stopwatch stopwatch;
        
        /// <summary>
        /// Occurs when generation ran.
        /// </summary>
        public event EventHandler CurrentGenerationInfo;

        /// <summary>
        /// Occurs when termination reached.
        /// </summary>
        public event EventHandler TerminationReached;

        /// <summary>
        /// Gets the generations number.
        /// </summary>
        /// <value>The generations number.</value>
        public int CurrentGenerationsNumber
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the best chromosome.
        /// </summary>
        /// <value>The best chromosome.</value>
        public IIndividual BestIndividual
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the time evolving.
        /// </summary>
        public TimeSpan TimeEvolving { get; protected set; }


        /// <summary>
        /// Constructor for genetic algorithm.
        /// Genetic algorithms imitate natural biological processes,
        /// </summary>
        /// <param name="population">Init population. </param>
        /// <param name="fitness">Fitness.</param>
        /// <param name="selection">Selection operator.</param>
        /// <param name="xover">Crossover operator.</param>
        /// <param name="mutation">Mutation operator.</param>
        /// <param name="elitizmus">Elitizmus.</param>
        /// <param name="termination">Termination GA.</param>
        /// <param name="executor">Executor.</param>
        /// <param name="mutationProbability">Mutation probability.</param>
        /// <param name="xoverProbability">Xover probability.</param>
        public GA(IPopulation population, 
                    IFitness fitness, 
                    ISelection selection, 
                    IXover xover, 
                    IMutation mutation, 
                    IElitizmus elitizmus, 
                    ITermination termination, 
                    IExecutor executor,
                    float mutationProbability,
                    float xoverProbability)
        {
            Population = population;
            this.fitness = fitness;
            this.selection = selection;
            this.xover = xover;
            this.mutation = mutation;
            this.elitizmus = elitizmus;
            this.executor = executor;
            this.termination = termination;

            // base probability
            this.xoverProbability = xoverProbability;
            this.mutationProbability = mutationProbability;
            TimeEvolving = TimeSpan.Zero;
            CurrentGenerationsNumber = 1;
        }

        // termination algorithm
        protected volatile bool terminationConditionReached = false;

        public void HandlerInvoke(EventHandler handler)
        {
            handler?.Invoke(this, EventArgs.Empty);
        }

        public void InitilizatePopulation()
        {
            stopwatch = Stopwatch.StartNew();
            Population.CreatePopulation();
            stopwatch.Stop();
            TimeEvolving = stopwatch.Elapsed;
        }

        /// <summary>
        /// Starts the genetic algorithm using population, fitness, selection, crossover,
        /// mutation and termination configured.
        /// </summary>
        public void Run()
        {
            InitilizatePopulation();

            do
            {
                stopwatch.Restart();
                EvolveCurrentGeneration();
                stopwatch.Stop();
                TimeEvolving += stopwatch.Elapsed;               
            }
            while (!terminationConditionReached);
        }

        public void Stop()
        {
            terminationConditionReached = true;
            stopwatch.Stop();
        }

        /// <summary>
        /// Evolve one generation.
        /// </summary>
        public void EvolveCurrentGeneration()
        {
            EvaluateFitness();
            BestIndividual = Population.GetBestIndividual();

            HandlerInvoke(CurrentGenerationInfo);

            if (termination.IsFulfilled(this))
            {
                HandlerInvoke(TerminationReached);
                terminationConditionReached = true;
            }

            var parents = SelectParents();
            var offspring = Cross(parents);
            Mutate(offspring);
            offspring = SelectElite(offspring, parents);

            Population.Individuals = offspring;    
            
            CurrentGenerationsNumber++;
        }

        /// <summary>
        /// Evaluates the fitness.
        /// </summary>
        protected void EvaluateFitness()
        {
            executor.EvaluateFitness(fitness, Population);
        }

        /// <summary>
        /// Selects the parents.
        /// </summary>
        /// <returns>The parents.</returns>
        protected IList<IIndividual> SelectParents()
        {
            return selection.SelectIndividuals(Population.Size, Population);
        }

        /// <summary>
        /// Crosses the specified parents.
        /// </summary>
        /// <param name="parents">The parents.</param>
        /// <returns>The result individuals.</returns>
        protected IList<IIndividual> Cross(IList<IIndividual> parents)
        {
            return executor.Cross(Population, xover, xoverProbability, parents);
        }

        // for adaptation or termination(fitness)
        private double? previuosBestFit = 0; 

        /// <summary>
        /// Mutate the specified individuals.
        /// </summary>
        /// <param name="individuals">The individuals.</param>
        protected void Mutate(IList<IIndividual> individuals)
        {
            // new best
            var fit = BestIndividual.Fitness;
            // compere best inds.
            if (fit != null & previuosBestFit * 1.5 <= fit)
            {
                previuosBestFit = fit;
                mutation.Adaptive();
            }

            executor.Mutate(mutation, mutationProbability, individuals);
        }

        /// <summary>
        /// Performs elitizmus.
        /// </summary>
        /// <param name="offspring">Children.</param>
        /// <param name="parents">Parents.</param>
        /// <returns>Offspring</returns>
        protected IList<IIndividual> SelectElite(IList<IIndividual> offspring, IList<IIndividual> parents) 
        {
            return elitizmus.EliteIndividuals(Population.Size, offspring, parents);
        }
    }
}
