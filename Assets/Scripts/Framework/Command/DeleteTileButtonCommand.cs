using UnityEngine;
using UnityEngine.Events;

using Tool.TileSystem;

namespace Framework.Command
{
    public sealed class DeleteTileButtonCommand : ICommand
    {
        private readonly string _tileName;
        private readonly Color _tileColor;
        private readonly UnityEvent<string> _onRemove;
        private readonly UnityEvent<string> _onAdd;
        private bool _wasDeleted;

        public DeleteTileButtonCommand(string tileName, Color tileColor, UnityEvent<string> onRemove, UnityEvent<string> onAdd)
        {
            _tileName = tileName;
            _tileColor = tileColor;
            _onRemove = onRemove;
            _onAdd = onAdd;
        }

        public void Execute(bool runtime = false)
        {
            _wasDeleted = TileDataHolder.Instance.DeleteData(_tileName); 
            
            if (_wasDeleted)
                _onRemove?.Invoke(_tileName);
        }

        public void Undo()
        {
            if (!_wasDeleted)
                return;
            
            TileDataHolder.Instance.CreateData(_tileName, _tileColor);
            _onAdd?.Invoke(_tileName);
        }
    }
}