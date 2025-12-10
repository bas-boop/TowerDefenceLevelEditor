using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    [Serializable]
    public sealed class TestData
    {
        public string name = "file";
        public int rows;
        public int cols;
        public string[] tileId;
    }
}