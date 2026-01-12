using UnityEngine;

namespace UI
{
    public sealed class UrlOpener : MonoBehaviour
    {
        public void OpenUrl(string url) => Application.OpenURL(url);
    }
}