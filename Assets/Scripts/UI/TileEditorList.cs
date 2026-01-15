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
                currentButton.colors = SetButtonColors(currentButton.colors, tileDatas.tileColors[i]);;
            }
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