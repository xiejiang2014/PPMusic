using System.ComponentModel;

namespace PPMusic.Player.Enum
{
    public enum OrderModes
    {
        [Description("随机播放")] Ran,
        [Description("顺序播放")] InOrder,
        [Description("单曲循环")] LoopSingle,
        [Description("列表循环")] LoopList,
    }
}