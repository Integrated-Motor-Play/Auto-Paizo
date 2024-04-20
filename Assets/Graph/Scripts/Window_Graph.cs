/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using TMPro;
using Random = System.Random;



public class Window_Graph : MonoBehaviour
{
    public int maxValueCount = 18;
    public int xSize = 50;
    public int height = 5;
    public float Value { get; set; }
    private float SmoothValue;
    private static float virtualValue;
    private static float isVirtualUp = 1;

    public BluetoothConnector Connector
    {
        get;
        set;
    }
    
    [SerializeField] private Sprite circleSprite;
    public Color circleColor;
    public Color lineColor;
    private RectTransform graphContainer;
    private List<(float, Color)> valueList = new List<(float, Color)>();

    private void Awake() {
        graphContainer = GetComponent<RectTransform>();
        DrawLine(0, Color.gray);
        DrawLine(1, Color.gray);
    }

    public void DrawLine(float h, Color color)
    {
        CreateDotConnection(new Vector2(0,HeightTransfer(h))
            , new Vector2(graphContainer.rect.width, HeightTransfer(h)),
            transform.parent,color);
    }
    
    public void DrawUniqueLine(float h, Color color, string key)
    {
        var parent = transform.parent.Find(key);
        if (parent == null)
        {
            parent = new GameObject(key, typeof(RectTransform)).transform;
            var rect = parent.GetComponent<RectTransform>();
            rect.anchorMin = new Vector2(0, 0);
            rect.anchorMax = new Vector2(1, 1);
            rect.offsetMin = new Vector2(0, 0); // This sets the left and bottom margins to 0
            rect.offsetMax = new Vector2(0, 0); // This sets the right and top margins to 0
            parent.SetParent(transform.parent, false);
            parent.localPosition = Vector3.zero;
        }
        else
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
        CreateDotConnection(new Vector2(0,HeightTransfer(h))
            , new Vector2(graphContainer.rect.width, HeightTransfer(h)),
            parent, color);
    }

    private float HeightTransfer(float h)
    {
        float graphHeight = graphContainer.rect.height;
        float yMaximum = 0.5f;
        h = h * yMaximum * graphHeight + graphHeight * 0.5f * (1 - yMaximum);
        return h;
    }

    private void Update()
    {
        if(Connector == null) return;
        ClearGraph();
        Value = Connector.EMGValueNormalized;
        //Value = GetVirtualValue();
        SmoothValue += (Value - SmoothValue) * Time.deltaTime * 10;
        valueList.Add((SmoothValue, circleColor));
        if(valueList.Count() > maxValueCount)
            valueList.RemoveAt(0);
        ShowGraph(valueList);
    }

    public static float GetVirtualValue()
    {
        if (UnityEngine.Random.Range(0,  100) < (1-Mathf.Abs(virtualValue - 0.5f)) * 10f)
            isVirtualUp = UnityEngine.Random.Range(-1f,1f);
        isVirtualUp = virtualValue switch
        {
            > 1 => -0.1f,
            < 0 => 0.1f,
            _ => isVirtualUp
        };
        virtualValue += UnityEngine.Random.Range(0, (Mathf.Abs(virtualValue - 0.5f) + 0.1f) * 10f) * Time.deltaTime * isVirtualUp;
        return virtualValue;
    }

    private GameObject CreateCircle(Vector2 anchoredPosition, Color color) {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(5, 5);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void ShowGraph(List<(float, Color)> valueList) {
        GameObject lastCircleGameObject = null;
        for (int i = 0; i < valueList.Count; i++) {
            float xPosition = xSize + i * xSize;
            float yPosition = HeightTransfer(valueList[i].Item1);
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), valueList[i].Item2);
            if (lastCircleGameObject != null) {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircleGameObject = circleGameObject;
        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        CreateDotConnection(dotPositionA, dotPositionB, graphContainer, lineColor);
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, Transform parent, Color color)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(parent, false);
        gameObject.GetComponent<Image>().color = color;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * .5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }

    public void ClearGraph()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
