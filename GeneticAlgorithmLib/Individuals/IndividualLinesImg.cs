using GeneticAlgorithm.Genes;
using GeneticAlgorithm.Randomization;
using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
using GeneticAlgorithmLib.Individuals;

namespace GeneticAlgorithm.Individuals
{
    /// <summary>
    /// This class represents the individual (image).
    /// The image passes edge detection (canny detection) and then lienes detection (HoughLinesP).
    /// Individual genes represent line image (sketch).
    /// </summary>
    class IndividualShapeLine : IndividualImage
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; protected set; }

        public IndividualShapeLine(int width, int height, int numberOfline ,bool init = true) : base(numberOfline)
        {
            Width = width;
            Height = height;

            if (init)
            {
                Initialize();
            }
        }

        public override Gene GenerateGene()
        {
            var sX = RandomizationRnd.GetInt(0, Width);
            var sY = RandomizationRnd.GetInt(0, Height);

            var shifts = RandomizationRnd.GetInts(2, 0, 30);

            //  return new Gene(new ShapeLine(sX, sY, sX + shifts[0], sY + shifts[1]));
            return new Gene(new LineSegment2D(new Point(sX, sY), new Point(sX + shifts[0], sY + shifts[1])));
        }

        public override IIndividual CreateNew()
        {
            return new IndividualShapeLine(Width, Height, Length, false);
        }

        /// <summary>
        /// Builds the bitmap from genes.
        /// </summary>
        /// <returns>The bitmap.</returns>
        public override Bitmap BuildBitmap()
        {
            Image<Bgr, Byte> img = new Image<Bgr, Byte>(Width, Height, new Bgr(255,255,255));
            foreach (var g in GetGenes())
            {
               var line = (LineSegment2D)g.Value;
                CvInvoke.Line(img, line.P1, line.P2, new MCvScalar(0, 0, 0));
            }

            return img.ToBitmap<Bgr, Byte>();   
        }
    }
}
