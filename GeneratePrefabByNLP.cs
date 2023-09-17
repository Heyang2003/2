using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class GeneratePrefabByNLP : MonoBehaviour
{
    public InputField inputField;
    public Button generateButton;

    void Start()
    {
        generateButton.onClick.AddListener(Generate);
    }

    void Generate()
    {
        string inputText = inputField.text.ToLower();

        // ʹ��������ʽ����׽�ؼ���
        MatchCollection matches = Regex.Matches(inputText, @"(create|generate|spawn) (\w+)");

        foreach (Match match in matches)
        {
            string action = match.Groups[1].Value;
            string prefabName = match.Groups[2].Value;

            // ��Resources/Prefabs�ļ��м���Ԥ����
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");

            if (prefab != null)
            {
                // ʵ����Ԥ�����ڹ̶�λ��
                GameObject instance = Instantiate(prefab, new Vector3(1.57f, -5.32f, -6.5f), Quaternion.identity);

                // ����Ԥ����Ĵ�СΪ 4*4*4
                instance.transform.localScale = new Vector3(4, 4, 4);
            }
            else
            {
                Debug.Log($"Prefab named {prefabName} not found.");
            }
        }
    }
}

