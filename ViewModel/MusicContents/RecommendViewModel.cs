using System.Collections.ObjectModel;
using MvvmHelpers;
using PPMusic.Model;
using Prism.Regions;
using Unity;

namespace PPMusic.ViewModel.MusicContents
{
    class RecommendViewModel : BaseViewModel, INavigationAware
    {
        [InjectionConstructor]
        public RecommendViewModel()
        {
            var albumsGroup = FakeDataCreator.CreateAlbumGroup(3);
            albumsGroup.Title = "Hi 张三 今日为你推荐";
            AlbumsGroupList.Add(albumsGroup);

            albumsGroup       = FakeDataCreator.CreateAlbumGroup(12);
            albumsGroup.Title = "你的歌单宝藏库";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = FakeDataCreator.CreateAlbumGroup(6);
            albumsGroup.Title = "大家都在听";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = FakeDataCreator.CreateAlbumGroup(12);
            albumsGroup.Title = "符合你最近的音乐";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = FakeDataCreator.CreateAlbumGroup(6);
            albumsGroup.Title = "你喜爱的歌手，这里都有";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = FakeDataCreator.CreateAlbumGroup(6);
            albumsGroup.Title = "最热门的电台节目，一键获得";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = FakeDataCreator.CreateAlbumGroup(6);
            albumsGroup.Title = "为你推荐话语流行歌";
            AlbumsGroupList.Add(albumsGroup);
        }

        /// <summary>
        /// 专辑列表
        /// </summary>
        public ObservableCollection<AlbumsGroup> AlbumsGroupList { get; } = new();

        public bool IsShowing { get; set; }

        #region INavigationAware Prism 导航

        /// <summary>
        /// 导航日志
        /// </summary>
        IRegionNavigationJournal _journal;

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
            _journal  = navigationContext.NavigationService.Journal;
            IsShowing = true;
        }

        /// <summary>
        /// 从当前页面导航到其它页面时触发,通常进行保存当前页的数据等操作
        /// </summary>
        /// <param name="navigationContext"></param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        #endregion
    }
}