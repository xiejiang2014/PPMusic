using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using PPMusic.Model;
using Prism.Common;
using Unity;

namespace PPMusic.ViewModel.MusicContents
{
    class MusicHallViewModel : BaseViewModel
    {
        [InjectionConstructor]
        public MusicHallViewModel(FakeDataCreator fakeDataCreator)
        {
            var albumsGroup = fakeDataCreator.CreateAlbumGroup(3);
            albumsGroup.Title = "Hi 邦德 今日为你推荐";
            AlbumsGroupList.Add(albumsGroup);

            albumsGroup       = fakeDataCreator.CreateAlbumGroup(12);
            albumsGroup.Title = "你的歌单宝藏库";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = fakeDataCreator.CreateAlbumGroup(6);
            albumsGroup.Title = "大家都在听";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = fakeDataCreator.CreateAlbumGroup(12);
            albumsGroup.Title = "符合你最近的音乐";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = fakeDataCreator.CreateAlbumGroup(6);
            albumsGroup.Title = "你喜爱的歌手，这里都有";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = fakeDataCreator.CreateAlbumGroup(6);
            albumsGroup.Title = "最热门的电台节目，一键获得";
            AlbumsGroupList.Add(albumsGroup);


            albumsGroup       = fakeDataCreator.CreateAlbumGroup(6);
            albumsGroup.Title = "为你推荐话语流行歌";
            AlbumsGroupList.Add(albumsGroup);
        }

        public ObservableCollection<AlbumsGroup> AlbumsGroupList { get; } = new();
    }
}