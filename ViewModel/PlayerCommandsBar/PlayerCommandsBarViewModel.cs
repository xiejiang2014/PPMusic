using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using JetBrains.Annotations;
using MoreLinq;
using MvvmHelpers;
using PPMusic.Model;
using PPMusic.Player;
using PPMusic.Player.Enum;
using Prism.Commands;
using PropertyChanged;

namespace PPMusic.ViewModel.PlayerCommandsBar
{
    [UsedImplicitly]
    public class PlayerCommandsBarViewModel : BaseViewModel
    {
        #region 播放核心

        public WaveDirectSoundPlayer WaveDirectSoundPlayer { get; } = new();

        #endregion

        public PlayerCommandsBarViewModel()
        {
            WaveDirectSoundPlayer.PlayComplete += WaveDirectSoundPlayer_PlayComplete;

            Commands.PlayAlbumCommand = new DelegateCommand<Album>(PlayAlbum);
            Commands.LoadAlbumCommand = new DelegateCommand<Album>(LoadAlbum);

            //随便加载一个专辑
            Commands.LoadAlbumCommand.Execute(FakeDataCreator.CreateAlbums().FirstOrDefault());
        }

        #region 专辑/歌曲的加载和播放

        private void PlayAlbum(Album album)
        {
            Album = album;
            WaveDirectSoundPlayer.Play();
        }

        /// <summary>
        /// 加载专辑但不播放
        /// </summary>
        /// <param name="album"></param>
        private void LoadAlbum(Album album)
        {
            Album = album;
            
            _playHistory.AddLast((Album, Song));
            CurrentHistoryNode = _playHistory.Last;
        }

        private Album _album;

        /// <summary>
        /// 专辑
        /// </summary>
        public Album Album
        {
            get => _album;
            set
            {
                _album = value;
                Song   = _album.Songs.FirstOrDefault();
            }
        }


        private Song _song;

        /// <summary>
        /// 歌曲
        /// </summary>
        [DoNotCheckEquality]
        public Song Song
        {
            get => _song;
            set
            {
                _song = value;
                WaveDirectSoundPlayer.Stop();
                Debug.Print($" song 更改, 已要求 WaveDirectSoundPlayer.Stop  PlayStatus = {WaveDirectSoundPlayer.PlayStatus}");
                WaveDirectSoundPlayer.AudioFile = "";
                WaveDirectSoundPlayer.AudioFile = _song?.AudioFile ?? "";
            }
        }

        #endregion

        #region 播放命令

        private ICommand _playCommand;

        public ICommand PlayCommand =>
            _playCommand ??=
                new DelegateCommand(Play).ObservesCanExecute(() => WaveDirectSoundPlayer.CanPlay);

        private void Play()
        {
            WaveDirectSoundPlayer.Play();
        }

        #endregion

        #region 暂停命令

        private ICommand _pauseCommand;

        public ICommand PauseCommand =>
            _pauseCommand ??=
                new DelegateCommand(Pause).ObservesCanExecute(() => WaveDirectSoundPlayer.CanPause);

        private void Pause()
        {
            WaveDirectSoundPlayer.Pause();
        }

        #endregion

        #region 循环模式

        private DelegateCommand<LoopModes?> _setLoopModeCommand;

        public DelegateCommand<LoopModes?> SetLoopModeCommand => _setLoopModeCommand ??= new DelegateCommand<LoopModes?>(SetLoopMode);

        private void SetLoopMode(LoopModes? loopMode)
        {
            if (!loopMode.HasValue)
            {
                throw new ArgumentNullException(nameof(loopMode));
            }

            LoopMode = loopMode.Value;
        }


        public string LoopModeName =>
            LoopMode switch
            {
                LoopModes.InOrder    => "顺序播放",
                LoopModes.LoopList   => "列表循环",
                LoopModes.Random     => "随机播放",
                LoopModes.LoopSingle => "单曲循环",
                _                    => throw new ArgumentOutOfRangeException()
            };

        public LoopModes LoopMode { get; set; } = LoopModes.Random;

        #endregion

        #region 当前歌曲播放完成

        /// <summary>
        /// 当前歌曲播放完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WaveDirectSoundPlayer_PlayComplete(object    sender,
                                                        EventArgs e
        )
        {
            TryPlayNextSong();
        }

        #endregion

        #region 专辑/歌曲导航

        /// <summary>
        /// 播放历史记录
        /// </summary>
        private readonly LinkedList<(Album, Song)> _playHistory = new();

        /// <summary>
        /// 当前历史节点
        /// </summary>
        private LinkedListNode<(Album Album, Song Song)> CurrentHistoryNode { get; set; }

        private ICommand _playNextSongCommand;

        /// <summary>
        /// 播放下一首歌曲命令
        /// </summary>
        public ICommand PlayNextSongCommand => _playNextSongCommand ??= new DelegateCommand(TryPlayNextSong);

        private ICommand _playPreviousSongCommand;

        /// <summary>
        /// 播放上一曲命令
        /// </summary>
        public ICommand PlayPreviousSongCommand =>
            _playPreviousSongCommand ??=
                new DelegateCommand(PlayPreviousSong);


        private void PlayPreviousSong()
        {
            if (CurrentHistoryNode?.Previous is not null)
            {
                Album = CurrentHistoryNode.Previous.Value.Album;
                Song  = CurrentHistoryNode.Previous.Value.Song;

                if (WaveDirectSoundPlayer.CanPlay)
                {
                    WaveDirectSoundPlayer.Play();
                    CurrentHistoryNode = CurrentHistoryNode.Previous;
                }
            }

            if (CurrentHistoryNode is not null)
            {
                Album = CurrentHistoryNode.Value.Album;
                Song  = CurrentHistoryNode.Value.Song;

                if (WaveDirectSoundPlayer.CanPlay)
                {
                    WaveDirectSoundPlayer.Play();
                    CurrentHistoryNode = CurrentHistoryNode;
                }
            }
        }
        
        private void TryPlayNextSong()
        {
            //如果当前是在历史记录中,那么播放历史记录中的下一条
            if (CurrentHistoryNode?.Next is not null)
            {
                Album = CurrentHistoryNode.Next.Value.Album;
                Song  = CurrentHistoryNode.Next.Value.Song;

                if (WaveDirectSoundPlayer.CanPlay)
                {
                    WaveDirectSoundPlayer.Play();
                    CurrentHistoryNode = CurrentHistoryNode.Next;
                }

                return;
            }

            //到此说明当前已在历史记录的结尾,所以要确定新的播放歌曲

            //根据循环模式,选定下一曲
            switch (LoopMode)
            {
                case LoopModes.Random:
                    Song = Album.Songs.Shuffle().FirstOrDefault();

                    PlayAlbumSongAndAddHistory();

                    break;

                case LoopModes.InOrder:

                    var songIndex = Album.Songs.IndexOf(Song);

                    //按顺序播放下一曲
                    if (songIndex >= 0 && songIndex < Album.Songs.Count - 1)
                    {
                        Song = Album.Songs[songIndex + 1];


                        PlayAlbumSongAndAddHistory();
                    }

                    break;

                case LoopModes.LoopSingle:
                    //直接从头播放当前歌曲
                    WaveDirectSoundPlayer.Play();
                    break;

                case LoopModes.LoopList:
                    var songIndex2 = Album.Songs.IndexOf(Song);
                    if (songIndex2 == -1)
                    {
                        Song = Album.Songs.FirstOrDefault();

                        PlayAlbumSongAndAddHistory();
                    }
                    else if (songIndex2 == Album.Songs.Count - 1)
                    {
                        //已经到最后一首,那么回到第一首
                        Song = Album.Songs.FirstOrDefault();


                        PlayAlbumSongAndAddHistory();
                    }
                    else
                    {
                        //按顺序播放下一曲
                        Song = Album.Songs[songIndex2 + 1];

                        PlayAlbumSongAndAddHistory();
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// 播放当前专辑的当前歌曲,并添加到播放历史记录中
        /// </summary>
        private void PlayAlbumSongAndAddHistory()
        {
            if (WaveDirectSoundPlayer.CanPlay)
            {
                WaveDirectSoundPlayer.Play();
                _playHistory.AddLast((Album, Song));
                CurrentHistoryNode = _playHistory.Last;
            }
            else
            {
                throw new InvalidOperationException("尚未加载音频,当前无法播放.");
            }
        }

        #endregion
    }
}