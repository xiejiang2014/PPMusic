using System.Windows;
using PPMusic.ViewModel.PlayerCommandsBar;
using Unity;

namespace PPMusic.View.PlayerCommandsBar
{
    /// <summary>
    /// PlayerCommandsBar.xaml 的交互逻辑
    /// </summary>
    public partial class PlayerCommandsBar
    {
        public PlayerCommandsBar()
        {
            InitializeComponent();
        }

        [InjectionConstructor]
        public PlayerCommandsBar(PlayerCommandsBarViewModel playerCommandsBarViewModel)
        {
            InitializeComponent();
            DataContext = playerCommandsBarViewModel;
        }

        #region ShowCoverButton

        public bool IsShowCoverButton
        {
            get => (bool) GetValue(IsShowCoverButtonProperty);
            set => SetValue(IsShowCoverButtonProperty, value);
        }

        public static readonly DependencyProperty IsShowCoverButtonProperty =
            DependencyProperty.Register(
                                        "IsShowCoverButton",
                                        typeof(bool),
                                        typeof(PlayerCommandsBar),
                                        new PropertyMetadata(true, ShowCoverButtonPropertyChanged)
                                       );

        private static void ShowCoverButtonPropertyChanged(DependencyObject                   d,
                                                           DependencyPropertyChangedEventArgs e
        )
        {
            if (d is PlayerCommandsBar thisPlayerCommandsBar &&
                e.OldValue is bool oldValue                  &&
                e.NewValue is bool newValue)
            {
            }
        }

        #endregion
    }
}