using MvvmHelpers;
using PPMusic.ViewModel.Menu;
using Prism.Regions;

namespace PPMusic.ViewModel.MainContentPages
{
    internal class ContentViewModelBase : BaseViewModel, INavigationAware
    {
        /// <summary>
        /// 导航日志
        /// </summary>
        protected IRegionNavigationJournal Journal;

        protected MenuItemViewModel MenuItemViewModel;

        /// <summary>
        /// 即将从其它页面导航到当前页时触发.
        /// 返回 true 表示重用已有实例;false 则创建新的实例.
        /// 注意
        /// 1:IRegionMemberLifetime 接口中的 KeepAlive 属性的优先级大于此方法的返回值.
        ///   如果 IRegionMemberLifetime.KeepAlive 为 false,那么这里返回 true 也无法重用.
        /// 2:将空的区域导航到当前页时,不触发此方法
        /// </summary>
        /// <param name="navigationContext"></param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        /// <summary>
        /// 导航到当前页时触发
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Journal = navigationContext.NavigationService.Journal;

            if (navigationContext.Parameters["MenuItemViewModel"] is MenuItemViewModel menuItemViewModel)
            {
                MenuItemViewModel            = menuItemViewModel;
                MenuItemViewModel.IsSelected = true;
            }

        }

        /// <summary>
        /// 从当前页面导航到其它页面时触发,通常进行保存当前页的数据等操作
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            if (MenuItemViewModel is not null)
            {
                MenuItemViewModel.IsSelected = false;
            }
        }
    }
}