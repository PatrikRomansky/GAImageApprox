using GeneticAlgorithm.Individuals;
using System.Collections.Generic;
using System.Linq;


namespace GeneticAlgorithm.Elitizmus
{
    /// <summary>
    /// The operator of the elitizmus that performs the elite selection.
    /// </summary>
    public class ElitizmusFitness : IElitizmus
    {
        /// <summary>
        /// Percentage of survivors.
        /// </summary>
        public double ElitePercentage { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elitePercentage">Percentage of survivors.</param>
        public ElitizmusFitness(double elitePercentage = 0.1)
        {
            ElitePercentage = elitePercentage;
        }

        /// <summary>
        /// Reinsert the number of individuals from the previous generation.
        /// </summary>
        /// <param name="popSize">Population size.</param>
        /// <param name="offspring">New individuals.</param>
        /// <param name="parents">The parents.</param>
        /// <returns>The elite population.</returns>
        public IList<IIndividual> EliteIndividuals(int popSize, IList<IIndividual> offspring, IList<IIndividual> parents)
        {
            int eliteIndsCount = (int) (popSize * ElitePercentage);

            if (eliteIndsCount < 1)
                return offspring;

            var ordered = parents.OrderByDescending(c => c.Fitness).Take(eliteIndsCount).ToList();

            for (int i = 0; i < popSize - eliteIndsCount; i++)
            {
                ordered.Add(offspring[i]);
            }


            return ordered;
        }
    }
}
