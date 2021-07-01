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


        /// <summary>
        /// 菜单是否被选中
        /// </summary>
        public bool IsSelected { get; set; }
    }
}