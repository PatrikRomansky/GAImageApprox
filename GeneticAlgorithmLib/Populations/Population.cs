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
        /// Gets the creation date.
        /// </summary>
        public DateTime CreationPopulationDate { get; private set; }

        /// <summary>
        /// Gets the creation date of current generation.
        /// </summary>
        public DateTime CreationGenerationDate { get; private set; }

        /// <summary>
        /// Gets the iindividuals.
        /// </summary>
        /// <value>The individuals.</value>
        public IList<IIndividual> Individuals { get; set; }

        /// <summary>
        /// Gets the number.
        /// </summary>
        /// <value>The number.</value>
        public int CurrentGenerationNumber { get; private set; }

        /// <summary>
        /// Gets or sets the best individual.
        /// </summary>
        /// <value>The best individual.</value>
        public IIndividual BestIndividual { get; protected set; }

        /// <summary>
        /// Create individual function.
        /// </summary>
        public Func<IIndividual> CreateIndividual;

        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>The size.</value>
        public int Size { get; set; }

        /// <summary>
        /// Constructor for problem population.
        /// </summary>
        /// <param name="size">popoluation size,</param>
        /// <param name="createIndividual">Create individual function.</param>
        public Population(int size, Func<IIndividual> createIndividual)
        {
            CreationPopulationDate = DateTime.Now;
            Size = size;

            CreateIndividual = createIndividual;
        }

        /// <summary>
        /// Inititialize first population.
        /// </summary>
        public virtual void CreateInitialPopulation()
        {
            CurrentGenerationNumber = 0;

            var individuals = new List<IIndividual>();

            // random population
            for (int i = 0; i < Size; i++)
            {
                var c = CreateIndividual();
                individuals.Add(c);
            }

            // create population
            CreateNewGeneration(individuals);
        }

        /// <summary>
        /// Creates a new generation.
        /// </summary>
        /// <param name="individuals">The individuals for new generation.</param>
        public virtual void CreateNewGeneration(IList<IIndividual> individuals)
        {
            CreationGenerationDate = DateTime.Now;
            Individuals = individuals;
            CurrentGenerationNumber++;
        }

        /// <summary>
        /// Ends the current generation.
        /// </summary>        
        public virtual void EndCurrentGeneration()
        {
            Individuals = Individuals.OrderByDescending(c => c.Fitness.Value).ToList();

            if (Individuals.Count > Size)
            {
                Individuals = Individuals.Take(Size).ToList();
            }
            // store best ind.
            BestIndividual = Individuals.First();
        }
    }
}
