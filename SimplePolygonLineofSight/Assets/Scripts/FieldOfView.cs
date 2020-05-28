using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{


    public PolygonInfo polygonInfo;

    private Segment closestSegment;
    private float t;
    float tmax = 1000;
    private Segment[] segments;
    private Vector3[] SortedEndpoints;
    [SerializeField]
    private float angleTolerace = 1f;
    private MeshFilter meshFilter;
    private Mesh mesh;
    private List<Vector3> vertices;
    private List<int> tris;
    //private List<Vector3> normals;
    int triCounter = 0;
    private int rayCounter = 0;
    private Isin ray, rayLeft, rayRight;
    private void Start()
    {
        segments = polygonInfo.Segments;
        closestSegment = segments[0];
        meshFilter = GetComponent<MeshFilter>();
        mesh = new Mesh();
        meshFilter.mesh = mesh;

        vertices = new List<Vector3>();
        tris = new List<int>();
       // normals = new List<Vector3>();

    }
    private void Update()
    {
        mesh.Clear();
        tris.Clear();
        vertices.Clear();
        //normals.Clear();
        rayCounter = 0;
        triCounter = 0;
        var sortedSegments = segments.OrderBy(t => t.EndPoint.NoktaAcisi(transform.parent.position)).ToList();
        vertices.Add(transform.localPosition);
        
        //Adım2: sıralı endpointleri bir loop ile gezin
        foreach (var item in sortedSegments)
        {
            
            rayLeft = new Isin(transform.parent.position, item.EndPoint - transform.parent.position, 1000);
            ray = new Isin(transform.parent.position, item.EndPoint - transform.parent.position, 1000);
            rayRight = new Isin(transform.parent.position, item.EndPoint - transform.parent.position, 1000);
            rayLeft.color = Color.red;
            rayRight.color = Color.green;
            ray.color = Color.yellow;
            rayLeft.Rotate(angleTolerace);
            rayRight.Rotate(-angleTolerace);
            
            var wallsRight = rayRight.KestigiSegmentler(sortedSegments);
            foreach (var wall in wallsRight)
            {
                //En yakın duvarı bul
                var (kesismeDurumu, kesismeNoktasi) = rayRight.CheckIntersection(wall);
                if (kesismeDurumu && kesismeNoktasi != Vector3.zero)
                {
                    var distance = Vector3.Distance(kesismeNoktasi, transform.parent.position);
                    if (distance < rayRight.distance)
                    {
                        closestSegment = wall;
                        rayRight.distance = distance;
                    }


                }
            }
            rayRight.DrawRay();
            
            
            var wallsRay = ray.KestigiSegmentler(sortedSegments);
            if (wallsRay.Count == 0)
            {
                ray.distance = Vector3.Distance(transform.parent.position, item.EndPoint);
            }
            else
            {


                foreach (var wall in wallsRay)
                {
                    //En yakın duvarı bul
                    var (kesismeDurumu, kesismeNoktasi) = ray.CheckIntersection(wall);
                    if (kesismeDurumu && kesismeNoktasi != Vector3.zero)
                    {
                        var distance = Vector3.Distance(kesismeNoktasi, transform.parent.position);
                        if (distance < ray.distance)
                        {
                            closestSegment = wall;
                            ray.distance = distance;
                        }


                    }
                }
            }
            ray.DrawRay();

            
            
            
            var wallsLeft = rayLeft.KestigiSegmentler(sortedSegments);
            
            foreach (var wall in wallsLeft)
            {
                //En yakın duvarı bul
                var (kesismeDurumu, kesismeNoktasi) = rayLeft.CheckIntersection(wall);
                if (kesismeDurumu && kesismeNoktasi != Vector3.zero)
                {
                    var distance = Vector3.Distance(kesismeNoktasi, transform.parent.position);
                    if (distance < rayLeft.distance)
                    {
                        closestSegment = wall;
                        rayLeft.distance = distance;
                    }


                }
            }
            rayLeft.DrawRay();
            tris.Add(0);
            vertices.Add(transform.localPosition + rayRight.direction.normalized * rayRight.distance);//1
            vertices.Add(transform.localPosition + ray.direction.normalized * ray.distance);//2
            vertices.Add(transform.localPosition + rayLeft.direction.normalized * rayLeft.distance);//3
            tris.Add(2 + triCounter);
            
            tris.Add(1 + triCounter);
            tris.Add(0);
            
            tris.Add(3 + triCounter);
            tris.Add(2 + triCounter);
            triCounter += 3;
            //if (rayCounter>0)
            //{
            //    tris.Add(0);
            //    tris.Add(3 + triCounter * rayCounter);
            //    tris.Add(3 + triCounter * (rayCounter-1));
            //}
            rayCounter++;

        }
        mesh.vertices = vertices.ToArray();
        //AralarıDoldur
        for (int i = 0; i < rayCounter; i++)
        {
            tris.Add(0);
            tris.Add(0+1 + i * 3);
            tris.Add(0+i * 3);
        }
        tris.Add(0);
        tris.Add(1);
        tris.Add(0  + rayCounter * 3);
        //Debug.Log(rayCounter);
        mesh.triangles = tris.ToArray();
        //mesh.normals = normals.ToArray();
        mesh.RecalculateNormals();
        
        
    }


    private void SortEndPoints(List<Segment> sortedSegments)
    {
        SortedEndpoints = new Vector3[segments.Length];
        int n = 0;
        foreach (var item in sortedSegments)
        {
            SortedEndpoints[n] = item.EndPoint;
        }
    }
}
