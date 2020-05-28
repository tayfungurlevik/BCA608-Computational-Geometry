using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Polygon", menuName = "ScriptableObjects/PolygonData", order = 1)]
public class PolygonInfo : ScriptableObject
{
    
    public Vector3 q;
    public Segment[] Segments;
}
