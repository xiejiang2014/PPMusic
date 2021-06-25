namespace PPMusic.Player.Enum
{
    public enum PlayStatus
    {
        /// <summary>
        /// 尚未加载需要播放的文件
        /// </summary>
        None,

        /// <summary>
        /// 已经加载了需要播放的文件,但当前没有播放
        /// </summary>
        Stopped,

        /// <summary>
        /// 正在播放中
        /// </summary>
        Playing,

        /// <summary>
        /// 已经暂停
        /// </summary>
        Paused
    }
}