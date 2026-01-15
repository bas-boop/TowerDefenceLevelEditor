using UnityEngine;
using UnityEngine.Events;

using Tool.TileSystem;

namespace Framework.Command
{
    public sealed class AddTileButtonCommand : ICommand
    {
        private readonly string _tileName;
        private readonly Color _tileColor;
        private readonly UnityEvent<string> _onAdd;
        private bool _wasCreated;

        public AddTileButtonCommand(string tileName, Color tileColor, UnityEvent<string> onAdd)
        {
            _tileName = tileName;
            _tileColor = tileColor;
            _onAdd = onAdd;
        }

        public void Execute(bool runtime = false)
        {
            _wasCreated = TileDataHolder.Instance.CreateData(_tileName, _tileColor);
            
            if (_wasCreated)
                _onAdd?.Invoke(_tileName);
        }

        public void Undo()
        {
            if (_wasCreated)
                TileDataHolder.Instance.DeleteData(_tileName);
        }
    }
}