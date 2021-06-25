using System.Collections.ObjectModel;
using MvvmHelpers;

namespace PPMusic.Model
{
    class AlbumsGroup : BaseViewModel
    {

        public ObservableCollection<Album> Albums { get; } = new();
    }
}