using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;

public class SocketClient : MonoBehaviour
{
    public Text inputField;  // ָ��Unity�����������ı��������

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
                // ���������ı��������
                inputField.text = receivedData;
            }
        }
    }
}
