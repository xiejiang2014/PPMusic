using MvvmHelpers;
using PPMusic.Model;

namespace PPMusic.ViewModel.PlayingSong
{
    public class PlayingSongViewModel : BaseViewModel
    {
        public Album Album { get; set; }

        public Song Song { get; set; }


    }
}