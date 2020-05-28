using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Isin 
{
    public float distance;
    public Vector3 direction;
    public Vector3 origin;
    public Color color;
    public Vector3 HitPoint { get => origin + direction.normalized * distance; }
    public Isin(Vector3 origin, Vector3 direction, float distance)
    {
        this.origin = origin;
        this.direction = direction;
        this.distance = distance;
    }
    
    public void DrawRay()
    {
        Debug.DrawLine(origin, HitPoint, color);
    }
    public (bool,Vector3) CheckIntersection(Segment segment)
    {
        //https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection
        var x1 = segment.StartPoint.x;
        var y1 = segment.StartPoint.y;
        var x2 = segment.EndPoint.x;
        var y2 = segment.EndPoint.y;

        var x3 = this.origin.x;
        var y3 = this.origin.y;
        var x4 = this.origin.x + this.direction.x;
        var y4 = this.origin.y + this.direction.y;
        var denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);
        if (denominator==0)
        {

        }
        var t = ((x1 - x3) * (y3 - y4) - (y1 - y3) * (x3 - x4)) / denominator;
        var u = -((x1 - x2) * (y1 - y3) - (y1 - y2) * (x1 - x3)) / denominator;
        if (t>=0&&t<=1&&u>0)
        {
            //intersection var
            Vector3 intersectionPoint = new Vector3(x1 + t * (x2 - x1), y1 + t * (y2 - y1), 0);
            return (true,intersectionPoint);
        }
        else
        {
            //intersection yok
            return (false,new Vector3(0,0,0));
        }
    }
    public List<Segment> KestigiSegmentler(List<Segment> segments)
    {
        List<Segment> kesilenler = new List<Segment>();
        foreach (var item in segments)
        {
            if (this.CheckIntersection(item).Item1)
            {
                kesilenler.Add(item);
            }
        }
        return kesilenler;
    }
    public void Rotate(float angle)
    {
        direction = Quaternion.Euler(0, 0, angle) * direction;
        
    }
}
