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

        /// <summary>
        /// Gets the width of the bitmap.
        /// </summary>
        /// <value>
        /// The width of the bitmap.
        /// </value>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets the height of the bitmap.
        /// </summary>
        /// <value>
        /// The height of the bitmap.
        /// </value>
        public int Height { get; protected set; }

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="target">The target bitmap.</param>
        public FitnessBitmap(Bitmap target)
        {
            this.Width = target.Width;
            this.Height = target.Height;

            this.targetBitmapPixels = IndividualBitmap.GetPixels(target);
            this.targetBitmapPixelsCount = targetBitmapPixels.Count;
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
         
            for (int i = 0; i < this.targetBitmapPixelsCount; i++)
            {     
                fitness -= PixelDifference(this.targetBitmapPixels[i], (Color)individual.GetGene(i).Value);
            }
            return fitness;        
        }
    }
}
