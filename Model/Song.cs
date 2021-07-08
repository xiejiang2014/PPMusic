using MvvmHelpers;

namespace PPMusic.Model
{
    public class Song : BaseViewModel
    {
        /// <summary>
        /// 音频文件路径
        /// </summary>
        public string AudioFile { get; set; }

        /// <summary>
        /// 是否被标记为喜欢
        /// </summary>
        public bool IsLiked { get; set; }

        /// <summary>
        /// 歌手
        /// </summary>
        public string PlayerName { get; set; }

        /// <summary>
        /// 所属专辑
        /// </summary>
        public Album ParentAlbum { get; set; }
    }
}