using UnityEngine;
using UnityEngine.Events;

using Tool.TileSystem;

namespace Framework.Command
{
    public sealed class EditTileButtonCommand : ICommand
    {
        private readonly string _tileName;
        private readonly Color _oldColor;
        private readonly Color _newColor;
        private readonly UnityEvent<string, Color> _onEdit;

        public EditTileButtonCommand(string tileName, Color oldColor, Color newColor, UnityEvent<string, Color> onEdit)
        {
            _tileName = tileName;
            _oldColor = oldColor;
            _newColor = newColor;
            _onEdit = onEdit;
        }

        public void Execute(bool runtime = false)
        {
            TileDataHolder.Instance.EditData(_tileName, _newColor);
            _onEdit?.Invoke(_tileName, _newColor);
        }

        public void Undo()
        {
            TileDataHolder.Instance.EditData(_tileName, _oldColor);
            _onEdit?.Invoke(_tileName, _oldColor);
        }
    }
}