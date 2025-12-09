using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class FileEditor : MonoBehaviour
    {
        [SerializeField] private TestData testData;
        [SerializeField] private TMP_InputField[] inputFields;

        public TestData GetData()
        {
            TestData data = new ()
            {
                rows = 3,
                cols = 3
            };
            data.numbers = new int[data.rows * data.cols];

            for (int i = 0; i < data.rows; i++)
                for (int j = 0; j < data.cols; j++)
                    data.numbers[i * data.rows + j] = Convert.ToInt32(inputFields[i * data.rows + j].text);

            return data;
        }

        public void SetData(TestData data)
        {
            for (int i = 0; i < data.numbers.Length; i++)
                inputFields[i].text = data.numbers[i].ToString();
        }
    }
}