using UnityEngine;

using Tool.TileSystem;

namespace UI
{
    public sealed class TileButtonStart : MonoBehaviour
    {
        [SerializeField] private TileButtoner tileButtoner;

        private void Start()
        {
            tileButtoner.AddSetupButtons(TileDataHolder.Instance.GetAllData());
        }
    }
}