using GeneticAlgorithm.Genes;

namespace GeneticAlgorithm.Individuals
{
    /// <summary>
    /// Defines an interface for an individual.
    /// </summary>
    public interface IIndividual
    {
        /// <summary>
        /// Gets or sets the fitness value.
        /// </summary>
        /// <value>The fitness.</value>
        double? Fitness { get; set; }

        /// <summary>
        /// Gets the length of individual.
        /// </summary>
        /// <value>The length.</value>
        int Length { get; }

        /// <summary>
        /// Resizes the individual to the new length.
        /// </summary>
        /// <param name="newLength">The new length.</param>
        void Resize(int newLength);

        /// <summary>
        /// Gets the gene in the specified index.
        /// </summary>
        /// <returns>The gene.</returns>
        /// <param name="index">The gene index.</param>
        Gene GetGene(int index);

        /// <summary>
        /// Gets the genes.
        /// </summary>
        /// <returns>The genes.</returns>
        Gene[] GetGenes();

        void AddGene(Gene gene);

        /// <summary>
        /// Generates the gene for the specified index.
        /// </summary>
        /// <returns>The gene.</returns>
        /// <param name="geneIndex">Gene index.</param>
        Gene GenerateGene();

        /// <summary>
        /// Replaces the gene in the specified index.
        /// </summary>
        /// <param name="index">The gene index to replace.</param>
        /// <param name="gene">The new gene.</param>
        void ReplaceGene(int index, Gene gene);

        /// <summary>
        /// Replaces the genes starting in the specified index.
        /// </summary>
        /// <remarks>
        /// The genes to be replaced can't be greater than the available space between the start index and the end of the individual.
        /// </remarks>
        /// <param name="startIndex">Start index.</param>
        /// <param name="genes">The genes.</param>
        void ReplaceGenes(int startIndex, Gene[] genes);

        /// <summary>
        /// Creates a new individual using the same structure of this.
        /// </summary>
        /// </summary>
        /// <returns>The new individual.</returns>
        IIndividual CreateNew();

        /// <summary>
        /// Creates a clone.
        /// </summary>
        /// <returns>The individualclone.</returns>
        IIndividual Clone();

        /// <summary>
        /// Initialization genes.
        /// </summary>
        void Initialize();
    }
}
