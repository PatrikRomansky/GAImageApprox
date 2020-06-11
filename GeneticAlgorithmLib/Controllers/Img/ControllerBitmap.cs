using GeneticAlgorithm.Elitizmus;
using GeneticAlgorithm.Operators.Mutations;
using GeneticAlgorithm.Operators.Xover;
using GeneticAlgorithm.Selections;
using GeneticAlgorithm.Terminations;
using System;
using System.Collections.Generic;

namespace GeneticAlgorithmLib.Controllers.Img
{
    /// <summary>
    /// Controller for individual, which genes represent Bitmap image (RGB).    
    /// All usable operators.
    /// </summary>
    class ControllerBitmap : BaseControllerBitmap
    {
        /// <summary>
        /// Initializes possible crossoversfor this instance.
        /// </summary
        public override void InitializeCrossovers()
        {
            this.xovers = new Dictionary<string, Type>
            {             
                { typeof(XoverUniform).Name, typeof(XoverUniform)},
                { typeof(XoverOnePoint).Name, typeof(XoverOnePoint)},
                { typeof(XoverTwoPoints).Name, typeof(XoverTwoPoints)},
                { typeof(XoverNon).Name, typeof(XoverNon)},
            };
        }

        /// <summary>
        /// Initializes possible elitizmuses for this instance.
        /// </summary>
        public override void InitializeElitizmuses()
        {
            this.elitizmuses = new Dictionary<string, Type>
            {
                { typeof(ElitizmusFitness).Name, typeof(ElitizmusFitness)},
                { typeof(ElitizmusNon).Name, typeof(ElitizmusNon)},
            };
        }

        /// <summary>
        /// Initializes possible mutations for this instance.
        /// </summary>
        public override void InitializeMutations()
        {
            this.mutations = new Dictionary<string, Type>
            {
                { typeof(MutationTwors).Name, typeof(MutationTwors)},
                { typeof(MutationUniform).Name, typeof(MutationUniform)}
            };
        }

        /// <summary>
        /// Initializes possible selections for this instance.
        /// </summary>
        public override void InitializeSelections()
        {
            this.selections = new Dictionary<string, Type>
            {
                { typeof(SelectionElite).Name, typeof(SelectionElite)},
                { typeof(SelectionTournament).Name, typeof(SelectionTournament)},
                { typeof(SelectionNon).Name, typeof(SelectionNon)},
            };
        }

        /// <summary>
        /// Initializes possible terminations for this instance.
        /// </summary>
        public override void InitializeTermination()
        {
            this.terminations = new Dictionary<string, Type>
            {
                { typeof(TerminationMaxTime).Name, typeof(TerminationMaxTime)},
                { typeof(TerminationMaxNumberGeneration).Name, typeof(TerminationMaxNumberGeneration)}
            };
        }
    }
}
