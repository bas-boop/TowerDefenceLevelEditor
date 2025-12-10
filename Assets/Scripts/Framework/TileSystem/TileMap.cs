using System;
using UnityEngine;

namespace Framework.TileSystem
{
    public sealed class TileMap : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Vector2Int size = Vector2Int.one * 3;

        private void Awake()
        {
            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    GameObject tile = Instantiate(tilePrefab, new Vector3(x, y), Quaternion.identity, transform);
                    tile.name = $"Tile {x}, {y}";
                }
            }
        }
    }
}