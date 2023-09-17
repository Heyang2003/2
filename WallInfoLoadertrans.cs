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

    private string originalJsonData = "������������ԭʼJSON����";

    private void Start()
    {
        // ����ԭʼJSON����
        List<WallInfo> wallInfoList = JsonConvert.DeserializeObject<List<WallInfo>>(originalJsonData);

        // ����Ŀ���ʽ��JSON����
        List<WallInfo> targetWallInfoList = new List<WallInfo>();

        foreach (var wallInfo in wallInfoList)
        {
            WallInfo newWallInfo = new WallInfo
            {
                contour = new List<List<ContourPoint>>(),
                length = 1.0f // ��������ΪĿ���ʽ�е�Ĭ��ֵ
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
                        z = 0 // ��������ΪĿ���ʽ�е�Ĭ��ֵ
                    });
                }

                newWallInfo.contour.Add(newContour);
            }

            targetWallInfoList.Add(newWallInfo);
        }

        // ��Ŀ���ʽ��JSON����ת��Ϊ�ַ���
        string targetJsonData = JsonConvert.SerializeObject(targetWallInfoList, Formatting.Indented);

        // ���Ŀ���ʽ��JSON����
        Debug.Log(targetJsonData);
    }
}
