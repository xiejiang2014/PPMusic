using MvvmHelpers;

namespace PPMusic.ViewModel.Menu
{
    public class MenuItemViewModel : BaseViewModel
    {
        public string IconPath { get; set; }
        
        /// <summary>
        /// 导航目标
        /// </summary>
        public string NavigationTarget { get; set; }
    }
}