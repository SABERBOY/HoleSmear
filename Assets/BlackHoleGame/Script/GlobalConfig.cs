using System.Collections.Generic;

namespace BlackHoleGame.Script
{
    /// <summary>
    /// Global  Config
    /// </summary>
    public class GlobalConfig
    {
        /// <summary>
        /// 场景皮肤解锁列表
        /// </summary>
        public static readonly List<int> SkinCastMoneyList = new List<int> { 1000, 2500, 3800, 4900, 6000 };

        /// <summary>
        /// 复活时间
        /// </summary>
        public static readonly int RevivalTime = 5;
    }
}