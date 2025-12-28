using System;

namespace Framework.TileSystem
{
    [Serializable]
    public sealed class TilemapData
    {
        public string jsonFile;
        public string version;
        
        public int rows;
        public int cols;
        public int[] tileId;
    }
}