using System.Collections;
using System.Collections.Generic;
public class Triangle
{
    public Vertex v1, v2, v3;
    public HalfEdge halfEdge;

    public Triangle(Vertex v1, Vertex v2, Vertex v3)
    {
        this.v1 = v1;
        this.v2 = v2;
        this.v3 = v3;
    }
    public Triangle(HalfEdge halfEdge)
    {
        this.halfEdge = halfEdge;
    }
    public void ChangeOrientation()
    {
        Vertex tmp = this.v1;
        this.v1 = this.v2;
        this.v2 = tmp;


    }
}
