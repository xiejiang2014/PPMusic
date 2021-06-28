using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;
using PPMusic.Model;
using PPMusic.Player.Enum;
using Prism.Commands;

namespace PPMusic.Player
{
    /// <summary>
    /// PlayerCommandsBar.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerCommandsBar : INotifyPropertyChanged
    {
        public PlayerCommandsBar()
        {
            InitializeComponent();

            Commands.PlayAlbumCommand = new DelegateCommand<Album>(PlayAlbum);
            Commands.LoadAlbumCommand = new DelegateCommand<Album>(LoadAlbum);
        }


        #region 当前播放的专辑

        private void PlayAlbum(Album album)
        {
            LoadAlbum(album);
            WaveDirectSoundPlayer.Play();
        }

        private void LoadAlbum(Album album)
        {
            Album                           = album;
            WaveDirectSoundPlayer.AudioFile = album.Songs.FirstOrDefault()?.AudioFile;
        }

        public Album Album { get; set; }

        #endregion


        #region 播放核心

        public WaveDirectSoundPlayer WaveDirectSoundPlayer { get; } = new();

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


        #region 静音命令

        private ICommand _muteCommand;

        public ICommand MuteCommand =>
            _muteCommand ??=
                new DelegateCommand(Pause).ObservesCanExecute(() => WaveDirectSoundPlayer.CanPause);

        private void Mute()
        {
            WaveDirectSoundPlayer.IsMute = true;
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

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}