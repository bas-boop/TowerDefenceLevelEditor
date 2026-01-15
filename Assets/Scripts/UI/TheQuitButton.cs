using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class TheQuitButton : Button
    {
        public void Quit() => Application.Quit();
    }
}