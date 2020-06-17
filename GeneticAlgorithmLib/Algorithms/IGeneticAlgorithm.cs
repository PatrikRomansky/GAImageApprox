using System;
using System.Collections.Generic;
using Emgu.CV;
using GeneticAlgorithm.Individuals;

namespace GeneticAlgorithm.Algorithms
{
    /// <summary>
    /// Interface for the genetic algorithm, which describe base functionality.
    /// </summary>
    public interface IGeneticlgorithm
    {
        /// <summary>
        /// Occurs when generation ran.
        /// </summary>
        event EventHandler CurrentGenerationInfo;

        /// <summary>
        /// Occurs when termination reached.
        /// </summary>
        event EventHandler TerminationReached;


        void HandlerInvoke(EventHandler handler);


        /// <summary>
        /// Gets the generations number.
        /// </summary>
        /// <value>The generations number.</value>
        int CurrentGenerationsNumber { get; }
        
        /// <summary>
        /// Gets the best chromosome.
        /// </summary>
        /// <value>The best chromosome.</value>
        IIndividual BestIndividual { get; }

        /// <summary>
        /// Gets the time evolving.
        /// </summary>
        /// <value>The time evolving.</value>
        TimeSpan TimeEvolving { get; }

        /// <summary>
        /// Run GA.
        /// </summary>
        void Run();

        /// <summary>
        /// Stop GA(evolution);
        /// </summary>
        void Stop();

        void EvolveCurrentGeneration();

        void InitilizatePopulation();
    }
}
