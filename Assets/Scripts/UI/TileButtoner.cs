using UnityEngine;
using UnityEngine.UI;

using Framework;
using Framework.TileSystem;
using TMPro;

namespace UI
{
    public sealed class TileButtoner : MonoBehaviour
    {
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private Transform buttonsParent;

        [SerializeField] private string tileName;
        [SerializeField] private Color tileColor;

        public void AddNewButton()
        {
            if (!TileDataHolder.Instance.CreateData(tileName, tileColor))
                return;
            
            Button b = Instantiate(buttonPrefab, buttonsParent);
            b.GetComponentInChildren<TMP_Text>().text = tileName;
            b.onClick.AddListener(ButtonEvent);
        }

        private void ButtonEvent()
        {
            ToolData.Instance.SetData(tileName);
        }
    }
}