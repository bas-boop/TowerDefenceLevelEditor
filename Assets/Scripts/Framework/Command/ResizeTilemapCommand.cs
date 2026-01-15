using Tool.TileSystem;
using UnityEngine;

namespace Framework.Command
{
    public class ResizeTilemapCommand : ICommand
    {
        private TileMap _tileMap;

        private Vector2Int _oldSize;
        private Vector2Int _newSize;

        private TilemapData _oldData;

        public ResizeTilemapCommand(TileMap tileMap, Vector2Int newSize, Vector2Int oldSize)
        {
            _tileMap = tileMap;
            _newSize = newSize;
            _oldSize = oldSize;
            _oldData = tileMap.GetData();
        }

        public void Execute(bool runtime = false) => _tileMap.Resize(_newSize);

        public void Undo() => _tileMap.Resize(_oldSize);
    }
}