using System.ComponentModel;

namespace PPMusic.Player.Enum
{
    public enum LoopModes
    {
        [Description("随机播放")] Random,
        [Description("顺序播放")] InOrder,
        [Description("单曲循环")] LoopSingle,
        [Description("列表循环")] LoopList,
    }
}