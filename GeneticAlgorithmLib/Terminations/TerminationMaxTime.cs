using GeneticAlgorithm.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticAlgorithm.Terminations
{
    /// <summary>
    /// Termination condition for genetic algorithm.
    /// The condition is the maximum time.
    /// </summary>
    class TerminationMaxTime : ITermination
    {
        /// <summary>
        /// Gets the execution maximum time.
        /// </summary>
        /// <value>The maximum time.</value>
        public TimeSpan MaxTime { get;}

        /// <summary>
        /// Constructor termination: Maximum time.
        /// </summary>
        /// <param name="maxTime">Expected maxmum time (sec).</param>
        public TerminationMaxTime(int maxTime)
        {
            MaxTime = new TimeSpan(0,0, maxTime);
        }

        /// <summary>
        /// Determines whether the specified genetic algorithm fulfilled the termination condition.
        /// </summary>
        /// <param name="geneticAlgorithm">The genetic algorithm.</param>
        /// <returns>True if termination has been fulfilled, otherwise false.</returns>
        public bool IsFulfilled(IGeneticlgorithm geneticAlgorithm)
        {
            if (geneticAlgorithm.TimeEvolving < MaxTime)
            {
                return false;
            }

            return true;
        }
    }
}
