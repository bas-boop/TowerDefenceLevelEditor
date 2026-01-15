using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

using Framework.Command;
using Tool;
using Tool.TileSystem;

namespace UI
{
    public sealed class TileButtoner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private Transform buttonsParent;
        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private TMP_InputField inputField;

        [Header("Runtime")]
        [SerializeField] private string tileName;
        [SerializeField] private Color tileColor;

        [Header("Event's")]
        [SerializeField] private UnityEvent<string> onAdd = new();
        [SerializeField] private UnityEvent<string> onRemove = new();
        [SerializeField] private UnityEvent<string, Color> onEdit = new();
        
        private readonly Dictionary<string, Button> _buttonCache = new ();
        private string _buttonNameToEdit;

        public void AddNewButton()
        {
            tileName = inputField.text;
            tileColor = colorPicker.GetSelectedColor();
    
            if (tileName is "" or null)
                return;

            CommandSystem.Instance.Execute(
                new AddTileButtonCommand(tileName, tileColor, onAdd)
            );
        }

        public void EditButton()
        {
            tileColor = colorPicker.GetSelectedColor();
            Color oldColor = TileDataHolder.Instance.GetData(_buttonNameToEdit).tileColor;
    
            CommandSystem.Instance.Execute(
                new EditTileButtonCommand(_buttonNameToEdit, oldColor, tileColor, onEdit)
            );
        }

        public void DeleteButton()
        {
            if (!_buttonCache.ContainsKey(_buttonNameToEdit))
                return;
    
            Color currentColor = TileDataHolder.Instance.GetData(_buttonNameToEdit).tileColor;
            
            CommandSystem.Instance.Execute(
                new DeleteTileButtonCommand(_buttonNameToEdit, currentColor, onRemove, onAdd)
            );
        }

        public void SetButtonToEdit(string targetButton)
        {
            if (_buttonCache.ContainsKey(targetButton))
                _buttonNameToEdit = targetButton;
        }

        public void AddSetupButtons(TileDatas data)
        {
            foreach (string currentName in data.tileNames)
            {
                if (_buttonCache.ContainsKey(currentName))
                    continue;
                
                CreateButton(currentName);
            }
        }

        public void CreateButton()
        {
            tileName = inputField.text;
            CreateButton(tileName);
        }

        private void CreateButton(string targetName)
        {
            if (_buttonCache.ContainsKey(targetName))
                return;
            
            Button b = Instantiate(buttonPrefab, buttonsParent);
            b.name = $"TileButton{targetName}";
            b.GetComponentInChildren<TMP_Text>().text = targetName;
            b.onClick.AddListener(() => ButtonEvent(targetName));
            
            _buttonCache.Add(targetName, b);
        }

        public void RemoveButton()
        {
            tileName = inputField.text;

            if (!_buttonCache.TryGetValue(tileName, out Button button))
                return;
            
            Destroy(button.gameObject);
            _buttonCache.Remove(tileName);
        }

        public void UpdateButtonColor()
        {
            tileName = inputField.text;
            tileColor = colorPicker.GetSelectedColor();

            if (!_buttonCache.TryGetValue(tileName, out Button button))
                return;
            
            ColorBlock colors = button.colors;
            colors.normalColor = tileColor;
            button.colors = colors;
        }
        
        private void ButtonEvent(string targetName) => ToolData.Instance.SetData(targetName);
    }
}