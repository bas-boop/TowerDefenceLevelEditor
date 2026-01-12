using System.Collections.Generic;
using UnityEngine;

using Tool;
using Tool.TileSystem;

namespace Player
{
    public sealed class CameraController : MonoBehaviour
    {
        private const float MARGIN = 0.75f;
        
        [SerializeField] private TileMap tileMap;
        [SerializeField] private Rigidbody2D rb;

        [SerializeField] private float speed = 3f;
        [SerializeField] private float baseZoom = 5f;
        [SerializeField] private float minSpeedMultiplier = 0.3f;
        [SerializeField] private float maxSpeedMultiplier = 3f;


        private Camera _camera;
        private Vector2 _centerPos;

        private void Awake() => _camera = Camera.main;

        private void Start() => ResetPosition();

        public void Move(Vector2 input)
        {
            if (ToolStateChanger.Instance.CurrentState == ToolStates.TILE_CREATION)
                return;

            float zoomFactor = _camera.orthographicSize / baseZoom;
            zoomFactor = Mathf.Clamp(zoomFactor, minSpeedMultiplier, maxSpeedMultiplier);
            rb.linearVelocity = input * (speed * zoomFactor);
        }

        public void ResetPosition()
        {
            if (ToolStateChanger.Instance.CurrentState == ToolStates.TILE_CREATION)
                return;
            
            List<Tile> tiles = tileMap.GetTiles();
            Bounds b = new();
            
            foreach (Tile t in tiles)
                b.Encapsulate(t.transform.position);

            transform.position = b.center;
            Zoom(tileMap.GetBiggestSide(), true);
        }

        public void Zoom(float scroll, bool isRest = false)
        {
            if (ToolStateChanger.Instance.CurrentState == ToolStates.TILE_CREATION)
                return;
            
            if (isRest)
            {
                _camera.orthographicSize = scroll * MARGIN;
                return;
            }
            
            _camera.orthographicSize += scroll;
            
            if (_camera.orthographicSize <= 1)
                _camera.orthographicSize = 1;
        }
    }
}