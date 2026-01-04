using System;

namespace Framework.TileSystem
{
    [Serializable]
    public sealed class TilemapData
    {
        public string identifier;
        public string version;
        
        public int rows;
        public int cols;
        public string[] tileId;
    }
}