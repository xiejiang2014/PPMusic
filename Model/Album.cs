using System;
using System.Collections.ObjectModel;
using MoreLinq;
using MvvmHelpers;

namespace PPMusic.Model
{
    /// <summary>
    /// 专辑
    /// </summary>
    public class Album : BaseViewModel
    {
        /// <summary>
        /// 封面图片
        /// </summary>
        public string CoverImageUrl { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        public ObservableCollection<string> Tags { get; set; } = new();

        /// <summary>
        /// 播放量
        /// </summary>
        public string PlayCountText { get; set; }

        /// <summary>
        /// 收藏量
        /// </summary>
        public string CollectionCountText { get; set; }

        /// <summary>
        /// 语种
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// 发行时间
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// 发行公司
        /// </summary>
        public string PublisherCompany { get; set; }


        private readonly ObservableCollection<Song> _songs = new();

        /// <summary>
        /// 歌曲列表
        /// </summary>
        public ObservableCollection<Song> Songs
        {
            get => _songs;
            init
            {
                _songs = value;
                _songs.ForEach(v => v.ParentAlbum = this);
            }
        }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}