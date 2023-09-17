# import cv2

# # 加载图像
# image = cv2.imread(r"C:\Users\86136\Desktop\opencv\somephotos\room.png")

# # 图像预处理（例如，灰度化、二值化、去噪声）
# gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)
# thresh = cv2.threshold(gray, 100, 255, cv2.THRESH_BINARY)[1]

# # 提取建筑物轮廓
# contours, _ = cv2.findContours(thresh, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

# # 遍历轮廓并测量面积
# for contour in contours:
#     area = cv2.contourArea(contour)
#     print(f"墙面积：{area/100000} M²")

# # 在图像上绘制轮廓
# cv2.drawContours(image, contours, -1, (0, 255, 0), 2)

# # 显示结果
# cv2.imshow('Building Blueprint', thresh)
# cv2.waitKey(0)
# cv2.destroyAllWindows()
import cv2
import json
import numpy as np
# 读取图像
image = cv2.imread(r"C:\Users\86136\Desktop\opencv\somephotos\room.png")
gray = cv2.cvtColor(image, cv2.COLOR_BGR2GRAY)

# 图像预处理：去噪和二值化
# 使用高斯模糊进行去噪
blurred = cv2.GaussianBlur(gray, (5, 5), 0)

# 图像预处理：二值化
_, binary_image = cv2.threshold(blurred, 100, 255, cv2.THRESH_BINARY_INV + cv2.THRESH_OTSU)

# 轮廓检测
contours, _ = cv2.findContours(binary_image, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

# 设定面积阈值
min_contour_area = 6000  # 根据需求调整阈值

# 保存符合面积阈值的墙的位置和尺寸信息
walls_info = []

for contour in contours:
    # 计算轮廓的面积
    area = cv2.contourArea(contour)
    
    # 如果面积大于阈值，将轮廓信息保存
    if area >= min_contour_area:
        perimeter = cv2.arcLength(contour, True)
        length = perimeter / 2  # 因为每个墙被两次计算，所以要除以2
        walls_info.append({'contour': contour, 'length': length})


# 显示图像并绘制墙的轮廓
image_with_contours = image.copy()
cv2.drawContours(image_with_contours, [wall['contour'] for wall in walls_info], -1, (0, 255, 0), 2)
 

# 将信息保存为JSON文件之前，将轮廓数据转换为Python列表
for wall_info in walls_info:
    wall_info['contour'] = wall_info['contour'].tolist()

# 指定要保存的JSON文件路径
output_file_path = "walls_info.json"

# 将信息保存为JSON文件
with open(output_file_path, 'w') as json_file:
    json.dump(walls_info, json_file)

print(f'信息已保存到 {output_file_path}')



# 将图像缩小
scale_percent = 70  # 缩小比例
width = int(image_with_contours.shape[1] * scale_percent / 100)
height = int(image_with_contours.shape[0] * scale_percent / 100)
smaller_image = cv2.resize(image_with_contours, (width, height))

cv2.imshow('Building Blueprint', smaller_image)
cv2.waitKey(0)
cv2.destroyAllWindows()

# 在打印轮廓信息之前，将轮廓数据还原为NumPy数组
for idx, wall in enumerate(walls_info):
    if 'contour' in wall:
        contour = np.array(wall['contour'])
        area = cv2.contourArea(contour)
        print(f'墙{idx + 1} - 长度: {wall["length"]:.2f} 像素，面积: {area:.2f} 像素')
    else:
        print(f'未找到有效轮廓数据')

