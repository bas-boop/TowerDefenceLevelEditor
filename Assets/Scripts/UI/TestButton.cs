using UnityEngine;

using Tool.TileSystem;

namespace UI
{
    public sealed class TestButton : MonoBehaviour
    {
        [SerializeField] private Tile tile;
        [SerializeField] private TileData yes;
        
        public void Test()
        {
            TileDataHolder.Instance.CreateData("new", Color.beige);
        }

        public void Test2()
        {
            Tile t = Instantiate(tile);
            t.GetComponent<SpriteRenderer>().color =TileDataHolder.Instance.GetData("new").tileColor;
        }
    }
}