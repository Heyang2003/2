using System;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class WallInfoLoadertrans : MonoBehaviour
{
    [Serializable]
    public class ContourPoint
    {
        public float x;
        public float y;
        public float z;
    }

    [Serializable]
    public class WallInfo
    {
        public List<List<ContourPoint>> contour;
        public float length;
    }

    private string originalJsonData = "在这里放入你的原始JSON数据";

    private void Start()
    {
        // 解析原始JSON数据
        List<WallInfo> wallInfoList = JsonConvert.DeserializeObject<List<WallInfo>>(originalJsonData);

        // 创建目标格式的JSON数据
        List<WallInfo> targetWallInfoList = new List<WallInfo>();

        foreach (var wallInfo in wallInfoList)
        {
            WallInfo newWallInfo = new WallInfo
            {
                contour = new List<List<ContourPoint>>(),
                length = 1.0f // 这里设置为目标格式中的默认值
            };

            foreach (var contour in wallInfo.contour)
            {
                List<ContourPoint> newContour = new List<ContourPoint>();

                foreach (var point in contour)
                {
                    newContour.Add(new ContourPoint
                    {
                        x = 0,
                        y = 0,
                        z = 0 // 这里设置为目标格式中的默认值
                    });
                }

                newWallInfo.contour.Add(newContour);
            }

            targetWallInfoList.Add(newWallInfo);
        }

        // 将目标格式的JSON数据转换为字符串
        string targetJsonData = JsonConvert.SerializeObject(targetWallInfoList, Formatting.Indented);

        // 输出目标格式的JSON数据
        Debug.Log(targetJsonData);
    }
}
