using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using Tool.TileSystem;

namespace UI
{
    public sealed class TileEditorList : MonoBehaviour
    {
        [SerializeField] private GameObject tileButton;
        [SerializeField] private Transform parent;
        
        [Space]
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private TileButtoner tileButtoner;

        private readonly Dictionary<string, Button> _buttons = new(); 

        private void Start()
        {
            TileDataHolder holder = TileDataHolder.Instance;
            
            if (holder == null)
                return;

            TileDatas tileDatas = holder.GetAllData();

            for (int i = 0; i < tileDatas.tileNames.Length; i++)
                AddButtonInternal(tileDatas.tileNames[i], tileDatas.tileColors[i]);
        }
        
        public void AddButton(string buttonName)
        {
            Color color = TileDataHolder.Instance.GetData(buttonName).tileColor;
            AddButtonInternal(buttonName, color);
        }

        private void AddButtonInternal(string buttonName, Color color)
        {
            if (_buttons.ContainsKey(buttonName))
                return;

            GameObject buttonObject = Instantiate(tileButton, parent);
            Button currentButton = buttonObject.GetComponent<Button>();

            TMP_Text text = currentButton.GetComponentInChildren<TMP_Text>();
            text.text = buttonName;

            currentButton.onClick.AddListener(() => SelectButton(buttonName, color));
            currentButton.colors = SetButtonColors(currentButton.colors, color);

            _buttons.Add(buttonName, currentButton);
        }

        public void RemoveButton(string buttonName)
        {
            if (!_buttons.TryGetValue(buttonName, out Button b))
                return;

            _buttons.Remove(buttonName);
            Destroy(b.gameObject);
        }

        public void UpdateButtonColor(string buttonName, Color newColor)
        {
            if (!_buttons.TryGetValue(buttonName, out Button button))
                return;

            button.colors = SetButtonColors(button.colors, newColor);
            
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => SelectButton(buttonName, newColor));
        }
        
        private void SelectButton(string targetName, Color targetColor)
        {
            nameInput.text = targetName;
            colorPicker.SetSelectedColor(targetColor);
            tileButtoner.SetButtonToEdit(targetName);
        }

        private ColorBlock SetButtonColors(ColorBlock block, Color targetColor)
        {
            block.normalColor = targetColor;
            block.highlightedColor = targetColor;
            block.selectedColor = targetColor;
            block.pressedColor = targetColor;

            return block;
        }
    }
}