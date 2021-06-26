using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using PPMusic.Player.Enum;

namespace PPMusic.Player
{
    /// <summary>
    /// Player.xaml 的交互逻辑
    /// </summary>
    public partial class Player : INotifyPropertyChanged
    {
        public Player()
        {
            InitializeComponent();
        }

        public string OrderModeName =>
            OrderMode switch
            {
                OrderModes.InOrder => "顺序播放",
                OrderModes.LoopList => "LoopList",
                OrderModes.Ran => "随机播放",
                OrderModes.LoopSingle => "单曲循环",
                _ => throw new ArgumentOutOfRangeException()
            };

        public OrderModes OrderMode { get; set; } = OrderModes.Ran;


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