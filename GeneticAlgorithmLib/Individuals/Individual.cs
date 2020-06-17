using System;
using GeneticAlgorithm.Genes;

namespace GeneticAlgorithm.Individuals
{
    /// <summary>
    /// Base abstract class for GA-individual.
    /// </summary>
    public abstract class Individual : IIndividual
    {
        private Gene[] genes;
        private int length;

        /// <summary>
        /// Gets or sets the fitness of the chromosome in the current problem.
        /// </summary>
        public double? Fitness { get; set; }

        /// <summary>
        /// Gets the length, in genes, of the chromosome.
        /// </summary>
        public int Length { get { return this.length; } }

        /// <summary>
        /// Initializes a new instance of the IndividualDefault.
        /// </summary>
        /// <param name="length">The length, in genes, of the chromosome.</param>
        protected Individual(int length)
        {
            this.length = length;
            this.genes = new Gene[length];
        }

        /// <summary>
        /// Generates the gene for the specified index.
        /// </summary>
        /// <param name="geneIndex">Gene index.</param>
        /// <returns>The gene generated at the specified index.</returns>
        public abstract Gene GenerateGene();

        /// <summary>
        /// Initialization genes.
        /// </summary>
        public virtual void Initialize()
        {
            for (int i = 0; i < Length; i++)
            {
                ReplaceGene(i, GenerateGene());
            }
        }

        /// <summary>
        /// Creates a new chromosome using the same structure of this.
        /// </summary>
        /// <returns>The new chromosome.</returns>
        public abstract IIndividual CreateNew();

        /// <summary>
        /// Gets the gene in the specified index.
        /// </summary>
        /// <param name="index">The gene index.</param>
        /// <returns>
        /// The gene.
        /// </returns>
        public Gene GetGene(int index)
        {
            return this.genes[index];
        }

        /// <summary>
        /// Gets the genes.
        /// </summary>
        /// <returns>The genes.</returns>
        public Gene[] GetGenes()
        {
            return this.genes;
        }

        /// <summary>
        /// Replaces the gene in the specified index.
        /// </summary>
        /// <param name="index">The gene index to replace.</param>
        /// <param name="gene">The new gene.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index;There is no Gene on index {0} to be replaced..With(index)</exception>
        public void ReplaceGene(int index, Gene gene)
        {
            this.genes[index] = gene;
            Fitness = null;
        }


        /// <summary>
        /// Replaces the genes starting in the specified index.
        /// </summary>
        /// <param name="startIndex">Start index.</param>
        /// <param name="genes">The genes.</param>
        /// <remarks>
        /// The genes to be replaced can't be greater than the available space between the start index and the end of the chromosome.
        /// </remarks>
        public void ReplaceGenes(int startIndex, Gene[] genes)
        {
            if (genes.Length > 0)
            {
                Array.Copy(genes, 0, this.genes, startIndex, genes.Length);

                Fitness = null;
            }
        }

        /// <summary>
        /// Created all the genes of an individual.
        /// New genes according to the individual.
        /// </summary>
        protected virtual void CreateGenes()
        {
            for (int i = 0; i < Length; i++)
            {
                ReplaceGene(i, GenerateGene());
            }
        }

        /// <summary>
        /// Changes the size of the individual.
        /// </summary>
        /// <param name="newLength">NEw individual lenght.</param>
        public void Resize(int newLength)
        {
            Array.Resize(ref this.genes, newLength);
            this.length = newLength;
        }

        /// <summary>
        /// Add a new gene to the gene pool.
        /// Resezi individual.
        /// </summary>
        /// <param name="gene"></param>
        public virtual void AddGene(Gene gene)
        {
            Resize(Length + 1 );
            ReplaceGene(Length -1, gene);
        }


        object ICloneable.Clone()
        {
            var clone = CreateNew();
            clone.ReplaceGenes(0, GetGenes());
            clone.Fitness = Fitness;

            return clone;
        }
    }
}
