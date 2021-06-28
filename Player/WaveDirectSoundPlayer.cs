using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using JetBrains.Annotations;
using NAudio.Wave;
using PPMusic.Player.Enum;

namespace PPMusic.Player
{
    /// <summary>
    /// 对 NAudio 的基础类进行包装而成的音频播放器
    /// </summary>
    public class WaveDirectSoundPlayer : INotifyPropertyChanged, IDisposable
    {
        public WaveDirectSoundPlayer()
        {
            _timer.Elapsed += _timer_Elapsed;
        }

        private readonly Timer _timer = new(200);

        private void _timer_Elapsed(object           sender,
                                    ElapsedEventArgs e
        )
        {
            OnPropertyChanged(nameof(CurrentTime));
        }


        #region 播放核心

        private DirectSoundOut _directSoundOut;

        /// <summary>
        /// 播放核心
        /// </summary>
        private DirectSoundOut DirectSoundOut
        {
            get => _directSoundOut;
            set
            {
                _directSoundOut = value;
                UpdatePlayStatus();
            }
        }

        #endregion

        #region AudioFile

        public event EventHandler AudioFileChanged;

        private string _audioFile;


        /// <summary>
        /// 音频文件,如果在播放途中更改,那么会导致播放停止.再次执行Play方法时会播放新设定的文件
        /// </summary>
        public string AudioFile
        {
            get => _audioFile;
            set
            {
                if (_audioFile == value)
                {
                    return;
                }

                Stop();

                _audioFile = value;

                //一旦设置了新的有效的音频文件,那么要立刻创建对应的 AudioFileReader 对象,否则很多数据无法立刻提供,比如音频时长等.
                FreeAudioFileReader();
                InitAudioFileReader();

                AudioFileChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region NAudio 提供的音频文件读取器

        private void InitAudioFileReader()
        {
            if (!string.IsNullOrWhiteSpace(AudioFile))
            {
                if (File.Exists(AudioFile))
                {
                    AudioFileReader = new AudioFileReader(AudioFile)
                                      {
                                          Volume = IsMute ? 0 : Volume
                                      };
                }
                else
                {
                    throw new FileNotFoundException("指定的音频文件不存在.", AudioFile);
                }
            }
        }

        private void FreeAudioFileReader()
        {
            if (AudioFileReader == null)
            {
                return;
            }

            AudioFileReader.Dispose();
            AudioFileReader = null;
        }

        private AudioFileReader _audioFileReader;

        private AudioFileReader AudioFileReader
        {
            get => _audioFileReader;
            set
            {
                if (!ReferenceEquals(_audioFileReader, value))
                {
                    _audioFileReader = value;

                    if (DirectSoundOut != null)
                    {
                        FreeDirectSoundOut();
                    }
                    else
                    {
                        UpdatePlayStatus();
                    }
                }
            }
        }

        #endregion

        #region 播放设备标识

        /// <summary>
        /// 播放设备标识,默认 Guid.Empty ,表示使用系统默认的播放设备
        /// </summary>
        public Guid DeviceGuid { get; set; }

        #endregion


        #region 定位

        /// <summary>
        /// 音频总时长
        /// </summary>
        public TimeSpan TotalTime => AudioFileReader?.TotalTime ?? TimeSpan.Zero;

        /// <summary>
        /// 当前时间
        /// </summary>
        public TimeSpan CurrentTime
        {
            get
            {
                if (DirectSoundOut == null || AudioFileReader == null)
                {
                    return TimeSpan.Zero;
                }

                //此处不能使用 DirectSoundOut.GetPosition , 因为可能未实现该接口

                return (DirectSoundOut.PlaybackState == PlaybackState.Stopped) ? TimeSpan.Zero : AudioFileReader.CurrentTime;
            }
            set => SetPositionInMilliseconds(value.TotalMilliseconds);
        }


        public double GetLengthInMilliseconds()
        {
            if (AudioFileReader == null)
            {
                throw new ApplicationException("尚未打开音频文件,无法获取长度.");
            }

            return TimeSpan
                  .FromSeconds(AudioFileReader.Length / (double) AudioFileReader.WaveFormat.AverageBytesPerSecond)
                  .TotalMilliseconds;
        }

        public double GetPositionInMilliseconds()
        {
            if (DirectSoundOut == null || AudioFileReader == null)
            {
                return 0;
            }

            return TimeSpan
                  .FromSeconds(DirectSoundOut.GetPosition() / (double) AudioFileReader.WaveFormat.AverageBytesPerSecond)
                  .TotalMilliseconds;
        }

        public void SetPositionInMilliseconds(double milliseconds)
        {
            if (AudioFileReader == null)
            {
                throw new ApplicationException("尚未打开音频文件,无法设置位置.");
            }

            AudioFileReader.Position = (long) (AudioFileReader.WaveFormat.AverageBytesPerSecond * milliseconds / 1000d);
        }

        #endregion


        #region 播放状态

        public event EventHandler PlayStatusChanged;

        /// <summary>
        /// 播放状态
        /// </summary>
        public PlayStatus PlayStatus { get; private set; }


        private void UpdatePlayStatus()
        {
            if (AudioFileReader == null)
            {
                PlayStatus = PlayStatus.None;
                return;
            }

            if (DirectSoundOut == null)
            {
                PlayStatus = PlayStatus.Stopped;
                return;
            }

            PlayStatus = DirectSoundOut.PlaybackState switch
                         {
                             PlaybackState.Playing => PlayStatus.Playing,
                             PlaybackState.Paused  => PlayStatus.Paused,
                             PlaybackState.Stopped => PlayStatus.Stopped,
                             _                     => throw new ArgumentOutOfRangeException()
                         };
        }

        #endregion

        #region 音量

        public event EventHandler VolumeChanged;

        private bool _isMute;

        /// <summary>
        /// 静音
        /// </summary>
        public bool IsMute
        {
            get => _isMute;
            set
            {
                _isMute = value;

                if (AudioFileReader == null) return;

                if (_isMute)
                {
                    if (AudioFileReader.Volume != 0f)
                    {
                        AudioFileReader.Volume = 0f;
                    }
                }
                else
                {
                    if (Math.Abs(AudioFileReader.Volume - Volume) > 0.0001)
                    {
                        AudioFileReader.Volume = Volume;
                    }
                }
            }
        }


        private float _volume = 1f;

        public float Volume
        {
            get => _volume;
            set
            {
                if (value is > 1f or < 0f)
                {
                    throw new ArgumentOutOfRangeException(nameof(Volume), $"值{value}超出了音量值的取值范围为0~1.");
                }

                _volume = value;

                if (AudioFileReader != null)
                {
                    AudioFileReader.Volume = _volume;
                }

                IsMute = _volume == 0f;

                VolumeChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        #endregion

        #region 获取系统所有声音播放设备

        /// <summary>
        /// 获取当前系统中能够进行声音输出的设备编号和名称
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> InitDevice()
        {
            var result = new Dictionary<int, string>();
            for (var deviceId = 0; deviceId < WaveOut.DeviceCount; deviceId++)
            {
                var capabilities = WaveOut.GetCapabilities(deviceId);

                result.Add(deviceId, capabilities.ProductName);
            }

            return result;
        }

        #endregion

        #region Play

        public event EventHandler PlayStarted;

        public event EventHandler PlayComplete;

        public bool CanPlay =>
            !string.IsNullOrWhiteSpace(AudioFile) &&
            AudioFileReader != null               &&
            PlayStatus      != PlayStatus.Playing;

        public void GetReadyToPlay()
        {
        }

        /// <summary>
        /// 从头开始播放.
        /// </summary>
        public void Play()
        {
            if (string.IsNullOrWhiteSpace(AudioFile))
            {
                return;
            }

            FreeDirectSoundOut();
            FreeAudioFileReader();

            InitAudioFileReader();
            InitDirectSoundOut();

            DirectSoundOut.Play();


            _timer.AutoReset = true;
            _timer.Start();

            UpdatePlayStatus();
            PlayStatusChanged?.Invoke(this, EventArgs.Empty);
            PlayStarted?.Invoke(this, EventArgs.Empty);
        }


        private void InitDirectSoundOut()
        {
            DirectSoundOut?.Dispose();

            if (DeviceGuid != Guid.Empty)
            {
                //确认指定的设备 Guid 是可用的,如果不可用,那么还是使用系统默认设备
                var device = DirectSoundOutManager.PlayDevices.FirstOrDefault(v => v.Guid == DeviceGuid);

                DeviceGuid = device?.Guid ?? Guid.Empty;
            }

            DirectSoundOut = DeviceGuid == Guid.Empty ? new DirectSoundOut() : new DirectSoundOut(DeviceGuid);

            DirectSoundOut.Init(AudioFileReader);

            DirectSoundOut.PlaybackStopped += OnPlaybackStopped;
        }

        #endregion

        #region Pause

        public event EventHandler Paused;

        public bool CanPause =>
            DirectSoundOut != null &&
            PlayStatus     == PlayStatus.Playing;

        public void Pause()
        {
            if (DirectSoundOut != null)
            {
                if (DirectSoundOut.PlaybackState == PlaybackState.Playing)
                {
                    DirectSoundOut.Pause();

                    _timer.Stop();
                    UpdatePlayStatus();
                    PlayStatusChanged?.Invoke(this, EventArgs.Empty);
                    Paused?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion


        #region Resume

        public event EventHandler Resumed;

        public bool CanResume =>
            DirectSoundOut != null &&
            PlayStatus     == PlayStatus.Paused;

        public void Resume()
        {
            if (DirectSoundOut != null)
            {
                if (DirectSoundOut.PlaybackState == PlaybackState.Paused)
                {
                    DirectSoundOut.Play();

                    _timer.AutoReset = true;
                    _timer.Start();
                    UpdatePlayStatus();
                    PlayStatusChanged?.Invoke(this, EventArgs.Empty);
                    Resumed?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        #endregion


        #region Stop

        public event EventHandler Stopped;

        public bool CanStop =>
            DirectSoundOut != null               &&
            PlayStatus     != PlayStatus.Stopped &&
            PlayStatus     != PlayStatus.None;

        public void Stop()
        {
            if (DirectSoundOut == null)
            {
                return;
            }

            if (DirectSoundOut.PlaybackState != PlaybackState.Stopped)
            {
                DirectSoundOut.Stop();
                Stopped?.Invoke(this, EventArgs.Empty);
            }
        }


        private void OnPlaybackStopped(object           sender,
                                       StoppedEventArgs e
        )
        {
            //只有在播放完全部的音频时才触发 PlayComplete
            if (AudioFileReader.Length == AudioFileReader.Position)
            {
                PlayComplete?.Invoke(this, EventArgs.Empty);
            }


            _timer.Stop();
            UpdatePlayStatus();
            PlayStatusChanged?.Invoke(this, EventArgs.Empty);

            FreeDirectSoundOut();
        }

        #endregion

        #region 销毁

        public void Dispose()
        {
            FreeDirectSoundOut();
            FreeAudioFileReader();
        }

        private void FreeDirectSoundOut()
        {
            if (DirectSoundOut == null)
            {
                return;
            }

            if (DirectSoundOut.PlaybackState != PlaybackState.Stopped)
            {
                DirectSoundOut.Stop();
            }

            DirectSoundOut.PlaybackStopped -= OnPlaybackStopped;
            DirectSoundOut.Dispose();
            DirectSoundOut = null;
        }

        #endregion 销毁

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}