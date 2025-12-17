using TMPro;
using UnityEngine;

using Framework.TileSystem;

namespace UI
{
    public sealed class FileEditor : MonoBehaviour
    {
        [SerializeField] private TilemapData tilemapData;
        [SerializeField] private TileMap tileMap;
        [SerializeField] private TMP_InputField[] inputFields;

        public TilemapData GetData()
        {
            TilemapData data = new ()
            {
                //todo fix hardcode numbers
                rows = 5,
                cols = 5
            };
            data.tileId = new int[data.rows * data.cols];
            
            for (int i = 0; i < data.rows; i++)
                for (int j = 0; j < data.cols; j++)
                    data.tileId[i * data.rows + j] = tileMap.GetTiles()[i * data.rows + j].GetId();

            return data;
        }

        public void SetData(TilemapData data)
        {
            tileMap.CreateNewMap(data);
        }
    }
}