using Emgu.CV;
using Emgu.CV.Structure;
using GeneticAlgorithm.Elitizmus;
using GeneticAlgorithm.Fitnesses;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Operators.Mutations;
using GeneticAlgorithm.Operators.Xover;
using GeneticAlgorithm.Selections;
using GeneticAlgorithm.Terminations;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace GeneticAlgorithmLib.Controllers.Img
{
    /// <summary>
    /// Controller for individual, which genes represent line image (sketch). 
    /// Basic operators selected on the basis of previous .
    /// </summary>
    public class BaseControllerLinesImg : ControllerImage
    {
        // number of lines
        private int indSize;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize(object target)
        {
            var targetBitmap = this.InititializeSame(target);

            var fit = new FitnessLine(targetBitmap.ToImage<Gray,byte>());
            indSize = fit.targetBitmapLineCount;
            fitness = fit;

            base.Initialize(target);
        }

        /// <summary>
        /// Creates the individual.
        /// </summary>
        /// <returns>The individual.</returns>
        public override IIndividual CreateIndividual()
        {
            return new IndividualShapeLine(width, height, indSize);
        }

        /// <summary>
        /// Initializes possible crossoversfor this instance.
        /// </summary
        public override void InitializeCrossovers()
        {
            this.xovers = new Dictionary<string, Type>
            {
               {typeof(XoverUniform).Name, typeof(XoverUniform)}
            };
        }

        /// <summary>
        /// Initializes possible elitizmuses for this instance.
        /// </summary>
        public override void InitializeElitizmuses()
        {
            this.elitizmuses = new Dictionary<string, Type>
            {
                { typeof(ElitizmusFitness).Name, typeof(ElitizmusFitness)}
            };
        }

        /// <summary>
        /// Initializes possible mutations for this instance.
        /// </summary>
        public override void InitializeMutations()
        {
            this.mutations = new Dictionary<string, Type>
            {
                { typeof(MutationLineAdaptive).Name, typeof(MutationLineAdaptive)}
            };
        }

        /// <summary>
        /// Initializes possible selections for this instance.
        /// </summary>
        public override void InitializeSelections()
        {
            this.selections = new Dictionary<string, Type>
            {
                {typeof(SelectionTournament).Name, typeof(SelectionTournament)}
            };
        }

        /// <summary>
        /// Initializes possible terminations for this instance.
        /// </summary>
        public override void InitializeTermination()
        {
            this.terminations = new Dictionary<string, Type>
            {
                {typeof(TerminationMaxNumberGeneration).Name, typeof(TerminationMaxNumberGeneration)}
            };
        }
    }
}
