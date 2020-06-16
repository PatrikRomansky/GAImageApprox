using GeneticAlgorithm.Algorithms;
using GeneticAlgorithm.Elitizmus;
using GeneticAlgorithm.Executor;
using GeneticAlgorithm.Fitnesses;
using GeneticAlgorithm.Individuals;
using GeneticAlgorithm.Mutations;
using GeneticAlgorithm.Selections;
using GeneticAlgorithm.Terminations;
using GeneticAlgorithm.Xover;
using GeneticAlgorithmLib.Individuals;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace GeneticAlgorithmLib.Controllers.Img
{
    /// <summary>
    /// Base abstract class for image problem controller.
    /// </summary>
    public abstract class ControllerImage : IControllerImage
    {
        // img dize
        protected int width, height, rawWidth, rawHeight;

        // result folder
        protected string m_destFolder;

        
        protected float scale;

        // operators
        protected Dictionary<string, Type> xovers;
        protected Dictionary<string, Type> mutations;
        protected Dictionary<string, Type> selections;
        protected Dictionary<string, Type> terminations;
        protected Dictionary<string, Type> elitizmuses;

        protected IExecutor executor;
        protected IFitness fitness;

        protected GA GA { get; set; }

        /// <summary>
        /// Configure the Genetic Algorithm.
        /// </summary>
        /// <param name="ga">The genetic algorithm.</param>
        public virtual void ConfigGATermination(GA ga)
        {
            GA = ga;
            ga.TerminationReached += (sender, args) =>
            {
                using (var collection = new MagickImageCollection())
                {
                    var files = Directory.GetFiles(m_destFolder, "*.png");

                    foreach (var image in files)
                    {
                        collection.Add(image);
                        collection[0].AnimationDelay = 100;
                    }

                    var settings = new QuantizeSettings();
                    settings.Colors = 256;
                    collection.Quantize(settings);

                    collection.Optimize();
                    collection.Write(Path.Combine(m_destFolder, "result.gif"));
                }
            };
        }

        /// <summary>
        /// Creates the individual.
        /// </summary>
        /// <returns>The individual.</returns>
        public abstract IIndividual CreateIndividual();

        /// <summary>
        /// Initialize this instance operator components.
        /// </summary>
        public void Initialize()
        {
            InitializeCrossovers();
            InitializeElitizmuses();
            InitializeMutations();
            InitializeSelections();
            InitializeTermination();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public virtual void Initialize(object target) 
        {

            executor = new ExecutorParallel();

            var inputImageFile = (String)target;
            var folder = Path.Combine(Path.GetDirectoryName(inputImageFile), "results");
            var filePath = inputImageFile.Split('/');
            var fileName = filePath[filePath.Length - 1].Split('.')[0];
            m_destFolder = folder + "_" + fileName + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss");

            Directory.CreateDirectory(m_destFolder);
        }

        /// <summary>
        /// Initializes possible crossoversfor this instance.
        /// </summary>
        public abstract void InitializeCrossovers();

        /// <summary>
        /// Initializes possible elitizmuses for this instance.
        /// </summary>
        public abstract void InitializeElitizmuses();

        /// <summary>
        /// Initializes possible mutations for this instance.
        /// </summary>
        public abstract void InitializeMutations();
        
        /// <summary>
        /// Initializes possible selections for this instance.
        /// </summary>
        public abstract void InitializeSelections();

        /// <summary>
        /// Initializes possible terminations for this instance.
        /// </summary>
        public abstract void InitializeTermination();

        /// <summary>
        /// Creates the crossover.
        /// </summary>
        /// <param name="name">Crossover name.</param>
        /// <returns>The croosover</returns>
        public IXover CreateCrossover(string name)
        {
            Type t = xovers[name];
            var a = Activator.CreateInstance(t);
            return Activator.CreateInstance(t) as IXover;
        }

        /// <summary>
        /// Creates the elitizmus.
        /// </summary>
        /// <param name="name">Elitizmus name.</param>
        /// <returns>The elitizmus.</returns>
        public IElitizmus CreateElitizmus(string name, double perc)
        {
            return Activator.CreateInstance(elitizmuses[name], perc) as IElitizmus;
        }

        /// <summary>
        ///  Create the executor.
        /// </summary>
        /// <returns>The executor.</returns>
        public IExecutor CreateExecutor()
        {
            return executor;
        }

        /// <summary>
        /// Creates the fitness.
        /// </summary>
        /// <returns>The fitness.</returns>
        public IFitness CreateFitness()
        {
            return fitness;
        }

        /// <summary>
        /// Creates the mutation.
        /// </summary>
        /// <param name="name">Mutation name.</param>
        /// <returns>The mutation.</returns>
        public IMutation CreateMutation(string name)
        {
            return Activator.CreateInstance(mutations[name]) as IMutation;
        }

        /// <summary>
        /// Creates the selection.
        /// </summary>
        /// <param name="name">Selection name.</param>
        /// <returns>The selection.</returns>
        public ISelection CreateSelection(string name)
        {
            return Activator.CreateInstance(selections[name]) as ISelection;
        }

        /// <summary>
        /// Creates the termination.
        /// </summary>
        /// <param name="name">Termination name</param>
        /// <param name="param"></param>
        /// <returns>The termination</returns>
        public ITermination CreateTermination(string name, int param)
        {
            return Activator.CreateInstance(terminations[name], param) as ITermination;
        }

        /// <summary>
        /// Gets possible crossover for this instance.
        /// </summary>
        /// <returns>Possible crossovers.</returns>
        public string[] PossibleCrossovers()
        {
            return xovers.Keys.ToArray();
        }

        /// <summary>
        /// Gets possible elitizmuses for this instance.
        /// </summary>
        /// <returns>Possible elitizmuses.</returns>
        public string[] PossibleElitizmuses()
        {
            return elitizmuses.Keys.ToArray();
        }

        /// <summary>
        /// Gets possible mutations for this instance.
        /// </summary>
        /// <returns>Possible mutations.</returns>
        public string[] PossibleMutations()
        {
            return mutations.Keys.ToArray();
        }

        /// <summary>
        /// Gets possible selections for this instance.
        /// </summary>
        /// <returns>Possible selections.</returns>
        public string[] PossibleSelections()
        {
            return selections.Keys.ToArray();
        }

        /// <summary>
        /// Initializes possible terminations for this instance.
        /// </summary>
        public string[] PossibleTerminations()
        {
            return terminations.Keys.ToArray();
        }

        /// <summary>
        /// Resize image.
        /// </summary>
        /// <param name="scale">Scale percentage.</param>
        public void InitializeScale(float scale)
        {
            this.scale = scale;
        }

        /// <summary>
        /// Draws the sample.
        /// </summary>
        /// <param name="bestIndividual">The current best individual.</param>
        public virtual object ShowBestIndividual(IIndividual bestIndividual)
        {
            if (GA.GenerationsNumber % 500 == 0 | GA.GenerationsNumber == 1)
            {
                var best = bestIndividual as IndividualImage;

                using (var bitmap = best.BuildBitmap())
                {

                    var img = ScaleImage(bitmap, rawWidth, rawHeight);

                    var fileName = m_destFolder + "/" + GA.GenerationsNumber.ToString("D10") + "_" + best.Fitness + ".png";
                 
                    img.Save(fileName);
                    return fileName;
                }
            }
            return null;
        }

        /// <summary>
        /// Resize image.
        /// </summary>
        /// <param name="image">Input image.</param>
        /// <param name="nnewWidth">New width.</param>
        /// <param name="newHeight">New height.</param>
        /// <returns>Scale image.</returns>
        public virtual Bitmap ScaleImage(Image image, int nnewWidth, int newHeight)
        {
            var destRect = new Rectangle(0, 0, nnewWidth, newHeight);
            var destImage = new Bitmap(nnewWidth, newHeight);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                // best quality of resize imgs
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage as Bitmap;
        }
    }
}
