using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class SocketClient : MonoBehaviour
{
    public Text inputField;  // 指向Unity场景中输入文本框的引用

    void Start()
    {
        TcpClient client = new TcpClient("localhost", 1678);
        NetworkStream stream = client.GetStream();

        while (true)
        {
            byte[] data = new byte[1024];
            int bytesRead = stream.Read(data, 0, data.Length);
            string receivedData = System.Text.Encoding.UTF8.GetString(data, 0, bytesRead);

            if (!string.IsNullOrEmpty(receivedData))
            {
                // 更新输入文本框的内容
                inputField.text = receivedData;
            }
        }
    }
}
