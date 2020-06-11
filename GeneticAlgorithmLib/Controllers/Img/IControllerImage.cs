using GeneticAlgorithm.Controllers;

namespace GeneticAlgorithmLib.Controllers.Img
{
    /// <summary>
    /// Interface for setting the properties of solved image problems.
    /// </summary>
    public interface IControllerImage:IController
    {
        /// <summary>
        /// Resize image.
        /// </summary>
        /// <param name="scale">Scale percentage.</param>
        void InitializeScale(float scale);
    }
}
