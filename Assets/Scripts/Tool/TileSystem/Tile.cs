using UnityEngine;

using Framework.Command;

namespace Tool.TileSystem
{
    public sealed class Tile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private string _tileName;
        private Color _tileColor;

        private void OnMouseOver()
        {
            if (ToolStateChanger.Instance.CurrentState == ToolStates.LEVEL_EDITING
                && Input.GetKey(KeyCode.Mouse0))
                DoDrag();
        }

        public void SetTileId(TileData targetData)
        {
            if (targetData == null)
                return;

            if (_tileName == targetData.tileName
                && _tileColor == targetData.tileColor)
                return;

            CommandSystem.Instance.Execute(
                new TileCommand(
                    this,
                    _tileName,
                    _tileColor,
                    targetData.tileName,
                    targetData.tileColor
                )
            );
        }

        public void Apply(string name, Color color)
        {
            _tileName = name;
            _tileColor = color;
            spriteRenderer.color = color;
        }

        public string GetId() => _tileName;

        private void DoDrag()
        {
            TileData tileData = ToolData.Instance.SelectedTileData;
            SetTileId(tileData);
        }
    }
}