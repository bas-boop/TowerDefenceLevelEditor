using System.Collections.Generic;
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

            if (filePaths == null || filePaths.Length == 0)
                return;

            string json = File.ReadAllText(filePaths[0]);
            Debug.Log($"Loaded JSON:\n{json}");

            TestData loadedJson = JsonUtility.FromJson<TestData>(json);
            onLoad?.Invoke(loadedJson);
        }

        public void SaveFile()
        {
            string filePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "data", "json");

            if (string.IsNullOrEmpty(filePath))
                return;

            TestData data = fileEditor.GetData();
            Debug.Log(data.numbers);

            string debug = null;
            for (int i = 0; i < data.rows; i++)
            {
                for (int j = 0; j < data.cols; j++)
                {
                    debug += $" {data.numbers[i * j]}";
                }
            }

            Debug.Log(debug);
            
            string json = JsonUtility.ToJson(data, true);
            Debug.Log(json);
            File.WriteAllText(filePath, json);

            onSave?.Invoke(data);
        }
    }
}