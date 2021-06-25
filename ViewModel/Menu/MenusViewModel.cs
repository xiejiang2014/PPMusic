using MvvmHelpers;

namespace PPMusic.ViewModel.Menu
{
    internal class MenusViewModel : BaseViewModel
    {
        public ObservableRangeCollection<MenuGroupViewModel> MenuGroups { get; }

        public MenusViewModel(NavigationCatalog navigationCatalog,
                              FakeDataCreator   fakeDataCreator
        )
        {
            MenuGroups = new ObservableRangeCollection<MenuGroupViewModel>(fakeDataCreator.CreateMenuGroups());

        }
    }
}