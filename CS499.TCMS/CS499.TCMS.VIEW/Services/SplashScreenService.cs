using System;
using System.Threading;

namespace CS499.TCMS.View.Views
{
    /// <summary>
    /// This class will handle displaying and closing the splash screen
    /// </summary>
    public class SplashScreenService
    {

        #region Methods

        /// <summary>
        /// Show the splash screen on another thread
        /// </summary>
        public static void ShowSplash()
        {

            watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            Thread t = new Thread(new ThreadStart(() =>
            {

                try
                {

                    splash = new SplashScreenView();
                    splash.ShowDialog();

                }
                catch (Exception ex)
                {

                    log.Error("Failed to show splash screen.", ex);
                }

            }));

            t.SetApartmentState(ApartmentState.STA);
            t.IsBackground = true;
            t.Start();

        }

        /// <summary>
        /// Set status on splash screen
        /// </summary>
        /// <param name="value">string for the status</param>
        public static void SetStatus(string value)
        {

            while (splash == null || watch.ElapsedMilliseconds >= 5000) ;

            if (splash != null)
            {
                splash.Dispatcher.BeginInvoke(new Action(() =>
                {
                    splash.Status.Text = value;
                    splash.InvalidateVisual();
                })
                    , null);
            }

        }

        /// <summary>
        /// Close splash screen
        /// </summary>
        public static void CloseSplash()
        {

            watch.Stop();

            if (splash != null)
            {

                if (watch.ElapsedMilliseconds <= 2000)
                {
                    Thread.Sleep(2000);
                }

                splash.Dispatcher.BeginInvoke(new Action(() =>
                {
                    splash.Close();
                })
                    , null);
            }

        }

        #endregion

        #region Properties

        /// <summary>
        /// Logging
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Splash screen window
        /// </summary>
        private static SplashScreenView splash;

        /// <summary>
        /// Stop watch
        /// </summary>
        private static System.Diagnostics.Stopwatch watch;

        #endregion

    }
}
