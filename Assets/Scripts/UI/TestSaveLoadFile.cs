using UnityEngine;
using System.IO;
using SFB;
using UnityEngine.Events;

namespace UI
{
    public sealed class TestSaveLoadFile : MonoBehaviour
    {
        [SerializeField] private FileEditor fileEditor;

        [SerializeField] private UnityEvent<TestData> onLoad;
        [SerializeField] private UnityEvent<TestData> onSave;

        public void LoadFile()
        {
            string[] filePaths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "json", false);

            if (filePaths == null
                || filePaths.Length == 0)
                return;

            string json = File.ReadAllText(filePaths[0]);
            TestData loadedJson = JsonUtility.FromJson<TestData>(json);

            onLoad?.Invoke(loadedJson);
        }

        public void SaveFile()
        {
            string filePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "data", "json");

            if (string.IsNullOrEmpty(filePath))
                return;

            TestData data = fileEditor.GetData();
            string json = JsonUtility.ToJson(data, true);
            
            File.WriteAllText(filePath, json);

            onSave?.Invoke(data);
        }
    }
}