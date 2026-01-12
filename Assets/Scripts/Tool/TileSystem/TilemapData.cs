using System;

namespace Tool.TileSystem
{
    [Serializable]
    public sealed class TilemapData : BaseData
    {
        public int rows;
        public int cols;
        public string[] tileId;
    }
}