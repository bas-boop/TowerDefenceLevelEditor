using UnityEngine;

using Framework.TileSystem;

namespace Framework.Command
{
    public sealed class TileCommand : ICommand
    {
        private Tile _tile;

        private string _oldName;
        private Color _oldColor;

        private string _newName;
        private Color _newColor;

        public TileCommand(Tile tile, string oldName, Color oldColor, string newName, Color newColor)
        {
            _tile = tile;

            _oldName = oldName;
            _oldColor = oldColor;

            _newName = newName;
            _newColor = newColor;
        }

        public void Execute(bool runtime = false) => _tile.Apply(_newName, _newColor);

        public void Undo() => _tile.Apply(_oldName, _oldColor);
    }

}