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
        private TimeSpan maxTime;

        public TerminationMaxTime()
        {
            this.maxTime = new TimeSpan(0, 0, 60);
        }

        public void InitializeTerminationCondition(object termination)
        {
            var sec = (int)termination;
            maxTime = new TimeSpan(0, 0, sec);
        }

        /// <summary>
        /// Determines whether the specified genetic algorithm fulfilled the termination condition.
        /// </summary>
        /// <param name="geneticAlgorithm">The genetic algorithm.</param>
        /// <returns>True if termination has been fulfilled, otherwise false.</returns>
        public bool IsFulfilled(IGeneticlgorithm geneticAlgorithm)
        {
            if (geneticAlgorithm.TimeEvolving < maxTime)
            {
                return false;
            }

            return true;
        }
    }
}
