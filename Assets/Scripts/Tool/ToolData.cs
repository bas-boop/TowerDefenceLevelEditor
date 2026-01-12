using UnityEngine;

using Framework;
using Tool.TileSystem;

namespace Tool
{
    public sealed class ToolData : Singleton<ToolData>
    {
        [SerializeField] private TileData startingData;
        
        public TileData SelectedTileData { get; set; }

        private void Start() => SelectedTileData = startingData;

        public void SetSelectedTileId(TileData data) => SelectedTileData = data;
        
        public void SetData(string targetName)
        {
            SelectedTileData = TileDataHolder.Instance.GetData(targetName);
        }
    }
}