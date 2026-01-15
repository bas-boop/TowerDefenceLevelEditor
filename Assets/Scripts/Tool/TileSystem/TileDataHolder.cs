using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Framework;
using Tool.FileSystem;

namespace Tool.TileSystem
{
    public sealed class TileDataHolder : Singleton<TileDataHolder>
    {
        [SerializeField] private FileEditor fileEditor;
        [SerializeField] private List<TileData> tilesDatas;

        private TileDatas _tileDatas;

        private void Start()
        {
            fileEditor.LoadTileDatasFile();
        }

        public bool CreateData(string targetName, Color targetColor)
        {
            if (tilesDatas.FirstOrDefault(tileData => tileData.tileName == targetName))
            {
                Debug.LogWarning("Tilename already in use");
                return false;
            }
            
            TileData newTileData = ScriptableObject.CreateInstance<TileData>();
            newTileData.name = targetName;
            newTileData.tileName = targetName;
            newTileData.tileColor = targetColor;
            tilesDatas.Add(newTileData);

            ConvertData();
            fileEditor.SaveTileDatas(_tileDatas);
            
            return true;
        }

        public bool EditData(string targetName, Color targetColor)
        {
            TileData existingTileData = GetData(targetName);

            if (existingTileData == null)
            {
                Debug.LogWarning("Tilename is not in use");
                return false;
            }

            existingTileData.tileColor = targetColor;

            ConvertData();
            fileEditor.SaveTileDatas(_tileDatas);

            return true;
        }

        public TileData GetData(string targetName)
        {
            return tilesDatas.FirstOrDefault(tileData => tileData.tileName == targetName);
        }

        public TileDatas GetAllData()
        {
            ConvertData();
            return _tileDatas;
        }

        public void Setup(TileDatas data)
        {
            if (data == null)
            {
                Debug.LogWarning("Setup called with null TileDatas.");
                return;
            }

            tilesDatas ??= new ();

            int count = Mathf.Min(data.tileNames.Length, data.tileColors.Length);

            for (int i = 0; i < count; i++)
            {
                string name = data.tileNames[i];
                Color color = data.tileColors[i];

                if (string.IsNullOrWhiteSpace(name))
                    continue;

                // Skip if already exists
                if (tilesDatas.Any(t => t.tileName == name))
                    continue;

                CreateData(name, color);
            }
        }


        private void ConvertData()
        {
            if (tilesDatas == null
                || tilesDatas.Count == 0)
            {
                Debug.LogWarning("No tile data to convert.");
                return;
            }

            if (tilesDatas.Any(t => t == null))
            {
                Debug.LogError("TileData list contains null entries.");
                return;
            }

            if (tilesDatas.GroupBy(t => t.tileName).Any(g => g.Count() > 1))
            {
                Debug.LogError("Duplicate tile names detected. Aborting conversion.");
                return;
            }

            string[] names = tilesDatas.Select(t => t.tileName).ToArray();
            Color[] colors = tilesDatas.Select(t => t.tileColor).ToArray();

            _tileDatas = new (names, colors);
        }
    }
}