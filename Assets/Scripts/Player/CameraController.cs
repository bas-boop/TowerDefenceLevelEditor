using System;
using System.Collections.Generic;
using Framework.TileSystem;
using UnityEngine;

namespace Player
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private TileMap tileMap;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float speed = 3;

        private Camera _camera;
        private Vector2 _centerPos;

        private void Awake() => _camera = Camera.main;

        private void Start() => ResetPosition();

        public void Move(Vector2 input)
        {
            rb.linearVelocity = input * speed;
        }

        public void ResetPosition()
        {
            List<Tile> tiles = tileMap.GetTiles();
            Bounds b = new();
            
            foreach (Tile t in tiles)
                b.Encapsulate(t.transform.position);

            transform.position = b.center;
        }

        public void Zoom(float scroll)
        {
            _camera.orthographicSize += scroll;
            
            if (_camera.orthographicSize <= 1)
                _camera.orthographicSize = 1;
        }
    }
}