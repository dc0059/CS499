
namespace RepositoryPatternExamples
{
    /// <summary>
    /// This class will contain generic methods for the model classes
    /// </summary>
    public abstract class ModelBase
    {

        /// <summary>
        /// Shallow clone the object
        /// </summary>
        /// <returns>new instance of the cloned class</returns>
        public virtual T Clone<T>() where T : IModel
        {
            return (T)this.MemberwiseClone();
        }

    }

}
