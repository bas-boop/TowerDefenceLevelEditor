// standard
using System.IO;
using UnityEngine;
using UnityEngine.Events;

// third party
using SFB;

// mine
using Framework.TileSystem;

namespace Framework.FileSystem
{
    public sealed class FileEditor : MonoBehaviour
    {
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
            (bool isSuccessful, BaseData baseData) = LoadFile<TileDatas>();
            
            if (isSuccessful)
                onLoadTiledatas?.Invoke(baseData as TileDatas);
        }

        public void SaveTilemapData()
        {
            TilemapData data = tileMap.GetData();
            SaveFile2(data);
            onSave?.Invoke(data);
        }

        public void SaveTileDatas(TileDatas data)
        {
            // should be saved somewhere and retrieved
            // todo: fix
            
            //TileDatas data = new ();
            SaveFile2(data);
            onSaveTiledatas?.Invoke(data);
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

        private void SaveFile2<T>(T dataToSave) where T : BaseData
        {
            string filePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, typeof(T).ToString(), "json");

            if (string.IsNullOrEmpty(filePath))
                return;

            string json = JsonUtility.ToJson(dataToSave, true);
            
            File.WriteAllText(filePath, json);
        }
        
        private (bool, BaseData) LoadFile<T>() where T : BaseData
        {
            string[] filePaths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false);

            if (filePaths == null
                || filePaths.Length == 0)
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
        
        private bool IsValidFile(BaseData data)
        {
            if (data == null)
                return false;

            if (data.identifier != "TDLE")
                return false;

            if (string.IsNullOrEmpty(data.version))
                return false;

            // old, before <T> with load and save
            /*if (data.rows <= 0 || data.cols <= 0)
                return false;

            if (data.tileId == null || data.tileId.Length == 0)
                return false;*/

            return true;
        }

    }
}