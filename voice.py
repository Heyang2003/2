import socket
import pyaudio
import wave
import requests
import json
import base64
import keyboard

# 获取百度API访问令牌
def get_access_token(api_key, secret_key):
    try:
        auth_url = f"https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id={api_key}&client_secret={secret_key}"
        response = requests.get(auth_url)
        json_data = json.loads(response.text)
        return json_data['access_token']
    except Exception as e:
        return f"Error: {e}"

# 百度语音识别
def speech_recognition(audio_data, access_token):
    try:
        audio_base64 = base64.b64encode(audio_data).decode('utf-8')
        audio_len = len(audio_data)
        url = "http://vop.baidu.com/server_api"
        headers = {'Content-Type': 'application/json'}
        data = {
            "format": "wav",
            "rate": 16000,
            "dev_pid": 1537,
            "channel": 1,
            "token": access_token,
            "cuid": "your_unique_cuid",
            "len": audio_len,
            "speech": audio_base64
        }
        response = requests.post(url, json=data, headers=headers)
        result = json.loads(response.text)
        if result['err_no'] == 0:
            return result['result'][0]
        else:
            return f"Error: {result['err_msg']}"
    except Exception as e:
        return f"Error: {e}"

# 创建Socket服务器
server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server_socket.bind(('localhost', 1678))
server_socket.listen(1)
print("等待Unity客户端连接...")
client_socket, address = server_socket.accept()
print(f"Unity客户端已连接：{address}")

# 初始化pyaudio
p = pyaudio.PyAudio()

while True:
    frames = []
    stream = p.open(format=pyaudio.paInt16,
                    channels=1,
                    rate=16000,
                    input=True,
                    frames_per_buffer=1024)

    print("开始录音，请说话...按ESC停止录音")

    while True:
        data = stream.read(1024)
        frames.append(data)
        if keyboard.is_pressed('esc'):
            print("录音停止")
            break

    stream.stop_stream()
    stream.close()

    audio_data = b''.join(frames)
    API_KEY = "0M47Yhg95AjV57GkgzsijI6C"
    SECRET_KEY = "ci4Cmi4DCGjL1FzUpO8EpkiiBU10fTpc"
    access_token = get_access_token(API_KEY, SECRET_KEY)

    text = speech_recognition(audio_data, access_token)
    # print("识别结果：", text)

    # 将识别结果发送到Unity
    client_socket.sendall(text.encode('utf-8'))

    user_input = input("继续录音吗？(y/n): ")
    if user_input.lower() != 'y':
        break

client_socket.close()
p.terminate()
