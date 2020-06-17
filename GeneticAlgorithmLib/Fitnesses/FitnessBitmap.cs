using GeneticAlgorithm.Genes;
using GeneticAlgorithm.Individuals;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GeneticAlgorithm.Fitnesses
{
    /// <summary>
    /// Fitness for individual, which genes represent Bitmap image (RGB).
    /// </summary>
    public class FitnessBitmap : IFitness
    {
        protected IList<Color> targetBitmapPixels;
        protected int targetBitmapPixelsCount;

        public Gene[] TargetGenes => throw new NotImplementedException();

        public int TargetSize => throw new NotImplementedException();

        /// <summary>
        /// Transform bitmap to genes.
        /// </summary>
        /// <param name="target">Bitmap.</param>
        public void Initialize(object target)
        {
            var bitmap = target as Bitmap;

            targetBitmapPixels = new List<Color>();
            var width = bitmap.Width;
            var height = bitmap.Height;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    targetBitmapPixels.Add(bitmap.GetPixel(x, y));
                }
            }

            targetBitmapPixelsCount = targetBitmapPixels.Count;
        }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="target">The target bitmap.</param>
        public FitnessBitmap(object target)
        {
            Initialize(target);
        }

        private double PixelDifference(Color px1, Color px2)
        {
            // return (px1.R - px2.R) * (px1.R - px2.R) + (px1.B - px2.B) * (px1.B - px2.B) + (px1.G - px2.G) * (px1.G - px2.G);
            return Math.Abs(px1.R - px2.R ) + Math.Abs(px1.B - px2.B) + Math.Abs(px1.G - px2.G);
        }

        /// <summary>
        /// Performs the evaluation against the specified individual.
        /// </summary>
        /// <param name="individual">The individual to be evaluated.</param>
        /// <returns>The fitness of the individual.</returns>
        public double Evaluate(IIndividual individual)
        {
            double fitness = 0;

            var genes = individual.GetGenes();
         
            for (int i = 0; i < this.targetBitmapPixelsCount; i++)
            {     
                fitness += PixelDifference(this.targetBitmapPixels[i], (Color)genes[i].Value);
            }
            return 1 / (1 + fitness);        
        }

    }
}
