namespace PPMusic.Player.Enum
{
    public enum RecordState
    {
        /// <summary>
        /// 初始化
        /// </summary>
        None = 0,

        /// <summary>
        /// 准备中 应该弃用
        /// </summary>
        Prepare = 1,

        /// <summary>
        /// 正在录音
        /// </summary>
        Recording = 2,

        /// <summary>
        /// 录音完成
        /// </summary>
        Recorded = 3,

        /// <summary>
        /// 已取消
        /// </summary>
        Canceled
    }
}