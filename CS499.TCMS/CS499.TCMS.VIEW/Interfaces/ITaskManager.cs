using CS499.TCMS.View.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CS499.TCMS.View.Interfaces
{
    /// <summary>
    /// This interface will expose methods to add and remove tasks to/from a list
    /// </summary>
    public interface ITaskManager
    {

        #region Methods

        /// <summary>
        /// Get a count of the current running tasks
        /// </summary>
        /// <returns>number of tasks</returns>
        int TaskCount();

        /// <summary>
        /// Add task to the list
        /// </summary>
        /// <param name="t">Running task</param>
        /// <param name="status">Task status</param>
        /// <returns>Running task</returns>
        Task AddTask(Task t, string status);

        /// <summary>
        /// Add task to the list
        /// </summary>
        /// <param name="t">Running task</param>
        /// <param name="status">Task status</param>
        /// <param name="continuation">Action to perform after</param>
        /// <param name="nextStatus">next status</param>
        /// <param name="scheduler">TaskScheduler</param>
        /// <param name="actionType">Action type being performed</param>
        /// <param name="databaseError">Database error text</param>
        /// <param name="log">Logger file the calling class</param>
        /// <returns>Running task</returns>
        Task AddTask(Task t, string status, Action continuation, string nextStatus,
            TaskScheduler scheduler, string actionType, string databaseError, log4net.ILog log);

        /// <summary>
        /// Add task to the list
        /// </summary>
        /// <param name="t">Running task</param>
        /// <param name="status">Task status</param>
        /// <param name="continuation">Action to perform after</param>
        /// <param name="nextStatus">next status</param>
        /// <param name="scheduler">TaskScheduler</param>
        /// <param name="actionType">Action type being performed</param>
        /// <param name="databaseError">Database error text</param>
        /// <param name="log">Logger file the calling class</param>
        /// <param name="continuationAlways">Action to perform after even if the task is faulted</param>
        /// <returns>Running task</returns>
        Task AddTask(Task t, string status, Action continuation, string nextStatus,
            TaskScheduler scheduler, string actionType, string databaseError, log4net.ILog log,
            Action continuationAlways);

        /// <summary>
        /// Remove task from the list
        /// </summary>
        /// <param name="t">Completed task</param>
        /// <param name="status">Task status</param>
        void RemoveTask(Task t, string status);

        /// <summary>
        /// Check to see if the task is still running
        /// </summary>
        /// <param name="id">unique identifier for the task</param>
        /// <returns>true if the task is still running, false otherwise</returns>
        bool TaskRunning(int id);

        /// <summary>
        /// Wait for tasks to complete
        /// </summary>
        /// <param name="tasks">list of tasks</param>
        /// <returns>awaitable task</returns>
        Task WaitForTasks(params Task[] tasks);

        #endregion

        #region Events

        /// <summary>
        /// Event raise when a task gets added to or removed from the list
        /// </summary>        
        event EventHandler<TaskManagerStatusEventArgs> OnTaskStatusChanged;

        #endregion

        #region Properties

        /// <summary>
        /// Task log
        /// </summary>
        ObservableCollection<Tuple<string, DateTime>> TaskLog { get; set; }

        #endregion

    }
}
