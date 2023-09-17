using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class GenerateGeometryByNLP : MonoBehaviour
{
    public InputField inputField; // 输入框
    public Button generateButton; // 按钮

    void Start()
    {
        // 绑定按钮的点击事件
        generateButton.onClick.AddListener(Generate);
    }

    void Generate()
    {
        string inputText = inputField.text.ToLower();

        // 使用正则表达式来捕捉几何物体和尺寸
        MatchCollection matches = Regex.Matches(inputText, @"(create|generate|make) (\d+) (\w+)(?: with size (\d+))?");

        foreach (Match match in matches)
        {
            string action = match.Groups[1].Value;
            int count = int.Parse(match.Groups[2].Value);
            string type = match.Groups[3].Value;
            int size = match.Groups[4].Value != "" ? int.Parse(match.Groups[4].Value) : 1; // 默认尺寸为1

            for (int i = 0; i < count; i++)
            {
                CreateObject(type, new Vector3(i * (size + 1), 0, 0), new Vector3(size, size, size));
            }
        }
    }

    void CreateObject(string type, Vector3 position, Vector3 scale)
    {
        PrimitiveType primitiveType = PrimitiveType.Cube; // 默认为立方体

        if (type == "sphere")
        {
            primitiveType = PrimitiveType.Sphere;
        }
        // 在这里添加更多的几何物体类型，如果需要

        GameObject obj = GameObject.CreatePrimitive(primitiveType);
        obj.transform.position = position;
        obj.transform.localScale = scale;
    }
}
