using UnityEngine;
using UnityEngine.UI;
using TMPro;

using Framework;
using Framework.TileSystem;

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

        public void AddNewButton()
        {
            tileName = inputField.text;
            tileColor = colorPicker.GetSelectedColor();
            
            if (!TileDataHolder.Instance.CreateData(tileName, tileColor))
                return;
            
            Button b = Instantiate(buttonPrefab, buttonsParent);
            b.GetComponentInChildren<TMP_Text>().text = tileName;
            b.onClick.AddListener(() => ButtonEvent(tileName));
        }

        private void ButtonEvent(string targetName)
        {
            Debug.Log(targetName);
            ToolData.Instance.SetData(targetName);
        }
    }
}