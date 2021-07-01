using MvvmHelpers;
using Prism.Commands;
using Prism.Regions;
using Unity;

namespace PPMusic.ViewModel.TitleBar
{
    public class TitleBarViewModel : BaseViewModel
    {
        private readonly NavigationCatalog _navigationCatalog;
        private readonly IRegionManager    _regionManager;


        /// <summary>
        /// 导航日志
        /// </summary>
        public IRegionNavigationJournal Journal { get; set; }

        [InjectionConstructor]
        public TitleBarViewModel(NavigationCatalog navigationCatalog,
                                 IRegionManager    regionManager
        )
        {
            _navigationCatalog = navigationCatalog;
            _regionManager     = regionManager;

            Journal = _regionManager.Regions[_navigationCatalog.MainRegion].NavigationService.Journal;

            _regionManager.Regions[_navigationCatalog.MainRegion].NavigationService.Navigated += NavigationService_Navigated;
        }

        private void NavigationService_Navigated(object                    sender,
                                                 RegionNavigationEventArgs e
        )
        {
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }

        #region 向后命令

        private DelegateCommand _goBackCommand;

        public DelegateCommand GoBackCommand =>
            _goBackCommand ??= new DelegateCommand(() => Journal.GoBack(),
                                                   () => Journal.CanGoBack
                                                  );

        #endregion


        #region 向前命令

        private DelegateCommand _goForwardCommand;

        public DelegateCommand GoForwardCommand =>
            _goForwardCommand ??= new DelegateCommand(() => Journal.GoForward(),
                                                      () => Journal.CanGoForward
                                                     );

        #endregion
    }
}