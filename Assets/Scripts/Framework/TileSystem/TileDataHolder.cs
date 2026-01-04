using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Framework.TileSystem
{
    public sealed class TileDataHolder : Singleton<TileDataHolder>
    {
        [SerializeField] private List<TileData> tilesDatas;

        public bool CreateData(string targetName, Color targetColor)
        {
            if (tilesDatas.FirstOrDefault(tileData => tileData.tileName == targetName))
            {
                Debug.LogError("Tilename already in use");
                return false;
            }
            
            TileData newTileData = ScriptableObject.CreateInstance<TileData>();
            newTileData.name = targetName;
            newTileData.tileName = targetName;
            newTileData.tileColor = targetColor;
            tilesDatas.Add(newTileData);

            return true;
        }

        public TileData GetData(string targetName)
        {
            return tilesDatas.FirstOrDefault(tileData => tileData.tileName == targetName);
        }
    }
}