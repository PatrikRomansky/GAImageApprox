using GeneticAlgorithm.Algorithms;
using GeneticAlgorithm.Controllers;
using GeneticAlgorithm.Populations;
using GeneticAlgorithmLib.Controllers.Img;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GAImageApproxUX
{
    public partial class GaUx : Form
    {
        // target img/fileName
        private string imgTargetFileName;

        // second Ga-thread
        private Thread threadGA;

        // genetic algorithm
        private GA ga;

        Dictionary<string, Type> controllers = new Dictionary<string, Type>();

        /// <summary>
        /// Initialization of basic values of the user interface.
        /// </summary>
        public GaUx()
        {
            InitializeComponent();

            controllers = ThreadSafeServiceUX.selectType<IController>();
            comboBoxControllers.Items.AddRange(controllers.Keys.ToArray());
        }

        /// <summary>
        /// User selected img.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            //For any other formats
            of.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (of.ShowDialog() == DialogResult.OK)
            {
                imgTargetFileName = of.FileName;
                pictureBoxTarget.Image = Bitmap.FromFile(of.FileName) as Bitmap;
            }
        }

        /// <summary>
        /// Star{Stop genetic algorithm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartStop_Click(object sender, EventArgs e)
        {
            if (buttonStartStop.Text == "START")
            {

                if (configGa())
                {
                    buttonStartStop.Text = "STOP";
                    threadGA = new Thread(ga.Run);
                    threadGA.Start();
                }
            }
            else
            {
                buttonStartStop.Text = "START";
                ga.Stop();
                threadGA.Abort();
            }
        }

        /// <summary>
        /// Kill all trheads.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GaUx_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (threadGA != null)
            {
                ga.Stop();
                threadGA.Abort();
            }
        }

        /// <summary>
        /// Genetic algorithm config.
        /// </summary>
        /// <returns>If threu = successful configuration.</returns>
        private bool configGa()
        {
            // opperators names
            string target,
                    xoverName,
                    elitizmusName,
                    mutationName,
                    selectionName,
                    terminationName;

            // operator parameters
            float percentageElit, mutationPorb, xoverProb, scale;
            int popSize, paramTerm;

            // problems contoller
            IControllerImage sampleController;

            // try sets operators and parameters
            try
            {
                // curr operators names
                xoverName = comboBoxXover.SelectedItem.ToString();
                elitizmusName = comboBoxElitizmus.SelectedItem.ToString();
                mutationName = comboBoxMutation.SelectedItem.ToString();
                selectionName = comboBoxSelection.SelectedItem.ToString();
                terminationName = comboBoxTermination.SelectedItem.ToString();


                mutationPorb = (float) numericUpDownMutation.Value / 100;
                xoverProb = (float) numericUpDownXover.Value / 100;

                // Ga population size
                popSize = (int)numericUpDownPopSize.Value;

                paramTerm = (int) numericUpDownTermination.Value;
                percentageElit = (float)numericUpDownElitizmus.Value / 100;

                scale = (float)numericUpDownScale.Value;

                var controllerName = controllers[comboBoxControllers.SelectedItem.ToString()];

                sampleController = Activator.CreateInstance(controllerName) as IControllerImage;

                target = imgTargetFileName.Replace("\\", "/");
                sampleController.Initialize();
                sampleController.InitializeScale(scale);
                sampleController.Initialize(target);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            
            // Creates operators
            var fitness = sampleController.CreateFitness();
            var executor = sampleController.CreateExecutor();
            var crossover = sampleController.CreateCrossover(xoverName);
            var mutation = sampleController.CreateMutation(mutationName);
            var selection = sampleController.CreateSelection(selectionName);

            var elitizmus = sampleController.CreateElitizmus(elitizmusName, percentageElit);
            var termination = sampleController.CreateTermination(terminationName, paramTerm);

            // creates population with curr. popSize
            var population = new Population(popSize, sampleController.CreateIndividual);

            // new genetic algorithm
            ga = new GA(population, fitness, selection, crossover, mutation, elitizmus, termination, executor);
            ga.XoverProbability = xoverProb;
            ga.MutationProbability = mutationPorb;

            // information transfer between ga/trhead and US
            ga.GenerationInfo += delegate
            {
                var bestIndividual = ga.Population.BestIndividual;

                ThreadSafeServiceUX.SetControlPropertyThreadSafe(labelTime, "Text", "Time: " + ga.TimeEvolving);
                ThreadSafeServiceUX.SetControlPropertyThreadSafe(labelFitness, "Text", "Fitness: " + bestIndividual.Fitness);
                ThreadSafeServiceUX.SetControlPropertyThreadSafe(labelGeneration, "Text", "Generations: " + ga.Population.CurrentGenerationNumber);

                var speed = ga.TimeEvolving.TotalSeconds / ga.Population.CurrentGenerationNumber;
                ThreadSafeServiceUX.SetControlPropertyThreadSafe(labelSpeed, "Text", "Speed(gen/sec): " + speed.ToString("0.0000"));

                var best = sampleController.ShowBestIndividual(bestIndividual);

                if (best != null)
                {
                    ThreadSafeServiceUX.SetControlPropertyThreadSafe(pictureBoxBestInd, "Image", Bitmap.FromFile(best.ToString()));
                }
            };

            // configuration termination
            sampleController.ConfigGATermination(ga);

            return true;
        }

        /// <summary>
        /// Refresh UX.
        /// </summary>
        private void ClearComboBoxes()
        {
            comboBoxXover.Items.Clear();
            comboBoxElitizmus.Items.Clear();
            comboBoxMutation.Items.Clear();
            comboBoxSelection.Items.Clear();
            comboBoxTermination.Items.Clear();
        }

        /// <summary>
        /// Selected init. settings.
        /// </summary>
        private void SetFirstItemCombobexes()
        {
            comboBoxXover.SelectedIndex = 0;
            comboBoxElitizmus.SelectedIndex = 0;
            comboBoxMutation.SelectedIndex = 0;
            comboBoxSelection.SelectedIndex = 0;
            comboBoxTermination.SelectedIndex = 0;
            comboBoxTermination.SelectedIndex = 0;
        }

        /// <summary>
        /// Sets parameters for the selected controller.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxControllers_SelectedIndexChanged(object sender, EventArgs e)
        {          
            var controllerName = controllers[comboBoxControllers.SelectedItem.ToString()];

            var guiSampleController = Activator.CreateInstance(controllerName) as IControllerImage;
            guiSampleController.Initialize();

            ClearComboBoxes();

            comboBoxXover.Items.AddRange(guiSampleController.PossibleCrossovers());
            comboBoxElitizmus.Items.AddRange(guiSampleController.PossibleElitizmuses());
            comboBoxMutation.Items.AddRange(guiSampleController.PossibleMutations());
            comboBoxSelection.Items.AddRange(guiSampleController.PossibleSelections());
            comboBoxTermination.Items.AddRange(guiSampleController.PossibleTerminations());

            SetFirstItemCombobexes();
        }
    }
}
