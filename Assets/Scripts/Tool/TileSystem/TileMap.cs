using System.Collections.Generic;
using UnityEngine;

namespace Tool.TileSystem
{
    public sealed class TileMap : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private TileData noneTileData;
        [SerializeField] private Vector2Int size = Vector2Int.one * 3;

        private List<Tile> _tiles = new();

        private void Awake() => InitTileMap(null);

        public List<Tile> GetTiles() => _tiles;

        public TilemapData GetData()
        {
            TilemapData data = new ()
            {
                identifier = "TDLE",
                version = "0.1",
                rows = size.x,
                cols = size.y
            };
            
            int total = data.rows * data.cols;
            data.tileId = new string[total];

            for (int y = 0; y < data.cols; y++)
            {
                for (int x = 0; x < data.rows; x++)
                {
                    int i = y * data.rows + x;

                    if (i >= _tiles.Count)
                        continue;

                    data.tileId[i] = _tiles[i].GetId();
                }
            }

            return data;
        }

        public void CreateNewMap(TilemapData data) => InitTileMap(data);

        public void SetHeight(string input)
        {
            int.TryParse(input, out int height);
            if (height <= 0)
                return;

            TilemapData oldData = GetData();
            size = new(size.x, height);
            CreateNewMap(oldData);
        }

        public void SetWidth(string input)
        {
            int.TryParse(input, out int width);
            
            if (width <= 0)
                return;

            TilemapData oldData = GetData();
            size = new(width, size.y);
            CreateNewMap(oldData);
        }
        
        public int GetBiggestSide() => size.x >= size.y ? size.x : size.y;

        private void InitTileMap(TilemapData? data)
        {
            ClearGrid();

            int newWidth = size.x;
            int newHeight = size.y;
            int newCount = newWidth * newHeight;

            int oldWidth = data?.rows ?? 0;
            int oldHeight = data?.cols ?? 0;

            for (int i = 0; i < newCount; i++)
            {
                int x = i % newWidth;
                int y = i / newWidth;

                Tile currentTile = CreateTile(new(x, y)).GetComponent<Tile>();
                _tiles.Add(currentTile);

                bool assigned = false;
                
                if (data != null)
                {
                    TryAssignTile(out assigned,
                        x,
                        y,
                        oldWidth,
                        oldHeight,
                        data,
                        currentTile);
                }

                if (!assigned)
                    currentTile.SetTileId(noneTileData);
            }
        }

        private void TryAssignTile(out bool assigned,
            int x,
            int y,
            int oldWidth,
            int oldHeight,
            TilemapData data,
            Tile currentTile)
        {
            if (x < oldWidth
                && y < oldHeight)
            {
                int oldIndex = y * oldWidth + x;

                if (oldIndex < data.tileId.Length)
                {
                    string id = data.tileId[oldIndex];
                    TileData tileData = TileDataHolder.Instance.GetData(id);

                    if (tileData != null)
                    {
                        currentTile.SetTileId(tileData);
                        assigned = true;
                        return;
                    }
                }
            }

            assigned = false;
        }
        
        private GameObject CreateTile(Vector2 pos)
        {
            GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            tile.name = $"Tile {pos.x}, {pos.y}";
            return tile;
        }

        private void ClearGrid()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);

            _tiles.Clear();
        }
    }
}