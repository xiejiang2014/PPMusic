using System.Windows;
using ControlzEx.Theming;
using MvvmHelpers;
using PPMusic.View.Menu;
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
            regionManager.RegisterViewWithRegion(navigationCatalog.MenuRegion, typeof(Menus));


            Commands.MainRegionNavigationCommand = new DelegateCommand<string>(MainRegionNavigation);
        }

        private void MainRegionNavigation(string navigationTarget)
        {
            if (!string.IsNullOrWhiteSpace(navigationTarget))
            {
                _regionManager?.RequestNavigate(_navigationCatalog.MainRegion, navigationTarget);
            }
        }
    }
}