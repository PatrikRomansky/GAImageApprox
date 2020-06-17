using GeneticAlgorithm.Individuals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm.Populations
{   
    /// <summary>
    /// Population fo GA.
    /// </summary>
    public class Population: IPopulation
    {
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size { get; set; }

        /// <summary>
        /// Create individual function.
        /// </summary>
        public Func<IIndividual> CreateIndividual { get; private set; }

        /// <summary>
        /// Gets the iindividuals.
        /// </summary>
        /// <value>The individuals.</value>
        public IList<IIndividual> Individuals { get; set; }


        /// <summary>
        /// Constructor for problem population.
        /// </summary>
        /// <param name="size">popoluation size,</param>
        /// <param name="createIndividual">Create individual function.</param>
        public Population(int size, Func<IIndividual> createIndividual)
        {
            Size = size;
            CreateIndividual = createIndividual;
        }

        /// <summary>
        /// Inititialize first population.
        /// </summary>
        public virtual void CreatePopulation()
        {
            Individuals = new List<IIndividual>();
            
            // random population
            for (int i = 0; i < Size; i++)
            {
                var c = CreateIndividual();
                Individuals.Add(c);
            }
        }

        public IIndividual GetBestIndividual()
        {
            Individuals = Individuals.OrderByDescending(c => c.Fitness.Value).ToList();
            return Individuals.First();
        }
    }
}
