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
            testData = new TestData(new int[3, 3]);
            
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    testData.numbers[i, j] = Convert.ToInt32(inputFields[i + j].text);
                }
            }
            
            return testData;
        }
    }
}