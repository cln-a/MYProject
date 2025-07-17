using Application.Model;
using System.Collections.ObjectModel;
using System.Windows;              // Application
using System.Windows.Threading;   // Dispatcher

namespace Application.UI
{
    public class BasePageViewModel<T> : BaseViewModel
    {
        protected ObservableCollection<T> _items;
        protected int _pagenumber = 1;
        protected int _pagesize = 15;
        protected int _totalcount;
        protected int _totalpage = 1;

        protected DelegateCommand _refreshCommand;
        protected DelegateCommand _InsertCommand;
        protected DelegateCommand<T> _deleteCommand;
        protected DelegateCommand<T> _updateCommand;
        protected DelegateCommand _previousPageCommand;
        protected DelegateCommand _nextPageCommand;
        protected DelegateCommand<T> _writeCommand;

        public ObservableCollection<T> Items => _items;
        public int pageNumber { get => _pagenumber; set => SetProperty(ref _pagenumber, value); }
        public int pageSize { get => _pagesize; set => SetProperty(ref _pagesize, value); }
        public int totalCount { get => _totalcount; set => SetProperty(ref _totalcount, value); }
        public int totalPage { get => _totalpage; set => SetProperty(ref _totalpage, value); }

        public DelegateCommand RefreshCommand => _refreshCommand ??= new DelegateCommand(Initialize);
        public DelegateCommand InsertCommand => _InsertCommand ??= new DelegateCommand(InsertCmd);
        public DelegateCommand<T> DelegateCommand => _deleteCommand ??= new DelegateCommand<T>(DeleteCmd);
        public DelegateCommand<T> UpdateCommand => _updateCommand ??= new DelegateCommand<T>(UpdateCmd);
        public DelegateCommand PreviousPageCommand => _previousPageCommand ??= new DelegateCommand(PreviousPageCmd);
        public DelegateCommand NextPageCommand => _nextPageCommand ??= new DelegateCommand(NextPageCmd);
        public DelegateCommand<T> WriteValueCommand => _writeCommand ??= new DelegateCommand<T>(WriteValueCmd);

        public BasePageViewModel() => _items = [];

        protected void SetPageResult(PageResult<T> pageResult)
        {
            pageNumber = pageResult.pagenumber;
            pageSize = pageResult.pageSize;
            totalCount = pageResult.totalCount;
            totalPage = pageResult.totalPage;
        }

        protected virtual Task<PageResult<T>> GetPage() => null!;

        protected virtual async void PageUpdateCmd()
        {
            var pageResult = await GetPage();
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Items.Clear();
                Items.AddRange(pageResult.items);
                SetPageResult(pageResult);
            });
        }

        protected override void Initialize() =>  PageUpdateCmd();

        protected virtual void InsertCmd() { }

        protected virtual void DeleteCmd(T entity) { }

        protected virtual void UpdateCmd(T entity) { }

        private void PreviousPageCmd()
        {
            if (pageNumber > 1)
                pageNumber--;
            PageUpdateCmd();
        }

        private void NextPageCmd()
        {
            if (totalPage == 1)
                return;
            if (pageNumber < totalPage)
                pageNumber++;
            PageUpdateCmd();
        }

        protected virtual void WriteValueCmd(T dto) { }
    }
}
