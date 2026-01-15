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
        
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private TileButtoner tileButtoner;

        private Dictionary<string, Button> _buttons = new(); 

        private void Start()
        {
            TileDataHolder holder = TileDataHolder.Instance;
            
            if (holder == null)
                return;

            TileDatas tileDatas = holder.GetAllData();

            for (int i = 0; i < tileDatas.tileNames.Length; i++)
            {
                GameObject buttonObject = Instantiate(tileButton, parent);
                Button currentButton = buttonObject.GetComponent<Button>();
                currentButton.GetComponentInChildren<TMP_Text>().text = tileDatas.tileNames[i];

                int cachedIndex = i;
                currentButton.onClick.AddListener(() => SelectButton(tileDatas.tileNames[cachedIndex], tileDatas.tileColors[cachedIndex]));
                currentButton.colors = SetButtonColors(currentButton.colors, tileDatas.tileColors[i]);
                _buttons.Add(tileDatas.tileNames[i], currentButton);
            }
        }
        
        public void AddButton(string buttonName)
        {
            if (_buttons.ContainsKey(buttonName))
                return;

            GameObject buttonObject = Instantiate(tileButton, parent);
            Button currentButton = buttonObject.GetComponent<Button>();

            TMP_Text text = currentButton.GetComponentInChildren<TMP_Text>();
            text.text = buttonName;

            Color color = colorPicker.GetSelectedColor();

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