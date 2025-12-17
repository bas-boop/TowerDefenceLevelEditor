using UnityEngine;

namespace Framework
{
    public sealed class ToolData : Singleton<ToolData>
    {
        public int SelectedTileId { get; set; } = 0;

        public void SetSelectedTileId(int id) => SelectedTileId = id;
    }
}