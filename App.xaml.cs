using System.Windows;
using System.Windows.Media;
using ControlzEx.Theming;
using PPMusic.View;
using PPMusic.View.MainContentPages;
using PPMusic.View.Menu;
using PPMusic.View.PlayerCommandsBar;
using PPMusic.View.PlayingSong;
using PPMusic.View.TitleBar;
using PPMusic.ViewModel;
using PPMusic.ViewModel.MainContentPages;
using PPMusic.ViewModel.Menu;
using PPMusic.ViewModel.PlayerCommandsBar;
using PPMusic.ViewModel.TitleBar;
using Prism.Ioc;
using Prism.Mvvm;

namespace PPMusic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        public App()
        {
            var color = Color.FromRgb(31, 211, 182);

            var theme = new Theme(
                                  "Light.PPGreenBlue",
                                  "PPGreenBlue(Light)",
                                  "Light",
                                  "PPGreenBlue",
                                  color,
                                  new SolidColorBrush(color),
                                  false,
                                  false
                                 );

            ThemeManager.Current.AddTheme(theme);
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册导航地址薄
            var navigationCatalog = new NavigationCatalog();
            containerRegistry.RegisterInstance(navigationCatalog);

            //注册假数据创建器
            containerRegistry.RegisterSingleton<FakeDataCreator>();

            //注册播放控制栏视图模型的单例,由于会出现多个播放控制栏 共享一个视图模型的情况,所以将其设为单例
            containerRegistry.RegisterSingleton<PlayerCommandsBarViewModel>();

            //注册导航
            containerRegistry.RegisterForNavigation<Recommend>(navigationCatalog.Recommend);
            containerRegistry.RegisterForNavigation<MusicHall>(navigationCatalog.MusicHall);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<ShellWindow, ShellWindowViewModel>();
            ViewModelLocationProvider.Register<PlayerCommandsBar, PlayerCommandsBarViewModel>();
            ViewModelLocationProvider.Register<PlayingSong, PlayerCommandsBarViewModel>();

            ViewModelLocationProvider.Register<Menus, MenusViewModel>();
            ViewModelLocationProvider.Register<TitleBar, TitleBarViewModel>();

            ViewModelLocationProvider.Register<Recommend, RecommendViewModel>();
            ViewModelLocationProvider.Register<MusicHall, MusicHallViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }
    }
}