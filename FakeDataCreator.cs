using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MoreLinq;
using MvvmHelpers;
using PPMusic.Model;
using PPMusic.ViewModel.Menu;

namespace PPMusic
{
    /// <summary>
    /// 假数据创建器
    /// </summary>
    class FakeDataCreator
    {
        private readonly NavigationCatalog _navigationCatalog;

        public FakeDataCreator(NavigationCatalog navigationCatalog)
        {
            _navigationCatalog = navigationCatalog;
        }

        /// <summary>
        /// 创建主菜单项
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MenuGroupViewModel> CreateMenuGroups()
        {
            yield return new()
                         {
                             Title = "在线音乐",
                             MenuItems = new ObservableRangeCollection<MenuItemViewModel>()
                                         {
                                             new()
                                             {
                                                 Title            = "推荐",
                                                 IconPath         = "M346,384,322,281l80-69-105-9-41-96-41,97-105,8,80,69L166,384l90-54ZM256,43A213,213,0,1,1,43,256,212.537,212.537,0,0,1,256,43Z",
                                                 NavigationTarget = _navigationCatalog.Recommend
                                             },
                                             new()
                                             {
                                                 Title    = "音乐馆",
                                                 IconPath = "M20 2.5V0L6 2v12.17A3 3 0 0 0 5 14H3a3 3 0 0 0 0 6h2a3 3 0 0 0 3-3V5.71L18 4.3v7.88a3 3 0 0 0-1-.17h-2a3 3 0 0 0 0 6h2a3 3 0 0 0 3-3V2.5z",
                                                 NavigationTarget = _navigationCatalog.MusicHall
                                             },
                                             new()
                                             {
                                                 Title    = "视频",
                                                 IconPath = "M363 288l85 85v-234l-85 85v-75c0 -12 -10 -21 -22 -21h-256c-12 0 -21 9 -21 21v214c0 12 9 21 21 21h256c12 0 22 -9 22 -21v-75z",
                                             },
                                             new()
                                             {
                                                 Title    = "电台",
                                                 IconPath = "M9 9H8c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1H7c-.55 0-1 .45-1 1v1c0 .55.45 1 1 1H6c-.55 0-1 .45-1 1v2h1v3c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-3h1v-2c0-.55-.45-1-1-1zM7 7h1v1H7V7zm2 4H8v4H7v-4H6v-1h3v1zm2.09-3.5c0-1.98-1.61-3.59-3.59-3.59A3.593 3.593 0 004 8.31v1.98c-.61-.77-1-1.73-1-2.8 0-2.48 2.02-4.5 4.5-4.5S12 5.01 12 7.49c0 1.06-.39 2.03-1 2.8V8.31c.06-.27.09-.53.09-.81zm3.91 0c0 2.88-1.63 5.38-4 6.63v-1.05a6.553 6.553 0 003.09-5.58A6.59 6.59 0 007.5.91 6.59 6.59 0 00.91 7.5c0 2.36 1.23 4.42 3.09 5.58v1.05A7.497 7.497 0 017.5 0C11.64 0 15 3.36 15 7.5z",
                                             }
                                         }
                         };
            yield return new()
                         {
                             Title = "我的音乐",
                             MenuItems = new ObservableRangeCollection<MenuItemViewModel>()
                                         {
                                             new()
                                             {
                                                 Title    = "我喜欢",
                                                 IconPath = "M896,1408a62.045,62.045,0,0,1-44-18L228,788C220,781,0,580,0,340,0,47,179-128,478-128,653-128,817,10,896,88c79-78,243-216,418-216,299,0,478,175,478,468,0,240-220,441-229,450L940,1390A62.045,62.045,0,0,1,896,1408Z",
                                             },
                                             new()
                                             {
                                                 Title    = "本地和下载",
                                                 IconPath = "M650 300V200H850V100H350V200H550V300H149.6A49.900000000000006 49.900000000000006 0 0 0 100 350.35V999.65C100 1027.45 122.75 1050 149.6 1050H1050.3999999999999C1077.8 1050 1100 1027.55 1100 999.65V350.3499999999999C1100 322.5499999999999 1077.25 299.9999999999998 1050.3999999999999 299.9999999999998H650z",
                                             },
                                             new()
                                             {
                                                 Title    = "最近播放",
                                                 IconPath = "M256,8C119,8,8,119,8,256S119,504,256,504,504,393,504,256,393,8,256,8Zm92.49,313h0l-20,25a16,16,0,0,1-22.49,2.5h0l-67-49.72a40,40,0,0,1-15-31.23V112a16,16,0,0,1,16-16h32a16,16,0,0,1,16,16V256l58,42.5A16,16,0,0,1,348.49,321Z"
                                             },
                                             new()
                                             {
                                                 Title    = "试听列表",
                                                 IconPath = "M16 17a3 3 0 0 1-3 3h-2a3 3 0 0 1 0-6h2a3 3 0 0 1 1 .17V1l6-1v4l-4 .67V17zM0 3h12v2H0V3zm0 4h12v2H0V7zm0 4h12v2H0v-2zm0 4h6v2H0v-2z",
                                             }
                                         }
                         };
        }


        /// <summary>
        /// 创建专辑组
        /// </summary>
        /// <returns></returns>
        public AlbumsGroup CreateAlbumGroup(int albumAmount)
        {
            var result = new AlbumsGroup();

            var albums = CreateAlbums()
                        .Shuffle()
                        .ToList();


            while (result.Albums.Count < albumAmount)
            {
                foreach (var album in albums)
                {
                    result.Albums.Add(album);

                    if (result.Albums.Count >= albumAmount)
                    {
                        break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 创建的专辑
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Album> CreateAlbums()
        {
            yield return new Album()
                         {
                             Title         = "声入人心",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\Cover (1).png"),
                             PlayCountText = "505.5万",
                             Description   = "《声入人心》是由湖南卫视制作的原创新形态声乐演唱节目，共12期，第一季由廖昌永、刘宪华、尚雯婕三位音乐人作为出品人；第二季由廖昌永、张惠妹、尚雯婕三位音乐人作为出品人。节目的播出减少了大家对高雅音乐的误解，让听众发现古典音乐作品的魅力，使听众开始走进剧场更趣味地了解欣赏音乐剧。该歌单收录了声入人心中的一些精彩现场，快来收藏歌单一起聆听经典。"
                         };

            yield return new Album()
                         {
                             Title         = "平行世界",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\平行世界.jpg"),
                             PlayCountText = "1905.2万",
                             Description   = "《平行世界》 我最后的信念，是与你在平行世界再相遇。 G.E.M. 邓紫棋2021年首支全创作单曲《平行世界》，唱出虽痛失所爱却为爱坚持的信念。"
                         };

            yield return new Album()
                         {
                             Title         = "超能力",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\超能力.jpg"),
                             PlayCountText = "1205.5万",
                             Description   = "G.E.M.邓紫棋 最新惊喜作品〈超能力〉 以玩味幽默作品 传达面对不快事情 也要一笑置之的乐观态度 你曾否被骗然后感觉忿忿不平？ 你曾否惊叹原来有人撒谎的能力强到不可思议？"
                         };
        }
    }
}