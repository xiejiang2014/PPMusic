using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    internal class FakeDataCreator
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
            yield return new MenuGroupViewModel()
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
                                                 Title            = "音乐馆",
                                                 IconPath         = "M20 2.5V0L6 2v12.17A3 3 0 0 0 5 14H3a3 3 0 0 0 0 6h2a3 3 0 0 0 3-3V5.71L18 4.3v7.88a3 3 0 0 0-1-.17h-2a3 3 0 0 0 0 6h2a3 3 0 0 0 3-3V2.5z",
                                                 NavigationTarget = _navigationCatalog.MusicHall
                                             },
                                             new()
                                             {
                                                 Title    = "视频(todo)",
                                                 IconPath = "M363 288l85 85v-234l-85 85v-75c0 -12 -10 -21 -22 -21h-256c-12 0 -21 9 -21 21v214c0 12 9 21 21 21h256c12 0 22 -9 22 -21v-75z"
                                             },
                                             new()
                                             {
                                                 Title    = "电台(todo)",
                                                 IconPath = "M9 9H8c.55 0 1-.45 1-1V7c0-.55-.45-1-1-1H7c-.55 0-1 .45-1 1v1c0 .55.45 1 1 1H6c-.55 0-1 .45-1 1v2h1v3c0 .55.45 1 1 1h1c.55 0 1-.45 1-1v-3h1v-2c0-.55-.45-1-1-1zM7 7h1v1H7V7zm2 4H8v4H7v-4H6v-1h3v1zm2.09-3.5c0-1.98-1.61-3.59-3.59-3.59A3.593 3.593 0 004 8.31v1.98c-.61-.77-1-1.73-1-2.8 0-2.48 2.02-4.5 4.5-4.5S12 5.01 12 7.49c0 1.06-.39 2.03-1 2.8V8.31c.06-.27.09-.53.09-.81zm3.91 0c0 2.88-1.63 5.38-4 6.63v-1.05a6.553 6.553 0 003.09-5.58A6.59 6.59 0 007.5.91 6.59 6.59 0 00.91 7.5c0 2.36 1.23 4.42 3.09 5.58v1.05A7.497 7.497 0 017.5 0C11.64 0 15 3.36 15 7.5z"
                                             }
                                         }
                         };
            yield return new MenuGroupViewModel()
                         {
                             Title = "我的音乐",
                             MenuItems = new ObservableRangeCollection<MenuItemViewModel>()
                                         {
                                             new()
                                             {
                                                 Title    = "我喜欢(todo)",
                                                 IconPath = "M896,1408a62.045,62.045,0,0,1-44-18L228,788C220,781,0,580,0,340,0,47,179-128,478-128,653-128,817,10,896,88c79-78,243-216,418-216,299,0,478,175,478,468,0,240-220,441-229,450L940,1390A62.045,62.045,0,0,1,896,1408Z"
                                             },
                                             new()
                                             {
                                                 Title    = "本地和下载(todo)",
                                                 IconPath = "M650 300V200H850V100H350V200H550V300H149.6A49.900000000000006 49.900000000000006 0 0 0 100 350.35V999.65C100 1027.45 122.75 1050 149.6 1050H1050.3999999999999C1077.8 1050 1100 1027.55 1100 999.65V350.3499999999999C1100 322.5499999999999 1077.25 299.9999999999998 1050.3999999999999 299.9999999999998H650z"
                                             },
                                             new()
                                             {
                                                 Title    = "最近播放(todo)",
                                                 IconPath = "M256,8C119,8,8,119,8,256S119,504,256,504,504,393,504,256,393,8,256,8Zm92.49,313h0l-20,25a16,16,0,0,1-22.49,2.5h0l-67-49.72a40,40,0,0,1-15-31.23V112a16,16,0,0,1,16-16h32a16,16,0,0,1,16,16V256l58,42.5A16,16,0,0,1,348.49,321Z"
                                             },
                                             new()
                                             {
                                                 Title    = "试听列表(todo)",
                                                 IconPath = "M16 17a3 3 0 0 1-3 3h-2a3 3 0 0 1 0-6h2a3 3 0 0 1 1 .17V1l6-1v4l-4 .67V17zM0 3h12v2H0V3zm0 4h12v2H0V7zm0 4h12v2H0v-2zm0 4h6v2H0v-2z"
                                             }
                                         }
                         };
        }


        /// <summary>
        /// 创建专辑组
        /// </summary>
        /// <returns></returns>
        public static AlbumsGroup CreateAlbumGroup(int albumAmount)
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
        public static IEnumerable<Album> CreateAlbums()
        {
            yield return new Album()
                         {
                             Title         = "声入人心",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\Cover (1).png"),
                             PlayCountText = "505.5万",
                             Description   = "《声入人心》是由湖南卫视制作的原创新形态声乐演唱节目，共12期，第一季由廖昌永、刘宪华、尚雯婕三位音乐人作为出品人；第二季由廖昌永、张惠妹、尚雯婕三位音乐人作为出品人。节目的播出减少了大家对高雅音乐的误解，让听众发现古典音乐作品的魅力，使听众开始走进剧场更趣味地了解欣赏音乐剧。该歌单收录了声入人心中的一些精彩现场，快来收藏歌单一起聆听经典。",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "平行世界",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\平行世界.jpg"),
                             PlayCountText = "1905.2万",
                             Description   = "《平行世界》 我最后的信念，是与你在平行世界再相遇。 G.E.M. 邓紫棋2021年首支全创作单曲《平行世界》，唱出虽痛失所爱却为爱坚持的信念。", Songs = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "超能力",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\超能力.jpg"),
                             PlayCountText = "1205.5万",
                             Description   = "G.E.M.邓紫棋 最新惊喜作品〈超能力〉 以玩味幽默作品 传达面对不快事情 也要一笑置之的乐观态度 你曾否被骗然后感觉忿忿不平？ 你曾否惊叹原来有人撒谎的能力强到不可思议？",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "刺激不！老板上班都要偷偷听的百首热歌",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3584864556.jpg"),
                             PlayCountText = "18.5万",
                             Description   = "好无聊！枯燥的两点一线的工作占用了你放松的好时光！简直是浪费生命！要不辞职吧！咳咳，这个算了！ 老铁们在上班的时候，你们的老板也要监督你们，他也很无聊，但为什么他每天都不累呢！反而越发的年轻！ 本期歌单揭开秘密，你的老板上班其实都在听这些歌曲放松自己 不要怂，戴好耳机躁动起来，被老板发现要责罚，请把你的一只耳机递给他，一起来摇摆吧！上班更轻松！",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "来一车中国风小说，我还可以继续看！",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3283735297.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "回眸你的一颦一笑，都使我泛开红晕，你步步生莲，化作一片浮云游子意，朗朗笙坤淡开心，钟别古色笙箫徐徐弹，墨笔韵梵净山上，你伫立而斜，刻出一幅画。 突然觉得自己文采真的棒棒哒！哈哈哈哈哈哈，自恋啦！我最爱的中国风啊！第一次这么告白，希望大家喜欢啦！ 2017.11.26",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "当代古典乐：走进孤独患者的禁地",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\893715899.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "安静的氛围、舒缓的旋律，仿佛在慢慢走进每一个孤独者的内心，在不被外界理解的时间里，只有我带着自己的的面具，道出碰撞手上然后自愈。希望这些音乐，能够给同样孤独的人一些心灵的慰藉，有了音乐的陪伴，起码还不是孤零零的。",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "涨知识：那些以提琴为前奏的韩语歌",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3584864556.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "选取了一些以提琴曲为前奏的韩语歌，华丽丽的提琴曲开启了这些韩语歌的前奏，一开始就很抓耳朵，一股高级制作感迎面而来有木有！欢迎补充！",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "孤独患者散步时听的百首日系民谣",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3275708881.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "说到民谣，我喜欢《成都》也喜欢《理想三旬》。 喜欢欧美民谣、中古民谣，也喜欢日本民谣。 日本民谣给人的感觉总是暖阳中带了点轻风，细雨中点了点忧伤。 就像2015年在北海道初次见到你时，你嘴角挂着的青涩微笑。 时隔两年，我偶然还会想起你，那个穿着白色短袖的男孩。 很后悔没留下联系方式。 --2017.北京",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "简单易学！学姐推荐的入门瑞典电音",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\1702976910.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "在华语乐坛里，有这么一群老男人，时光飞逝，音乐却不老，他们的歌总会深入人心，给人一种成熟的感觉，毕竟经历的太多。",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "三字食 | 宅家点外卖参考List",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\4114640001.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "打开三字食把味蕾交给它们来解決，音乐配美味，生活有滋味！ 即使真空不传声，宇宙也知道你想吃下它！等等，我要起飞了！",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "当耐人寻味的吉他声再次响起",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\1398758987.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "美丽的相聚如期而至，那隽永的抒情挂在疲备的吉他上，往事成了一首悠远的歌，总在有夕阳的日子里浮现。我有一个小秘密，藏在我的吉他里，跟着我的拍子，唱我的旋律，让我轻轻地告诉你…",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "纯音·水下光年·自然原韵",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3179735066.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "水下光年 聆听 自然的呓语",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "纯音乐小集「山」主题",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3244804514.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "最近总是繁忙，觉得越在城市跑越不开心，人与事人与人。终于有一晚睡觉，梦里的自己在爬山，很美很美。之后就搜索各种山的图片，听有关山的音乐，幻想自己在旅行，心情就好点了。生活总有各种无可奈何，但我们还有音乐陪伴，很好了不是吗？",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "纯音：悠然自得 聆听陶笛清脆悦耳",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3545485941.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "陶笛（英语：Ocarina，也译作奥卡利那笛、洋埙、瓦埙、土笛、鼓浪笛等），目前国际上比较流行的陶笛多数是一种源自意大利、状似潜艇、有哨口、通常用陶土烧制的吹管乐器。               虽说世事沧桑、风雨更替，但花开不止一季，美丽的风景一直在路上。错过了春天的百花风情，我们还会迎来夏天的蛙叫蝉鸣;错过了秋天的落叶静美，我们还会迎来冬天的雪花摇曳。只要我们守住一颗宁静的心，我们就可以在滚滚红尘中，悠然自得地听高山流水、弄花香满衣。",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "犹如天使的治愈小提琴",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3476526085.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "节奏舒服的小提琴  完美的体现了拉好一个“二胡”的重要性！",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "4500高冷海拔之地！空灵的入耳藏语歌",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\1777789617.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "以前听惯了草原，卓玛，雪山，还有姑娘……现在藏语有了流行歌曲，有了说唱，逐渐走出了自己的风格。作为一个汉族人，真心觉得好听！",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "Billboard 2018百大电音舞曲",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\5847280921.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "Billboard 2018电音舞曲榜全球百大金曲！",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
            yield return new Album()
                         {
                             Title         = "reputation巡演 | 惊喜曲目合辑",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\4368460036.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "巡演已于2018年11月21日结束。也非常期待TS7和她的新巡演。现场表演均为不插电版，可站内并没此版本，所以罗列的是已收录专辑的版本，若想看现场请前往某博关键字搜索，谢谢。",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "哇！通宵无需可乐或咖啡",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\2002998660.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "哇！通宵无需可乐或咖啡 咖啡被高考无效化了，来英国后论文，report又把可乐秒杀了，熬夜上阵顶不住怎么破～戳进来听听歌呗～ “点点，你什么时候会打推销广告了？” “这叫自荐小标语，不要把点点完美的文字不着痕迹地贬低好嘛！” “你自己通宵就算了，别祸国殃民。” “这叫助人为乐，你不懂，对于有责任感不干完活不睡觉的家伙们，点点的歌单是福音好不好！” 听完后—— “点点，你是不是那几次presentation之前都听了这该死的歌单？” “然也～” “怪不得上台和哈士奇一样兴奋，果然该给你肖像绘里加一个肥绒大尾巴。” “改我肖像立绘的时候温柔点，太重的尾巴点点拖不动” “不拖，以后天天朝我摆摆尾巴就好。” “你有毒＝＝” 事后， “点点，你是不是把我这大触的出场写的太平乏了？” “少罗嗦，点点这是在弘扬实事求是的中华传统美德，乖乖赶稿～” “嗯，我去把你黑眼圈加进立绘里。” “！！！”",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "势不可挡！2017混音大佬冲榜力作！",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\878028874.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "每首都经过精挑细选，还有两个月就该评选今年的全球百大DJ",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "出神电音区，够胆你就进 ▪ Trance",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\4237687835.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "关于Trance，大部分人知道”出神“一词，早期Trance为techno和house的混合体，它融合了大量的techno的拍子和节奏方面的结构，但同时加入了更多旋律优美的段子。同时，在鼓点结构方面不像house那样富有活力和拥有难以预测的提升效果。这些trance形式被定义为古典trance比起后辈那些更能带动舞池气氛的trance来说要冗长和抽象。",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "下班最来劲",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\5109267378.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "下班倒计时五分钟，兴奋情绪飙升中。努力工作明天再说，现在只想听一首歌，放飞自我，让你下班最来劲。每月不定期更新～",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "如斯女嗓 骨子里的高冷",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\2914617281.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "▼ 姑娘 你如此出众 你的气质诱我至深 让我狠狠拥抱你的清冷 觊觎你骨子里的莫测 无谓曲高和寡 无关世间万千 你本高冷 我本钟情 5.18.2016♥",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };

            yield return new Album()
                         {
                             Title         = "快节奏 亲密的音乐告白",
                             CoverImageUrl = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources\\Images\\Covers\\256\\3527197788.jpg"),
                             PlayCountText = "3.5万",
                             Description   = "音乐的旋律是委婉唯美的诗歌 是奔放狂野的气息  是迷离徜恍的梦境  是我们内心深处的呐喊 也是心灵的亲密告白：        喜欢文字 喜欢音乐 喜欢绘画 喜欢舞蹈 喜欢精美细腻的小礼物 那么多那么多的喜欢 不及对你的喜欢 喜欢天马行空的想象 却不及想念你的万分之一             幻想如此美妙 没有你心中空洞的无法自我疗伤  唯有在睡梦中望着你深邃的目光 黎明到来也不能遗忘 ……",
                             Songs         = new ObservableCollection<Song>(GetRandomSongs())
                         };
        }


        public static IEnumerable<Song> GetRandomSongs()
        {
            _mp3Files ??= GetRandomWaveFiles();

            return _mp3Files
               .Select(v => new Song()
                            {
                                AudioFile = v
                            }
                      );
        }

        private static IEnumerable<string> _mp3Files;

        /// <summary>
        /// 从mp3文件夹中随机返回一些mp3文件的路径.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetRandomWaveFiles()
        {
            var dirInfo = new DirectoryInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                                                         "Resources\\mp3")
                                           );

            return dirInfo.EnumerateFiles("*.mp3")
                          .Select(v => v.FullName);
        }
    }
}