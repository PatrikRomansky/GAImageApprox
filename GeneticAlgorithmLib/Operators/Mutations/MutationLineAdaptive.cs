using Emgu.CV.Structure;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Randomization;
using GeneticAlgorithm.Genes;
using System.Drawing;

namespace GeneticAlgorithm.Operators.Mutations
{
    /// <summary>
    /// Special adaptive mutation for an individual represented by lines.
    /// </summary>
    class MutationLineAdaptive : MutationLine
    {
        int shift;

        /// <summary>
        /// Construcotr: Adaptive line mutation operator.
        /// </summary>
        public MutationLineAdaptive()
        {
            this.shift = 15;
        }

        /// <summary>
        /// Adaptation of mutation properties.
        /// </summary>
        public override void Adaptive()
        {
            this.shift -= 1;

            if (this.shift < 1)
                this.shift = 1;
        }
    }
}
