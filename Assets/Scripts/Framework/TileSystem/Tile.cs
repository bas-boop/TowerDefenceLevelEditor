using UnityEngine;

namespace Framework.TileSystem
{
    public sealed class Tile : MonoBehaviour
    {
        [SerializeField] private TileData tileData;
        [SerializeField] private SpriteRenderer spriteRenderer;

        private void OnMouseEnter() { }

        private void OnMouseExit() { }

        private void OnMouseOver()
        {
            if (Input.GetKey(KeyCode.Mouse0))
                DoDrag();
        }

        public void SetTileId(TileData data)
        {
            tileData = data;
            spriteRenderer.color = tileData.tileColor;
        }

        public string GetId() => tileData.tileName;
        
        private void DoDrag()
        {
            tileData = ToolData.Instance.SelectedTileData;
            SetTileId(tileData);
        }
    }
}