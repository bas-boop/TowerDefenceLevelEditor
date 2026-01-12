using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Tool;
using Tool.TileSystem;

namespace UI
{
    public sealed class TileButtoner : MonoBehaviour
    {
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private Transform buttonsParent;
        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private TMP_InputField inputField;

        [SerializeField] private string tileName;
        [SerializeField] private Color tileColor;
        
        private readonly Dictionary<string, Button> _buttonCache = new ();

        public void AddNewButton()
        {
            tileName = inputField.text;
            tileColor = colorPicker.GetSelectedColor();
            
            CreateButton(tileName, tileColor);
        }

        public void AddSetupButtons(TileDatas data)
        {
            for (int i = 0; i < data.tileNames.Length; i++)
            {
                if (_buttonCache.ContainsKey(data.tileNames[i]))
                    continue;
                
                CreateButton(data.tileNames[i], data.tileColors[i]);
            }
        }

        private void CreateButton(string targetName, Color targetColor)
        {
            if (!TileDataHolder.Instance.CreateData(targetName, targetColor)
                && _buttonCache.ContainsKey(targetName))
                return;
            
            Button b = Instantiate(buttonPrefab, buttonsParent);
            b.GetComponentInChildren<TMP_Text>().text = targetName;
            b.onClick.AddListener(() => ButtonEvent(targetName));
            
            _buttonCache.Add(targetName, b);
        }

        
        private void ButtonEvent(string targetName)
        {
            Debug.Log(targetName);
            ToolData.Instance.SetData(targetName);
        }
    }
}