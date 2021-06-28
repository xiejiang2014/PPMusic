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
        /// 加载并播放指定专辑
        /// </summary>
        public static ICommand PlayAlbumCommand { get; set; }


        /// <summary>
        /// 加载指定专辑,但不开始播放
        /// </summary>
        public static ICommand LoadAlbumCommand { get; set; }
    }
}