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
    public sealed class TestSaveLoadFile : MonoBehaviour
    {
        [SerializeField] private FileEditor fileEditor;

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
            
            onLoad?.Invoke(loadedJson);
        }

        public void SaveFile()
        {
            string filePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "data", "json");

            if (string.IsNullOrEmpty(filePath))
                return;

            TilemapData data = fileEditor.GetData();
            string json = JsonUtility.ToJson(data, true);
            
            File.WriteAllText(filePath, json);

            onSave?.Invoke(data);
        }
    }
}