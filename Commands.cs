using System.Windows.Input;

namespace PPMusic
{
    public static class Commands
    {
        /// <summary>
        /// 主内容区的导航命令
        /// </summary>
        public static ICommand MainRegionNavigationCommand { get; set; }

        /// <summary>
        /// 加载并播放指定专辑命令
        /// </summary>
        public static ICommand PlayAlbumCommand { get; set; }


        /// <summary>
        /// 加载指定专辑,但不开始播放命令
        /// </summary>
        public static ICommand LoadAlbumCommand { get; set; }

        /// <summary>
        /// 显示正在播放的歌曲命令
        /// </summary>
        public static ICommand ShowPlayingSongCommand { get; set; }
        public static ICommand HidePlayingSongCommand { get; set; }
    }
}