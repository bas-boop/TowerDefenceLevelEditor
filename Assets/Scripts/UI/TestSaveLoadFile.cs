using System;
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
            string[] filePaths = StandaloneFileBrowser.OpenFilePanel("Open File", "", ".json", false);
            //string json = File.ReadAllText(filePaths[0]);
            
            TestData loadedJson = JsonUtility.FromJson<TestData>(filePaths[0]);
            onLoad?.Invoke(loadedJson);
        }

        public void SaveFile()
        {
            string filePath = StandaloneFileBrowser.SaveFilePanel("Save File", Application.dataPath, "", ".json");
            
            string save = JsonUtility.ToJson(fileEditor.GetData());
            File.WriteAllText(filePath, save);
        }
    }
}