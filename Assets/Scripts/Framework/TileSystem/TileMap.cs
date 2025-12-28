using System.Collections.Generic;
using UnityEngine;

namespace Framework.TileSystem
{
    public sealed class TileMap : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Vector2Int size = Vector2Int.one * 3;

        private List<Tile> _tiles = new();

        private void Awake() => InitTileMap(null);

        public List<Tile> GetTiles() => _tiles;

        public TilemapData GetData()
        {
            TilemapData data = new ()
            {
                jsonFile = "TDLE",
                version = "0.1",
                rows = size.x,
                cols = size.y
            };
            
            data.tileId = new int[data.rows * data.cols];
            
            for (int i = 0; i < data.rows; i++)
            for (int j = 0; j < data.cols; j++)
            {
                if (i * data.rows + j >= _tiles.Count)
                    continue;
                
                data.tileId[i * data.rows + j] = _tiles[i * data.rows + j].GetId();
            }

            return data;
        }

        public void CreateNewMap(TilemapData data)
        {
            size = new (data.rows, data.cols);
            InitTileMap(data);
        }

        public void SetHeight(string input)
        {
            int.TryParse(input, out int height);
            
            if (height <= 0)
                return;
            
            size = new (size.x, height);
            CreateNewMap(GetData());
        }

        public void SetWidth(string input)
        {
            int.TryParse(input, out int width);
            
            if (width <= 0)
                return;
            
            size = new (width, size.y);
            CreateNewMap(GetData());
        }

        public int GetBiggestSide() => size.x >= size.y ? size.x : size.y;

        private void InitTileMap(TilemapData? data)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
                Destroy(transform.GetChild(i).gameObject);
            
            if (_tiles.Count > 0)
                _tiles.Clear();
            
            for (int i = 0; i < size.x * size.y; i++)
            {
                int x = i % size.x;
                int y = i / size.x;
                
                _tiles.Add(CreateTile(new (x, y)).GetComponent<Tile>());
                _tiles[i].SetTileId(data != null ? data.tileId[i] : 0);
            }
        }
        
        private GameObject CreateTile(Vector2 pos)
        {
            GameObject tile = Instantiate(tilePrefab, pos, Quaternion.identity, transform);
            tile.name = $"Tile {pos.x}, {pos.y}";
            return tile;
        }
    }
}