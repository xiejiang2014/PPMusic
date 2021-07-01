using System.Windows;
using ControlzEx.Theming;
using MvvmHelpers;
using PPMusic.View.Menu;
using PPMusic.ViewModel.Menu;
using Prism.Commands;
using Prism.Regions;
using Unity;

namespace PPMusic.ViewModel
{
    class ShellWindowViewModel : BaseViewModel
    {
        private readonly IRegionManager    _regionManager;
        private readonly NavigationCatalog _navigationCatalog;


        [InjectionConstructor]
        public ShellWindowViewModel(IRegionManager    regionManager,
                                    NavigationCatalog navigationCatalog
        )
        {
            ThemeManager.Current.ChangeThemeColorScheme(Application.Current, "PPGreenBlue");
            _regionManager     = regionManager;
            _navigationCatalog = navigationCatalog;

            Commands.MainRegionNavigationCommand = new DelegateCommand<MenuItemViewModel>(MainRegionNavigation);

            Commands.ShowPlayingSongCommand = new DelegateCommand(ShowPlayingSong);


            regionManager.RegisterViewWithRegion(navigationCatalog.MenuRegion,              typeof(Menus));
            regionManager.RegisterViewWithRegion(navigationCatalog.TitleRegion,             typeof(View.TitleBar.TitleBar));
            regionManager.RegisterViewWithRegion(navigationCatalog.PlayerCommandsBarRegion, typeof(View.PlayerCommandsBar.PlayerCommandsBar));

        }




        private void MainRegionNavigation(MenuItemViewModel menuItemViewModel)
        {
            if (!string.IsNullOrWhiteSpace(menuItemViewModel?.NavigationTarget))
            {
                _regionManager?.RequestNavigate(_navigationCatalog.MainRegion,
                                                menuItemViewModel.NavigationTarget,
                                                new NavigationParameters()
                                                {
                                                    {"MenuItemViewModel", menuItemViewModel}
                                                });
            }
        }


        /// <summary>
        /// 显示当前播放的歌曲
        /// </summary>
        private void ShowPlayingSong()
        {
            //让 PlayingSong 从窗口最下方滑出
            
        }
    }
}