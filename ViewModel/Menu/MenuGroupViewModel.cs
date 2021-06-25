using MvvmHelpers;

namespace PPMusic.ViewModel.Menu
{
    public class MenuGroupViewModel : BaseViewModel
    {
        public ObservableRangeCollection<MenuItemViewModel> MenuItems { get; set; }
    }
}