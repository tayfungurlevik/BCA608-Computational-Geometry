using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Segment
{
    private Vector3 startPoint;
    private Vector3 endPoint;

    public Vector3 StartPoint { get => startPoint; set => startPoint = value; }
    public Vector3 EndPoint { get => endPoint; set => endPoint = value; }
    public Segment(Vector3 start,Vector3 end)
    {
        this.startPoint = start;
        this.endPoint = end;
    }
    public float DistanceToPoint(Vector3 point)
    {
        return Mathf.Abs((endPoint.y - startPoint.y) * point.x - (endPoint.x - startPoint.x) 
            *
            point.y + endPoint.x * startPoint.y - endPoint.y * startPoint.x)
            / 
            Mathf.Sqrt(Mathf.Pow(endPoint.y - startPoint.y, 2) + Mathf.Pow(endPoint.x - startPoint.x, 2));
    }

}
