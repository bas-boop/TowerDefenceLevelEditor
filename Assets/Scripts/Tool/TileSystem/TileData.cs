using UnityEngine;

namespace Tool.TileSystem
{
    [CreateAssetMenu(fileName = "NewTileData", menuName = "TDLE", order = 0)]
    public sealed class TileData : ScriptableObject
    {
        public string tileName;
        public Color tileColor;
    }
}