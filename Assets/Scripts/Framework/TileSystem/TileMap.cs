using System.Collections.Generic;
using UnityEngine;

namespace Framework.TileSystem
{
    public sealed class TileMap : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Vector2Int size = Vector2Int.one * 3;

        private List<Tile> _tiles = new();

        private void Awake()
        {
            InitTileMap(null);
        }

        public List<Tile> GetTiles() => _tiles;

        public void CreateNewMap(TilemapData data)
        {
            size = new (data.rows, data.cols);
            InitTileMap(data);
        }
        
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
                
                if (data != null)
                    _tiles[i].SetTileId(data.tileId[i]);
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