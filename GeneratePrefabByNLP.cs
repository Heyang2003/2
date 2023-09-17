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

        // 使用正则表达式来捕捉关键词
        MatchCollection matches = Regex.Matches(inputText, @"(create|generate|spawn) (\w+)");

        foreach (Match match in matches)
        {
            string action = match.Groups[1].Value;
            string prefabName = match.Groups[2].Value;

            // 从Resources/Prefabs文件夹加载预制体
            GameObject prefab = Resources.Load<GameObject>($"Prefabs/{prefabName}");

            if (prefab != null)
            {
                // 实例化预制体在固定位置
                GameObject instance = Instantiate(prefab, new Vector3(1.57f, -5.32f, -6.5f), Quaternion.identity);

                // 设置预制体的大小为 4*4*4
                instance.transform.localScale = new Vector3(4, 4, 4);
            }
            else
            {
                Debug.Log($"Prefab named {prefabName} not found.");
            }
        }
    }
}

