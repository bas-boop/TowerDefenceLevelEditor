// standard
using System.IO;
using UnityEngine;
using UnityEngine.Events;

// third party
using SFB;

// mine
using Framework.TileSystem;

namespace UI
{
    public sealed class FileEditor : MonoBehaviour
    {
        [SerializeField] private TileMap tileMap;

        [SerializeField] private UnityEvent<TilemapData> onLoad;
        [SerializeField] private UnityEvent<TilemapData> onSave;

        public void LoadFile()
        {
            string[] filePaths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false);

            if (filePaths == null
                || filePaths.Length == 0)
                return;

            string json = File.ReadAllText(filePaths[0]);
            
            TilemapData loadedJson = JsonUtility.FromJson<TilemapData>(json);

            if (!IsValidTilemapFile(loadedJson))
            {
                Debug.LogError("Invalid JSON file.");
                return;
            }

            Debug.Log("Valid TDLE file loaded!");
            onLoad?.Invoke(loadedJson);
        }

        public void SaveFile()
        {
            string filePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "data", "json");

            if (string.IsNullOrEmpty(filePath))
                return;

            TilemapData data = tileMap.GetData();
            string json = JsonUtility.ToJson(data, true);
            
            File.WriteAllText(filePath, json);

            onSave?.Invoke(data);
        }
        
        private bool IsValidTilemapFile(TilemapData data)
        {
            if (data == null)
                return false;

            if (data.identifier != "TDLE")
                return false;

            if (string.IsNullOrEmpty(data.version))
                return false;

            if (data.rows <= 0 || data.cols <= 0)
                return false;

            if (data.tileId == null || data.tileId.Length == 0)
                return false;

            return true;
        }

    }
}