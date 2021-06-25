using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PPMusic.View;
using PPMusic.View.Menu;
using PPMusic.View.MusicContents;
using PPMusic.ViewModel;
using PPMusic.ViewModel.Menu;
using PPMusic.ViewModel.MusicContents;
using Prism.Ioc;
using Prism.Mvvm;

namespace PPMusic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //注册导航地址薄
            var navigationCatalog = new NavigationCatalog();
            containerRegistry.RegisterInstance(navigationCatalog);

            //注册假数据创建器
            containerRegistry.RegisterSingleton<FakeDataCreator>();

            //注册导航
            containerRegistry.RegisterForNavigation<Menus>(navigationCatalog.MenuRegion);
            containerRegistry.RegisterForNavigation<Recommend>(navigationCatalog.Recommend);
            containerRegistry.RegisterForNavigation<MusicHall>(navigationCatalog.MusicHall);
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.Register<ShellWindow, ShellWindowViewModel>();
            ViewModelLocationProvider.Register<Menus, MenusViewModel>();
            ViewModelLocationProvider.Register<Recommend, RecommendViewModel>();
            ViewModelLocationProvider.Register<MusicHall, MusicHallViewModel>();
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<ShellWindow>();
        }
    }
}