// standard
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

// third party
using SFB;

// mine
using Tool.TileSystem;

namespace Tool.FileSystem
{
    public sealed class FileEditor : MonoBehaviour
    {
        private const string TILES_DATA = "/Framework.TileSystem.TileDatas.json";
        
        [SerializeField] private TileMap tileMap;

        // todo: should rename
        [SerializeField] private UnityEvent<TilemapData> onLoad;
        [SerializeField] private UnityEvent<TilemapData> onSave;
        
        [SerializeField] private UnityEvent<TileDatas> onLoadTiledatas;
        [SerializeField] private UnityEvent<TileDatas> onSaveTiledatas;
        
        public void LoadTilemapDataFile()
        {
            (bool isSuccessful, BaseData baseData) = LoadFile<TilemapData>();
            
            if (isSuccessful)
                onLoad?.Invoke(baseData as TilemapData);
        }

        public void LoadTileDatasFile()
        {
            (bool isSuccessful, BaseData baseData) = LoadFile<TileDatas>(true);
            
            if (isSuccessful)
                onLoadTiledatas?.Invoke(baseData as TileDatas);
        }

        public void SaveTilemapData()
        {
            TilemapData data = tileMap.GetData();
            SaveFile(data);
            onSave?.Invoke(data);
        }

        public void SaveTileDatas(TileDatas data)
        {
            SaveFile(data, true);
            onSaveTiledatas?.Invoke(data);
        }

        private void SaveFile<T>(T dataToSave, bool usePersistentDataPath = false) where T : BaseData
        {
            Debug.Log(Application.persistentDataPath);
            
            string filePath = usePersistentDataPath
                ? Application.persistentDataPath + TILES_DATA
                : StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, typeof(T).ToString(), "json");

            if (string.IsNullOrEmpty(filePath))
                return;

            string json = JsonUtility.ToJson(dataToSave, true);
            
            File.WriteAllText(filePath, json);
        }
        
        private (bool, BaseData) LoadFile<T>(bool usePersistentDataPath = false) where T : BaseData
        {
            List<string> filePaths = new ();

            if (usePersistentDataPath)
                filePaths.Add(Application.persistentDataPath + TILES_DATA);
            else
                filePaths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false).ToList();

            if (filePaths.Count == 0
                || !File.Exists(filePaths[0]))
                return (false, null);

            string json = File.ReadAllText(filePaths[0]);
            
            T loadedJson = JsonUtility.FromJson<T>(json);

            if (!IsValidFile(loadedJson))
            {
                Debug.LogError("Invalid JSON file.");
                return (false, null);
            }

            Debug.Log("Valid TDLE file loaded!");
            return (true, loadedJson);
        }
        
        // todo: this should be a abstract function in each data type
        private bool IsValidFile(BaseData data)
        {
            if (data == null)
                return false;

            if (data.identifier != "TDLE")
                return false;

            if (string.IsNullOrEmpty(data.version))
                return false;

            return true;
        }

    }
}