using UnityEngine;

namespace Framework
{
    public sealed class ToolData : Singleton<ToolData>
    {
        public string SelectedTileId { get; set; } = "Empty";

        public void SetSelectedTileId(string id) => SelectedTileId = id;
    }
}