using System;
using System.Collections.Generic;
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
                cols = 3,
                numbers = new int[9]
            };

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    data.numbers[i * 3 + j] = Convert.ToInt32(inputFields[i * 3 + j].text);

            return data;
        }

        public void SetData(TestData data)
        {
            for (int i = 0; i < data.numbers.Length; i++)
            {
                inputFields[i].text = data.numbers[i].ToString();
            }
        }
    }
}