using System.Linq;
using System.Windows;
using System.Windows.Input;
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

        private ICommand _loadedCommand;
        public  ICommand LoadedCommand => _loadedCommand ??= new DelegateCommand(Loaded);


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

        private void Loaded()
        {
            //默认导航到推荐页
            Commands.MainRegionNavigationCommand.Execute(_navigationCatalog.Recommend);

            //随便加载一个专辑
            Commands.LoadAlbumCommand.Execute(FakeDataCreator.CreateAlbums().FirstOrDefault());
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