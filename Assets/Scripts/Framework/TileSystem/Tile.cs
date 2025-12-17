using UnityEngine;

namespace Framework.TileSystem
{
    public sealed class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private int _id;

        private void OnMouseEnter() { }

        private void OnMouseExit() { }

        private void OnMouseOver()
        {
            if (Input.GetKey(KeyCode.Mouse0))
                DoDrag();
        }

        public void SetTileId(int id)
        {
            _id = id;
            
            //todo: fix hardcoded id's with tile name
            string a = _id switch
            {
                0 => "Empty",
                1 => "Grass",
                2 => "Path",
                3 => "Start",
                4 => "End",
                _ => "Empty"
            };
            
            spriteRenderer.color = a switch
            {
                "Empty" => ColorArray.GetColor(0),
                "Grass" => ColorArray.GetColor(1),
                "Path" => ColorArray.GetColor(2),
                "Start" => ColorArray.GetColor(3),
                "End" => ColorArray.GetColor(4),
                _ => Color.black
            };
        }

        public int GetId() => _id;
        
        private void DoDrag()
        {
            _id = ToolData.Instance.SelectedTileId;
            SetTileId(_id);
        }
    }
}