using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;

[ExecuteInEditMode]
public class SimplePolygon : MonoBehaviour
{
    public enum Yontemler { Yontem1,Yontem2 };
    public Yontemler secilenYontem;
    public PolygonInfo polygonInfo;
    [HideInInspector]
    public List<Vector3> points;
    [Header("Polygon Özellikleri")]
    public int noktaSayisi = 4;
    [SerializeField]
    [Range(0,500)]
    private float width, heigth;
    
    
    [SerializeField]
    private TextAsset dosya;
    private Segment[] segments;
    private Vector3 q;
    private Vector3[] sortedPoints;
    private LineRenderer lineRenderer;

    public Segment[] Segments { get => segments; }
    public Vector3 Q { get => q; }

    public event Action<Vector3[],Vector3,Segment[]> OnPolygonGenerated = delegate { };
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
    }
    public void DosyadanOku()
    {
        string fs = dosya.text;
        string[] lines = Regex.Split(fs, "\n|\r\n");
        //noktaSayisi = int.Parse(lines[0]);
        string qString = lines[0];
        noktaSayisi = 0;
        q = new Vector3();
        q.x = float.Parse(qString.Split(' ')[0]);
        q.y = float.Parse(qString.Split(' ')[1]);
        q.z = 0;

        points = new List<Vector3>();
        for (int i = 1; i < lines.Length; i++)
        {
            var vals = lines[i].Split(' ');

            points.Add(new Vector3(float.Parse(vals[0]), float.Parse(vals[1]), 0));
            noktaSayisi++;
        }
        lineRenderer.positionCount = noktaSayisi;
        UpdateLineRenderer(points.ToArray());
    }

    public void GeneratePolygon1()
    {
        lineRenderer.positionCount = noktaSayisi;
        points = new List<Vector3>();
        q = Vector3.zero;
        for (int i = 0; i < noktaSayisi; i++)
        {
            Vector3 p = new Vector3();
            if (i % 3 == 0)
            {
                p.x = i * 4;
                p.y = i * i * 2;
                p.z = 0;
            }
            else
            {
                p.x = i * 7;
                p.y = -i * i - 3;
                p.z = 0;
            }

            points.Add(p);

        }
        
        UpdateLineRenderer(points.ToArray());
        

    }
    public void GeneratePolygon2()
    {
        lineRenderer.positionCount = noktaSayisi;
        q = Vector3.zero;
        points = new List<Vector3>();
        for (int i = 0; i < noktaSayisi; i++)
        {
            var nokta= new Vector3();
            nokta.x = UnityEngine.Random.Range(0, width);
            nokta.y = UnityEngine.Random.Range(0, heigth);
            nokta.z = 0;
            points.Add(nokta);
        }
        Vector3 merkezNokta = MerkezBul(points.ToArray());
        sortedPoints = points.OrderBy(t => t.NoktaAcisi(merkezNokta)).ToArray();
        var acilar = new double[noktaSayisi];
        for (int i = 0; i < noktaSayisi; i++)
        {
            acilar[i] = sortedPoints[i].NoktaAcisi(merkezNokta);
        }
        UpdateLineRenderer(sortedPoints);
       

    }

    private Vector3 MerkezBul(Vector3[] points)
    {
        float sumX = 0, SumY = 0;
        for (int i = 0; i < points.Length; i++)
        {
            sumX += points[i].x;
            SumY += points[i].y;
        }
        return new Vector3(sumX / points.Length, SumY / points.Length);
    }

    private void UpdateLineRenderer(Vector3[] noktalar)
    {
        SegmentleriOlustur(noktalar);
        if (q==Vector3.zero)
        {
            q = QNoktasiOlustur();
        }
        
        lineRenderer.SetPositions(noktalar);
        polygonInfo.Segments = segments;
        polygonInfo.q = q;
        OnPolygonGenerated?.Invoke(noktalar,q,segments);
    }

    private Vector3 OrtaNoktaBul(Vector3[] points)
    {
        float sumX = 0;
        float sumY = 0;
        for (int i = 0; i < points.Length; i++)
        {
            sumX += points[i].x;
            sumY += points[i].y;

        }
        return new Vector3(sumX / points.Length, sumY / points.Length, 0);
    }
    private void SegmentleriOlustur(Vector3[] noktalar)
    {
        segments = new Segment[noktalar.Length];
        for (int i = 1; i < noktalar.Length; i++)
        {
            Segment segment = new Segment(noktalar[i - 1], noktalar[i]);
            segments[i - 1] = segment;
        }
        Segment sonSegment = new Segment(noktalar[noktalar.Length - 1], noktalar[0]);
        segments[Segments.Length - 1] = sonSegment;
    }
    private Vector3 QNoktasiOlustur()
    {
        Vector3 rastgeleQ = new Vector3();
        rastgeleQ.x = UnityEngine.Random.Range(0, width);
        rastgeleQ.y = UnityEngine.Random.Range(0, heigth);
        rastgeleQ.z = 0;
        if (PoligonIcindemi(rastgeleQ))
        {
            return rastgeleQ;
        }
        else
            return QNoktasiOlustur();
    }

    private bool PoligonIcindemi(Vector3 rastgeleQ)
    {
        Isin sagIsin = new Isin(rastgeleQ, Vector3.right, width + 10);
        int kesismeSayisi = 0;
        foreach (var item in segments)
        {
            var (kesisti, kesisimNoktasi) = sagIsin.CheckIntersection(item);
            if (kesisti)
            {
                kesismeSayisi++;
            }
        }
        if (kesismeSayisi%2==1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
