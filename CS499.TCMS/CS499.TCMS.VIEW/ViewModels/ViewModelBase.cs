using CS499.TCMS.DataAccess;
using CS499.TCMS.View.Interfaces;
using CS499.TCMS.View.Services;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Threading;

namespace CS499.TCMS.ViewModels
{

    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {

        #region Constructor

        protected ViewModelBase()
        {
            this.ContentId = "Blank";
            this.Factory = new RepositoryFactory(CoreAssembly.CurrentUser(), CoreAssembly.GetDatabaseName());
        }

        #endregion // Constructor

        #region Properties

        /// <summary>
        /// Returns the viewModel-friendly name of this object.
        /// Child classes can set this property to a new value,
        /// or override it to determine the value on-demand.
        /// </summary>
        public virtual string DisplayName { get; protected set; }

        /// <summary>
        /// Returns a visibility value for the object
        /// </summary>
        public virtual bool IsVisible { get; set; }

        /// <summary>
        /// Returns a selected value for the object
        /// </summary>
        public virtual bool IsSelected { get; set; }

        /// <summary>
        /// Returns a tooltip with more details than the
        /// DisplayName
        /// </summary>
        public virtual string DisplayToolTip { get; set; }

        /// <summary>
        /// Returns a new value for the object
        /// </summary>
        public virtual bool IsNew { get; set; }

        /// <summary>
        /// Return unique id for the object
        /// </summary>
        public virtual string ContentId { get; set; }

        /// <summary>
        /// Return a changed value for the object
        /// </summary>
        public virtual bool HasChanges { get; set; }

        /// <summary>
        /// Task Manager to execute task
        /// </summary>
        public ITaskManager TaskManager { get; set; }

        /// <summary>
        /// Repository factory to create repositories
        /// </summary>
        public RepositoryFactory Factory { get; set; }

        /// <summary>
        /// Default instance of the MVVMLight messager class
        /// </summary>
        public IMessenger MessengerInstance
        {
            get
            {
                return Messenger.Default;
            }
        }

        /// <summary>
        /// Dispatcher Timer that can be used for timed functions
        /// </summary>
        protected DispatcherTimer timer;

        #endregion

        #region Debugging Aides

        /// <summary>
        /// Warns the developer if this object does not have
        /// a public property with the specified name. This 
        /// method does not exist in a Release build.
        /// </summary>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        /// <summary>
        /// Returns whether an exception is thrown, or if a Debug.Fail() is used
        /// when an invalid property name is passed to the VerifyPropertyName method.
        /// The default value is false, but subclasses used by unit tests might 
        /// override this property's getter to return true.
        /// </summary>
        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        #endregion // Debugging Aides

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Raised when a property on this object has a new value.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The property that has a new value.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            this.VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        #endregion // INotifyPropertyChanged Members

        #region IDisposable Members

        /// <summary>
        /// Invoked when this object is being removed from the application
        /// and will be subject to garbage collection.
        /// </summary>
        public void Dispose()
        {
            this.OnDispose();
        }

        /// <summary>
        /// Child classes can override this method to perform 
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected virtual void OnDispose()
        {

        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName, this.GetHashCode());
            System.Diagnostics.Debug.WriteLine(msg);
        }
#endif

        #endregion // IDisposable Members

    }
}