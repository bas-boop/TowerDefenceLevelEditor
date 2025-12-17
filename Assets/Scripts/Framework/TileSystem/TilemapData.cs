using System;

namespace Framework.TileSystem
{
    [Serializable]
    public sealed class TilemapData
    {
        //todo: needs more data; json file type, version, ect.
        public string name = "file";
        
        public int rows;
        public int cols;
        public int[] tileId;
    }
}