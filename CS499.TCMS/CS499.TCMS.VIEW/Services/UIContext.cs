using System.Threading;
using System.Threading.Tasks;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class will hold reference to the UI context
    /// </summary>
    public static class UIContext
    {

        #region Methods

        /// <summary>
        /// Initialize ui context
        /// </summary>
        public static void InitializeContext()
        {

            if (_current != null)
            {
                return;
            }

            if (SynchronizationContext.Current == null)
            {
                SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
            }

            Current = TaskScheduler.FromCurrentSynchronizationContext();

        }

        #endregion

        #region Properties

        private static TaskScheduler _current;

        /// <summary>
        /// Hold reference to the UI task scheduler
        /// </summary>
        public static TaskScheduler Current
        {
            get
            {

                if (_current == null)
                {
                    InitializeContext();
                }

                return _current;

            }
            set
            {
                _current = value;
            }

        }

        #endregion

    }
}
