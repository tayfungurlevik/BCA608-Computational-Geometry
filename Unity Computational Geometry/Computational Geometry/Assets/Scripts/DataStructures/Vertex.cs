using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex
{
    public Vector3 position;
    public HalfEdge halfEdge;
    public Triangle triangle;
    public Vertex prevVertex;
    public Vertex nextVertex;

    public bool isReflex;
    public bool isConvex;
    public bool isEar;
    public Vertex(Vector3 position)
    {
        this.position = position;
    }

    public Vector2 GetPos2D_XZ()
    {
        Vector2 pos_2d_xz = new Vector2(position.x, position.z);
        return pos_2d_xz;
    }
}
