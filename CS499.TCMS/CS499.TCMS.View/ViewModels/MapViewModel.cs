using CS499.TCMS.View.Resources;
using CS499.TCMS.ViewModels;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will generate the Google web address for manifest directions
    /// </summary>
    /// <seealso cref="CS499.TCMS.ViewModels.ViewModelBase" />
    public class MapViewModel : ViewModelBase
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MapViewModel"/> class.
        /// </summary>
        public MapViewModel()
        {
            this.urlBuilder = new StringBuilder();
            this.MessengerInstance.Register<NotificationMessage<DataTable>>(this, (n) => this.BuildUrl(n));
        }


        #endregion

        #region Methods

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <param name="n">The notification message.</param>
        private void BuildUrl(NotificationMessage<DataTable> n)
        {
            this.BuildUrl(n.Content);
        }

        /// <summary>
        /// Builds the URL.
        /// </summary>
        /// <param name="content">The data.</param>
        private void BuildUrl(DataTable data)
        {

            try
            {

                // clear web address
                this.WebAddress = null;

                // clear URL builder
                this.urlBuilder.Clear();

                // append Google maps URL
                this.urlBuilder.Append(this.googleMaps);

                // extract addresses from the DataTable
                List<string> addresses = this.ExtractAddresses(data);

                // append each address
                addresses.ForEach((a) => this.urlBuilder.AppendFormat("{0}{1}", a, this.separator));

                // set web address
                this.WebAddress = this.urlBuilder.ToString();

                // set MapView open flag
                this.IsMapOpen = true;

            }
            catch (Exception ex)
            {

                // get new error id and message
                string id = Guid.NewGuid().ToString();
                string msg = string.Format(Messages.UnexpectedError, "building URL", Environment.NewLine, id);

                // log error
                log.Error(msg, ex);

            }
        }

        /// <summary>
        /// Extracts the addresses.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        private List<string> ExtractAddresses(DataTable data)
        {
            List<string> addresses = new List<string>();

            foreach (DataRow item in data.Rows)
            {

                // add source
                addresses.Add(item.Field<string>("Source Address").ReplaceWhiteSpaceAndNewLines("+"));

                // add destination
                addresses.Add(item.Field<string>("Destination Address").ReplaceWhiteSpaceAndNewLines("+"));

            }

            // remove duplicate addresses and return
            return new HashSet<string>(addresses).ToList();
        }

        /// <summary>
        /// Child classes can override this method to perform
        /// clean-up logic, such as removing event handlers.
        /// </summary>
        protected override void OnDispose()
        {
            this.MessengerInstance.Unregister(this);
            base.OnDispose();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Initialize logger
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The Google maps
        /// </summary>
        private string googleMaps = @"https://www.google.com/maps/dir/";

        /// <summary>
        /// The address separator
        /// </summary>
        private string separator = "/";

        /// <summary>
        /// The URL builder
        /// </summary>
        private StringBuilder urlBuilder;

        private string _webAddress;

        /// <summary>
        /// Gets or sets the web address.
        /// </summary>
        /// <value>
        /// The web address.
        /// </value>
        public string WebAddress
        {
            get
            {
                return _webAddress;
            }
            set
            {

                if (_webAddress == value)
                {
                    return;
                }

                _webAddress = value;
                base.OnPropertyChanged("WebAddress");

            }
        }

        private bool _isMapOpen;

        /// <summary>
        /// Flag indicating the map flyout is open
        /// </summary>
        public bool IsMapOpen
        {
            get
            {
                return _isMapOpen;
            }
            set
            {

                if (_isMapOpen == value)
                {
                    return;
                }

                _isMapOpen = value;

                base.OnPropertyChanged("IsMapOpen");

            }
        }

        #endregion

    }
}
