using Emgu.CV.Structure;
using GeneticAlgorithm.Individuals;
using System;
using System.Collections.Generic;
using Emgu.CV;
using GeneticAlgorithm.Genes;

namespace GeneticAlgorithm.Fitnesses
{
    /// <summary>
    /// Fitness for individual, which genes represent line image (sketch).
    /// </summary>
    class FitnessLine : IFitness
    {
        protected IList<LineSegment2D> targetBitmapLine;
        public int targetBitmapLineCount;

        public Gene[] TargetGenes => throw new NotImplementedException();

        public int TargetSize => throw new NotImplementedException();

        public void Initialize(object target)
        {
            var imageIn = target as Image<Gray, byte>;
            Mat edges = new Mat();
            // Edges detection
            CvInvoke.Canny(imageIn, edges, 95, 100);
            //HoughLinesP
            targetBitmapLine = CvInvoke.HoughLinesP(edges, 1, Math.PI / 180, 5, 2, 10);
            targetBitmapLineCount = targetBitmapLine.Count;
        }


        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="target">The target bitmap.</param>
        public FitnessLine(object target)
        {
            Initialize(target);
        }

        /// <summary>
        /// Two lines distance.
        /// Dinstence between end-points.
        /// </summary>
        /// <param name="l1">First line.</param>
        /// <param name="l2">Second line</param>
        /// <returns>The distance.</returns>
        private double LinesDifference(LineSegment2D l1, LineSegment2D l2)
        {
            var distP1P1 = Math.Abs(l1.P1.X - l2.P1.X) + Math.Abs(l1.P1.Y - l2.P1.Y);
            var distP2P2 = Math.Abs(l1.P2.X - l2.P2.X) + Math.Abs(l1.P2.Y - l2.P2.Y);

            var distP1P2 = Math.Abs(l1.P1.X - l2.P2.X) + Math.Abs(l1.P1.Y - l2.P2.Y);
            var distP2P1 = Math.Abs(l1.P2.X - l2.P1.X) + Math.Abs(l1.P2.Y - l2.P1.Y);


            int dist1 = distP1P1 + distP2P2;
            int dist2 = distP1P2 + distP2P1;

            // nearest distance
            if (dist1 < dist2)
            {
                return dist1;
            }

            return dist2;
        }

        /// <summary>
        /// Performs the evaluation against the specified individual.
        /// </summary>
        /// <param name="individual">The individual to be evaluated.</param>
        /// <returns>The fitness of the individual.</returns>
        public double Evaluate(IIndividual individual)
        {
            double fitness = 0.0;
            for (var i = 0; i < individual.Length; i++)
            {
                fitness += LinesDifference((LineSegment2D)individual.GetGene(i).Value, targetBitmapLine[i]);
            }

            return 1 / (fitness + 1);
        }
    }
}
