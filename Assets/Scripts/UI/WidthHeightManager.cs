using TMPro;
using UnityEngine;

using Tool.TileSystem;

namespace UI
{
    public sealed class WidthHeightManager : MonoBehaviour
    {
        [SerializeField] private TMP_InputField widthField;
        [SerializeField] private TMP_InputField heightField;

        public void UpdateText(TilemapData data)
        {
            widthField.text = data.rows.ToString();
            heightField.text = data.cols.ToString();
        }

        public void UpdateText(int w, int h)
        {
            widthField.text = w.ToString();
            heightField.text = h.ToString();
        }
    }
}