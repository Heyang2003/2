using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class GenerateGeometryByNLP : MonoBehaviour
{
    public InputField inputField; // �����
    public Button generateButton; // ��ť

    void Start()
    {
        // �󶨰�ť�ĵ���¼�
        generateButton.onClick.AddListener(Generate);
    }

    void Generate()
    {
        string inputText = inputField.text.ToLower();

        // ʹ��������ʽ����׽��������ͳߴ�
        MatchCollection matches = Regex.Matches(inputText, @"(create|generate|make) (\d+) (\w+)(?: with size (\d+))?");

        foreach (Match match in matches)
        {
            string action = match.Groups[1].Value;
            int count = int.Parse(match.Groups[2].Value);
            string type = match.Groups[3].Value;
            int size = match.Groups[4].Value != "" ? int.Parse(match.Groups[4].Value) : 1; // Ĭ�ϳߴ�Ϊ1

            for (int i = 0; i < count; i++)
            {
                CreateObject(type, new Vector3(i * (size + 1), 0, 0), new Vector3(size, size, size));
            }
        }
    }

    void CreateObject(string type, Vector3 position, Vector3 scale)
    {
        PrimitiveType primitiveType = PrimitiveType.Cube; // Ĭ��Ϊ������

        if (type == "sphere")
        {
            primitiveType = PrimitiveType.Sphere;
        }
        // ��������Ӹ���ļ����������ͣ������Ҫ

        GameObject obj = GameObject.CreatePrimitive(primitiveType);
        obj.transform.position = position;
        obj.transform.localScale = scale;
    }
}
