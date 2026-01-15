using System;
using UnityEngine;

namespace Tool.TileSystem
{
    [Serializable]
    public sealed class TileDatas : BaseData
    {
        public string[] tileNames;
        public Color[] tileColors;

        public TileDatas(string[] targetNames, Color[] targetColors)
        {
            identifier = "TDLE";
            version = "1.0";
            tileNames = targetNames;
            tileColors = targetColors;
        }
    }
}