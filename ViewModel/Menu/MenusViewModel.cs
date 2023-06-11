using System.Linq;
using System.Windows.Input;
using MvvmHelpers;
using Prism.Commands;

namespace PPMusic.ViewModel.Menu
{
    internal class MenusViewModel : BaseViewModel
    {
        public ObservableRangeCollection<MenuGroupViewModel> MenuGroups { get; }

        private ICommand _loadedCommand;
        public  ICommand LoadedCommand => _loadedCommand ??= new DelegateCommand(Loaded);

        public MenusViewModel(FakeDataCreator fakeDataCreator
        )
        {
            MenuGroups = new ObservableRangeCollection<MenuGroupViewModel>(fakeDataCreator.CreateMenuGroups());
        }


        private void Loaded()
        {
            //默认导航到推荐页
            Commands.MainRegionNavigationCommand.Execute(MenuGroups.First().MenuItems.FirstOrDefault());
        }
    }
}