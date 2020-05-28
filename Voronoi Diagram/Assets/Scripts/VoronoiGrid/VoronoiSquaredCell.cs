using PixelsForGlory.ComputationalSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoronoiSquaredCell 
{
    public Vector3 center;
    public int size;
    public Vector2[] corners=new Vector2[4];
    // Start is called before the first frame update
    public VoronoiSquaredCell(Vector3 center,int size)
    {
        this.center = center;
        this.size = size;
        CalculateCorners();
    }

    private void CalculateCorners()
    {
        corners[0] = center + (new Vector3(-1, -1, 0)).normalized * size * Mathf.Sqrt(2) / 2;//Sol alt kose
        corners[1] = center + (new Vector3(1, -1, 0)).normalized * size * Mathf.Sqrt(2) / 2;//Sag alt kose
        corners[2] = center + (new Vector3(1, 1, 0)).normalized * size * Mathf.Sqrt(2) / 2;//Sag ust kose
        corners[3] = center + (new Vector3(-1, 1, 0)).normalized * size * Mathf.Sqrt(2) / 2;//Sol ust kose
    }
    public void ChangeSize(int newSize)
    {
        size = newSize;
        CalculateCorners();
    }


}
