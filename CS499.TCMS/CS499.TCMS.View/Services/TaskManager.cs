using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Resources;
using CS499.TCMS.View.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ToolKit.Data;

namespace CS499.TCMS.View.Services
{
    /// <summary>
    /// This class will hold a list of running task that the main ViewModel can
    /// use to ensure the user does not close the application before the background process has completed
    /// </summary>
    public class TaskManager : ITaskManager
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public TaskManager()
        {
            tasks = new List<Task>();
            this.TaskLog = new ObservableCollection<Tuple<string, DateTime>>();
        }

        /// <summary>
        /// Add dialog service
        /// </summary>
        /// <param name="dialog">dialog service to show messages from viewmodel</param>
        public TaskManager(IDialogService dialog)
            : this()
        {
            _dialog = dialog;
        }

        #endregion

        #region Events

        /// <summary>
        /// Event raise when a task gets added to or removed from the list
        /// </summary>        
        public event EventHandler<TaskManagerStatusEventArgs> OnTaskStatusChanged;

        #endregion

        #region Methods

        /// <summary>
        /// Get a count of the current running tasks
        /// </summary>
        /// <returns>number of tasks</returns>
        public int TaskCount()
        {
            return tasks.Count();
        }

        /// <summary>
        /// Add task to the list and change status
        /// </summary>
        /// <param name="t">Running task</param>
        /// <param name="status">status</param>
        private void Add(Task t, string status)
        {
            tasks.Add(t);
            this.Log(t, status, "Add");
            this.TaskStatuChanged(status);
        }

        /// <summary>
        /// Log the task status
        /// </summary>
        /// <param name="t">task</param>
        /// <param name="status">task status</param>
        private void Log(Task t, string status, string action)
        {
            this.TaskLog.Add(new Tuple<string, DateTime>(string.Format("{0}: TaskId => {1} Status => {2}::{3}", action, t.Id, t.Status, status), DateTime.Now));
        }

        /// <summary>
        /// Add task to the list
        /// </summary>
        /// <param name="t">Running task</param>
        /// <param name="status">Task status</param>
        /// <returns>Running task</returns>
        public Task AddTask(Task t, string status)
        {
            this.Add(t, status);
            return t;
        }

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
        public Task AddTask(Task t, string status, Action continuation, string nextStatus,
            TaskScheduler scheduler, string actionType, string databaseError, log4net.ILog log)
        {
            this.Add(t, status);

            var continueTask = t.ContinueWith((task) =>
            {
                try
                {

                    // throw inner exception if faulted
                    if (task.IsFaulted)
                    {
                        throw task.Exception.InnerException;
                    }

                    // perform action
                    continuation();

                }
                catch (UnauthorizedAccessException uex)
                {

                    string msg = uex.Message;

                    // log error
                    log.Error(msg, uex);

                    // show viewModel sanitized message
                    _dialog.Dialog.ShowMessageAsync(_dialog.ViewModel, Messages.TitleApp, msg);

                }
                catch (DatabaseException dex)
                {

                    string msg = null;

                    if (dex.ErrorType == ErrorTypes.DbConnectionFailure)
                    {
                        msg = Messages.DatabaseConnectionError;
                    }
                    else
                    {
                        msg = databaseError;
                    }

                    // log error
                    log.Error(msg, dex);

                    // show viewModel sanitized message
                    _dialog.Dialog.ShowMessageAsync(_dialog.ViewModel, Messages.TitleApp, msg);

                }
                catch (Exception ex)
                {

                    // get new error id and message
                    string id = Guid.NewGuid().ToString();
                    string msg = string.Format(Messages.UnexpectedError, actionType, Environment.NewLine, id);

                    // log error
                    log.Error(msg, ex);

                    // show viewModel sanitized message
                    _dialog.Dialog.ShowMessageAsync(_dialog.ViewModel, Messages.TitleApp, msg);

                }

            }, scheduler);

            var removeTask = continueTask.ContinueWith((task) => this.RemoveTask(t, nextStatus), scheduler);

            return removeTask;

        }


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
        public Task AddTask(Task t, string status, Action continuation, string nextStatus,
            TaskScheduler scheduler, string actionType, string databaseError, log4net.ILog log,
            Action continuationAlways)
        {

            var continueAlwaysTask = this.AddTask(t, status, continuation, nextStatus, scheduler, actionType, databaseError, log)
                .ContinueWith((task) =>
                {
                    continuationAlways();

                }, System.Threading.CancellationToken.None, TaskContinuationOptions.AttachedToParent, scheduler);

            return continueAlwaysTask;
        }

        /// <summary>
        /// Remove task from the list
        /// </summary>
        /// <param name="t">Completed task</param>
        /// <param name="status">Task status</param>
        public void RemoveTask(Task t, string status)
        {
            this.Log(t, status, "Remove");
            tasks.Remove(t);
            this.TaskStatuChanged(status);
        }

        /// <summary>
        /// Check to see if the task is still running
        /// </summary>
        /// <param name="id">unique identifier for the task</param>
        /// <returns>true if the task is still running, false otherwise</returns>
        public bool TaskRunning(int id)
        {
            return this.tasks.FirstOrDefault((t) => t.Id == id) != null;
        }

        /// <summary>
        /// Wait for tasks to complete
        /// </summary>
        /// <param name="tasks">list of tasks</param>
        /// <returns>awaitable task</returns>
        public async Task WaitForTasks(params Task[] tasks)
        {

            // wait if any task is null
            while (tasks.FirstOrDefault((t) => t == null) == null)
            {
                await Task.Delay(500);
            }

            // wait for all tasks to complete
            while (tasks.FirstOrDefault((t) => t.IsCompleted == false) != null)
            {
                await Task.Delay(500);
            }

        }

        /// <summary>
        /// Raises the task status change event
        /// </summary>
        /// <param name="status">Task status</param>
        private void TaskStatuChanged(string status)
        {

            if (OnTaskStatusChanged != null)
            {
                this.OnTaskStatusChanged(this, new TaskManagerStatusEventArgs(status));
            }

        }

        #endregion

        #region Properties

        /// <summary>
        /// List of tasks
        /// </summary>
        private List<Task> tasks;

        private ObservableCollection<Tuple<string, DateTime>> _taskLog;

        /// <summary>
        /// Log of all executed task
        /// </summary>
        public ObservableCollection<Tuple<string, DateTime>> TaskLog
        {
            get { return _taskLog; }
            set { _taskLog = value; }
        }

        /// <summary>
        /// Dialog service
        /// </summary>
        private IDialogService _dialog;

        #endregion

    }

}
