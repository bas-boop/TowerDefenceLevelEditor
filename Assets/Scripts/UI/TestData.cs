using System;

namespace UI
{
    [Serializable]
    public sealed class TestData
    {
        public string name = "file";
        public int[,] numbers;

        public TestData(int[,] numbers)
        {
            this.numbers = numbers;
        }
    }
}