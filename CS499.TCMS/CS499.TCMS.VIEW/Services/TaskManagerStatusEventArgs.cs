using System;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class will contain the status of task
    /// </summary>
    public class TaskManagerStatusEventArgs : EventArgs
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="status">status of the task</param>
        public TaskManagerStatusEventArgs(string status)
        {
            this.Status = status;
        }

        #endregion

        #region Properties

        public string Status { get; set; }

        #endregion

    }
}
