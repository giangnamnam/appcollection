using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Damany.Imaging.Common;
using Damany.PortraitCapturer.DAL;
using Damany.RemoteImaging.Common;
using PagedList;

namespace RemoteImaging
{
    public class PicQueryFormPresenter : IPicQueryPresenter
    {
        public PicQueryFormPresenter(IPicQueryScreen screen,
                                      ConfigurationManager configManager)
        {
            this.screen = screen;
            _configManager = configManager;
        }

        #region IPicQueryPresenter Members

        public void Search()
        {
            this.range = this.screen.TimeRange;
            selectedCamera = screen.SelectedCamera;

            if (selectedCamera == null)
            {
                return;
            }

            Action disableUI = delegate
            {
                EnableScreen(false);
                this.screen.ShowStatus("开始搜索");
                this.screen.IsBusy = true;
            };

            Action enableUI = delegate
            {
                EnableScreen(true);
                this.screen.ShowStatus("搜索完毕");
                this.screen.IsBusy = false;
            };

            this.DoActionsAsync(disableUI, enableUI,
                (Action)SearchInternal,
                (Action)CalculatePaging,
                (Action)ShowCurrentPage);

        }

        public void PlayVideo()
        {
            var p = this.screen.SelectedItem;

            if (p != null) VideoPlayer.PlayRelatedVideo(p.CapturedAt, p.CapturedFrom.Id);
        }

        public void SelectedItemChanged()
        {
            if (this.screen.SelectedItem == null) return;

            var item = this.screen.SelectedItem;

            this.screen.CurrentPortrait = item;

            var frame = item.Frame;

            if (frame != null)
            {
                this.screen.CurrentBigPicture = frame.GetImage().ToBitmap();
            }


        }

        public void Start()
        {
            this.screen.AttachPresenter(this);
            this.screen.Cameras = _configManager.GetCameras().ToArray();
            this.screen.Show();
        }


        private void ShowCurrentPage()
        {
            if (this._portraits == null) return;

            this.screen.Clear();

            foreach (var item in _currentPage)
            {
                this.screen.AddItem(item);
            }
        }



        public void NavigateToPrev()
        {
            if (this._portraits == null || _currentPage == null) return;

            if (_currentPage.HasPreviousPage)
            {
                MoveCurrentPage(i=>i-1);
                UpdateCurrentPageAsync();
            }
        }

        private void MoveCurrentPage(Func<int, int> nextPageMethod)
        {
            var idx = nextPageMethod(_currentPage.PageIndex);
            _currentPage = this._portraits.ToPagedList(idx, this.screen.PageSize);
        }

        public void NavigateToNext()
        {
            if (this._portraits == null || _currentPage == null) return;

            if (this._currentPage.HasNextPage)
            {
                MoveCurrentPage(i => i + 1);
                UpdateCurrentPageAsync();
            }
        }

        public void NavigateToLast()
        {
            if (this._portraits == null || _currentPage == null) return;
            if ( _currentPage.IsLastPage ) return;

            MoveCurrentPage(i => _currentPage.PageCount - 1);
            UpdateCurrentPageAsync();

        }

        public void NavigateToFirst()
        {
            if (this._portraits == null || _currentPage == null) return;
            if (_currentPage.IsFirstPage) return;

            MoveCurrentPage(i => 0);
            UpdateCurrentPageAsync();
        }


        public void PageSizeChanged()
        {
            if (this._portraits == null || _currentPage == null) return;

            Action pre = delegate { this.screen.Clear(); this.EnableScreen(false); };
            Action after = delegate { this.EnableScreen(true); };

            this.DoActionsAsync(pre, after, this.ShowCurrentPage);
        }

        #endregion

        private void DoActionsAsync(Action entryAction, Action exitAction, params Action[] actions)
        {
            if (entryAction != null)
            {
                entryAction();
            }


            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    foreach (var item in actions)
                    {
                        item();
                    }
                }
                finally
                {
                    if (exitAction != null)
                    {
                        exitAction();
                    }

                }
            });
        }

        private void CalculatePaging()
        {
            if (this._portraits.Count > 0)
            {
                this.screen.CurrentPage = _currentPage.PageNumber;
                this.screen.TotalPage = _currentPage.PageCount;
                }
            }

        private void SearchInternal()
        {
            _currentPage = this._portraits.ToPagedList(0, this.screen.PageSize);
        }

        private void EnableScreen(bool enable)
        {
            this.screen.ShowUserIsBusy(!enable);
            this.screen.EnableSearchButton(enable);
            this.screen.EnableNavigateButtons(enable);
        }

        private void UpdateCurrentPageAsync()
        {
            Action pre = delegate { this.EnableScreen(false); this.CalculatePaging(); };
            Action after = delegate { this.EnableScreen(true); };

            this.DoActionsAsync(pre, after, this.ShowCurrentPage);
        }

        private Damany.Util.DateTimeRange range;
        IPicQueryScreen screen;
        private readonly ConfigurationManager _configManager;
        IList<Damany.Imaging.Common.Portrait> _portraits;
        private IPagedList<Damany.Imaging.Common.Portrait> _currentPage;
        Damany.PC.Domain.Destination selectedCamera;

    }
}
