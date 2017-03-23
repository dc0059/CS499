using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CS499.TCMS.View.ViewModels
{
    /// <summary>
    /// This class will extend the observable collection to provide other functionality like paging
    /// </summary>
    /// <typeparam name="T">class</typeparam>
    public class ObservableCollectionExtended<T> : ObservableCollection<T>, INotifyPropertyChanged
    {

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="items">list of items</param>
        public ObservableCollectionExtended(List<T> items)
            : base()
        {

            // create internal lists
            this.collection = new List<T>(items);
            this.collectionFiltered = new List<T>();

            // expose read only version of the list
            this._unfilteredList = new ReadOnlyCollection<T>(this.collection);

            // set page sizes
            this.PageSizes = new int[] { 5, 10, 50, 100, 250, 500, 1000, 5000, 10000, 50000, 100000, 500000 };

            // set page size
            this.pageSize = this.PageSizes[0];

            // default the current page to 1
            this.currentPage = 1;

            // set filtered flag
            this.filtered = false;

        }

        #endregion

        #region Methods

        /// <summary>
        /// Add item to the collection
        /// </summary>
        /// <param name="item">item to add</param>
        public void AddItem(T item)
        {
            this.collection.Add(item);
        }

        /// <summary>
        /// Add range1 of items to the collection
        /// </summary>
        /// <param name="items">list of items</param>
        public void AddRange(List<T> items)
        {
            this.collection.AddRange(items);
        }

        /// <summary>
        /// Remove item from the collection
        /// </summary>
        /// <param name="item">item to remove</param>
        public void RemoveItem(T item)
        {
            this.collection.Remove(item);
        }

        /// <summary>
        /// Refresh the observable collection based on the 
        /// page size and current page
        /// </summary>
        public void Refresh()
        {

            // clear the list
            this.Clear();

            // set filted flag
            this.filtered = false;

            // get filtered list
            List<T> page = this.collection
                               .Skip(this.pageSize * (this.currentPage - 1))
                               .Take(this.pageSize).ToList();

            // loop through and add all items in the list
            page.ForEach((i) => this.Add(i));

            // refresh page info
            base.OnPropertyChanged(new PropertyChangedEventArgs("PageInfo"));
            base.OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
            base.OnPropertyChanged(new PropertyChangedEventArgs("PageCount"));

        }

        public void RefreshFiltered()
        {
            // clear the list
            this.Clear();

            // get filtered list
            List<T> page = this.collectionFiltered
                               .Skip(this.pageSize * (this.currentPage - 1))
                               .Take(this.pageSize).ToList();

            // loop through and add all items in the list
            page.ForEach((i) => this.Add(i));

            // refresh page info
            base.OnPropertyChanged(new PropertyChangedEventArgs("PageInfo"));
            base.OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
            base.OnPropertyChanged(new PropertyChangedEventArgs("PageCount"));
        }

        /// <summary>
        /// Goto first page
        /// </summary>
        public void GotoFirstPage()
        {

            this.currentPage = 1;

            if (this.filtered)
            {
                this.RefreshFiltered();
            }
            else
            {
                this.Refresh();
            }

        }

        /// <summary>
        /// Goto next page
        /// </summary>
        public void GotoNextPage()
        {

            this.currentPage++;

            if (this.filtered)
            {
                this.RefreshFiltered();
            }
            else
            {
                this.Refresh();
            }

        }

        /// <summary>
        /// Goto previous page
        /// </summary>
        public void GotoPreviousPage()
        {

            this.currentPage--;

            if (this.filtered)
            {
                this.RefreshFiltered();
            }
            else
            {
                this.Refresh();
            }

        }

        /// <summary>
        /// Goto last page
        /// </summary>
        public void GotoLastPage()
        {

            this.currentPage = this.PageCount;

            if (this.filtered)
            {
                this.RefreshFiltered();
            }
            else
            {
                this.Refresh();
            }

        }

        /// <summary>
        /// Calculate the total numbers of pages
        /// </summary>
        /// <returns>page count</returns>
        private int CalculatePageCount()
        {

            int count = this.filtered ? this.collectionFiltered.Count : this.collection.Count;

            if (count <= this.pageSize)
            {
                return 1;
            }
            else
            {
                if ((count % this.pageSize) == 0)
                {
                    return count / this.pageSize;
                }
                else
                {
                    return (count / this.pageSize) + 1;
                }
            }

        }

        /// <summary>
        /// Search the collection for the first matching item
        /// and return the first match
        /// </summary>
        /// <param name="filter">type of filter</param>
        /// <param name="searchText">search text</param>
        /// <param name="props">reportProperties to search</param>
        /// <returns>first matching item</returns>
        public T Search(string filter, string searchText, params string[] props)
        {

            // pass function
            return this.Search((i) =>
            {

                bool result = false;

                // loop through each property and check it against the search searchText
                foreach (string prop in props)
                {

                    // get property value
                    object propValue = i.GetType().GetProperty(prop).GetValue(i, null);

                    // process nulls as empty strings
                    string value = string.Empty;
                    if (propValue != null)
                    {
                        value = propValue.ToString();
                    }

                    switch (filter)
                    {

                        case "contains":

                            result = value.Contains(searchText, StringComparison.OrdinalIgnoreCase);

                            break;

                        case "does not contain":

                            result = value.Contains(searchText, StringComparison.OrdinalIgnoreCase);

                            break;

                        case "starts with":

                            result = value.StartsWith(searchText, StringComparison.OrdinalIgnoreCase);

                            break;

                        case "does not start with":

                            result = value.StartsWith(searchText, StringComparison.OrdinalIgnoreCase);

                            break;

                        case "ends with":

                            result = value.EndsWith(searchText, StringComparison.OrdinalIgnoreCase);

                            break;

                        case "does not end with":

                            result = value.EndsWith(searchText, StringComparison.OrdinalIgnoreCase);

                            break;

                        case "equals":

                            result = value.Equals(searchText, StringComparison.OrdinalIgnoreCase);

                            break;

                        case "does not equal":

                            result = value.Equals(searchText, StringComparison.OrdinalIgnoreCase);

                            break;

                        default:

                            result = value.Contains(searchText, StringComparison.OrdinalIgnoreCase);

                            break;
                    }

                    if (!filter.StartsWith("does not") && result)
                    {
                        return true;
                    }
                    else if (result)
                    {
                        return false;
                    }

                }

                if (filter.StartsWith("does not"))
                {
                    return true;
                }

                return result;

            });

        }

        /// <summary>
        /// Search the collection for the first matching item
        /// and return the first match
        /// </summary>
        /// <param name="filter">filter function</param>
        /// <returns>first matching item</returns>
        public T Search(Func<T, bool> filter)
        {
            this.filter = filter;
            this.filtered = true;
            this.CurrentPage = 1;
            return this.Search();
        }

        /// <summary>
        /// Search the collection for the first matching item
        /// and return the first match
        /// </summary>
        /// <returns>first matching item</returns>
        private T Search()
        {

            // filter list
            this.collectionFiltered = this.collection.Where(this.filter).ToList();

            // set first
            this.match = collectionFiltered.FirstOrDefault();

            // refresh page info
            this.RefreshFiltered();

            return this.match;
        }

        /// <summary>
        /// Clear internal collection
        /// </summary>
        public void ClearAll()
        {
            this.collection.Clear();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Last matched item in the collection
        /// </summary>
        private T match;

        /// <summary>
        /// Flag indicating the list is filtered
        /// </summary>
        private bool filtered;

        /// <summary>
        /// Filter function
        /// </summary>
        private Func<T, bool> filter;

        /// <summary>
        /// Internal collection to hold the whole list
        /// </summary>
        private List<T> collection;

        /// <summary>
        /// Internal collection to hold the filtered list
        /// </summary>
        private List<T> collectionFiltered;

        private ReadOnlyCollection<T> _unfilteredList;

        /// <summary>
        /// Unfiltered read only list of the item collection
        /// </summary>
        public ReadOnlyCollection<T> UnfilteredList
        {
            get { return _unfilteredList; }
        }


        private int pageSize;

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {

                if (pageSize == value)
                {
                    return;
                }

                pageSize = value;

                base.OnPropertyChanged(new PropertyChangedEventArgs("PageSize"));

                if (this.filtered)
                {
                    this.RefreshFiltered();
                }
                else
                {
                    // set current page to 1 and refresh the list
                    this.currentPage = 1;
                    this.Refresh();
                }

                base.OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));
                base.OnPropertyChanged(new PropertyChangedEventArgs("PageCount"));

            }
        }

        /// <summary>
        /// Page sizes
        /// </summary>
        public int[] PageSizes { get; set; }

        /// <summary>
        /// Number of pages
        /// </summary>
        public int PageCount
        {
            get
            {
                return this.CalculatePageCount();
            }
        }

        private int currentPage;

        /// <summary>
        /// Number of the current page
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {

                if (currentPage == value || value > this.PageCount)
                {
                    return;
                }

                currentPage = value;

                if (this.filtered)
                {
                    this.RefreshFiltered();
                }
                else
                {
                    this.Refresh();
                }

                base.OnPropertyChanged(new PropertyChangedEventArgs("CurrentPage"));

            }
        }

        /// <summary>
        /// Check to see if you can move to the first page
        /// </summary>
        public bool CanGotoFirstPage
        {
            get
            {
                return this.currentPage != 1;
            }
        }

        /// <summary>
        /// Check to see if you can move to next page
        /// </summary>
        public bool CanGotoNextPage
        {
            get
            {
                return this.currentPage < this.PageCount;
            }
        }

        /// <summary>
        /// Check to see if you can move to next page
        /// </summary>
        public bool CanGotoPreviousPage
        {
            get
            {
                return this.currentPage >= 2;
            }
        }

        /// <summary>
        /// Check to see if you can move the last page
        /// </summary>
        public bool CanGotoLastPage
        {
            get
            {
                return this.currentPage != this.PageCount;
            }
        }

        /// <summary>
        /// Shows page x of y
        /// </summary>
        public string PageInfo
        {
            get
            {
                return string.Format("Page {0} of (1)", this.currentPage, this.PageCount);
            }
        }

        private ICommand _commandFirst;

        public ICommand CommandFirst
        {
            get
            {

                if (_commandFirst == null)
                {
                    _commandFirst = new RelayCommand(param =>
                    {
                        this.GotoFirstPage();
                    },
                        param => this.CanGotoFirstPage);
                }

                return _commandFirst;
            }
        }

        private ICommand _commandPrevious;

        public ICommand CommandPrevious
        {
            get
            {

                if (_commandPrevious == null)
                {
                    _commandPrevious = new RelayCommand(param =>
                    {
                        this.GotoPreviousPage();
                    },
                        param => this.CanGotoPreviousPage);
                }

                return _commandPrevious;
            }
        }

        private ICommand _commandNext;

        public ICommand CommandNext
        {
            get
            {

                if (_commandNext == null)
                {
                    _commandNext = new RelayCommand(param =>
                    {
                        this.GotoNextPage();
                    },
                        param => this.CanGotoNextPage);
                }

                return _commandNext;
            }
        }

        private ICommand _commandLast;

        public ICommand CommandLast
        {
            get
            {

                if (_commandLast == null)
                {
                    _commandLast = new RelayCommand(param =>
                    {
                        this.GotoLastPage();
                    },
                        param => this.CanGotoLastPage);
                }

                return _commandLast;
            }
        }

        #endregion

    }
}
