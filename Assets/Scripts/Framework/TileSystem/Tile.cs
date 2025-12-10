using UnityEngine;

namespace Framework.TileSystem
{
    public sealed class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private string _id;
        
        private void OnMouseEnter() { }

        private void OnMouseExit() { }

        private void OnMouseOver()
        {
            if (Input.GetKey(KeyCode.Mouse0))
                DoDrag();
        }

        private void DoDrag()
        {
            _id = ToolData.Instance.SelectedTileId;
            spriteRenderer.color = _id switch
            {
                "Empty" => ColorArray.GetColor(0),
                "Grass" => ColorArray.GetColor(1),
                "Path" => ColorArray.GetColor(2),
                "Start" => ColorArray.GetColor(3),
                "End" => ColorArray.GetColor(4),
                _ => spriteRenderer.color
            };
        }
    }
}