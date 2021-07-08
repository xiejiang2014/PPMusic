using System.Collections.ObjectModel;
using MvvmHelpers;
using PPMusic.Model;

namespace PPMusic.ViewModel.SongsViews
{
    public class SongsListViewModel : BaseViewModel
    {
        public ObservableCollection<Song> Songs { get; set; } = new();
    }
}