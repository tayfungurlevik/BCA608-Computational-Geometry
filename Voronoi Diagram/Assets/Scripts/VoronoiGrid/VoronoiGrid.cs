using PixelsForGlory.ComputationalSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class VoronoiGrid : MonoBehaviour
{

    public VoronoiKare[,] grid;
    public int x = 50;
    
    public int k = 10;
    

    [SerializeField]
    private VoronoiKare voronoiPrefab;
    [SerializeField]
    private PointGenerator pointGenerator;

    [SerializeField]
    private LayerMask voronoiLayer;
    
    private Camera camera;
   
    
    private void OnEnable()
    {
       
        
        ControlPanel.OnKChanged += ControlPanel_OnKChanged;
        PointGenerator.OnPointAdded += PointGenerator_OnPointAdded;
        PointGenerator.OnPointRemoved += PointGenerator_OnPointRemoved;
        
    }

    private void PointGenerator_OnPointRemoved(List<PointPlaceholder> points,int removedIndex)
    {
        if (points.Count==0)
        {
            foreach (var item in grid)
            {
                item.color = UnityEngine.Color.red;
                item.SiteIndex = -1;
                item.GenerateMesh();
            }
        }
        else
        {
            var deletedSitesSquares = grid.Cast<VoronoiKare>().Where(t => t.SiteIndex == removedIndex);
            foreach (var item in deletedSitesSquares)
            {
                var enyakinSite = pointGenerator.points.OrderBy(t => Vector3.Distance(t.transform.position, item.center)).FirstOrDefault();
                item.color = enyakinSite.color;
                item.SiteIndex = enyakinSite.Index;
                item.GenerateMesh();
            }
        }
    }

    private void PointGenerator_OnPointAdded(Vector3 position, UnityEngine.Color color, int siteIndex)
    {
        if (siteIndex ==0)
        {
            //Eğer ilk voronoi noktasi ise tum kareleri boya
            foreach (var item in grid)
            {
                item.color = color;
                item.GenerateMesh();
            }
        }
        else
        {
            //Her bir kare icin
            foreach (var item in grid)
            {
                //En yakindaki voronoinoktasinibul
                var enyakinSite = pointGenerator.points.OrderBy(t => Vector3.Distance(t.transform.position, item.center)).FirstOrDefault();
                item.color = enyakinSite.color;
                item.SiteIndex = siteIndex;
                item.GenerateMesh();
            }
        }
    }

    private void OnDisable()
    {
        ControlPanel.OnKChanged -= ControlPanel_OnKChanged;
        PointGenerator.OnPointAdded -= PointGenerator_OnPointAdded;
    }
    private void Start()
    {
        camera = Camera.main;
        GenerateGrid();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AddSiteToDiagram();
        }
        if (Input.GetMouseButtonDown(1))
        {
            RemoveSiteToDiagram();
        }
    }

    private void RemoveSiteToDiagram()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100, voronoiLayer)) ;
        {
            if (hitInfo.collider == null)
            {
                return;
            }
            var kare = hitInfo.collider.GetComponent<VoronoiKare>();
            if (kare != null)
            {
                //nokta verisini al
                pointGenerator.RemovePoint(kare.SiteIndex);
                
                //o noktaya bir voronoi noktasi ata
            }
        }
    }

    private void AddSiteToDiagram()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 100, voronoiLayer)) ;
        {
            if (hitInfo.collider == null)
            {
                return;
            }
            var kare = hitInfo.collider.GetComponent<VoronoiKare>();
            if (kare != null)
            {
                //nokta verisini al
                Vector3 tiklananNokta = hitInfo.point;
                pointGenerator.AddPoint(tiklananNokta);
               // Debug.Log("Tıklanan nokta="+tiklananNokta);
                //o noktaya bir voronoi noktasi ata
            }
        }
    }


    private void ControlPanel_OnKChanged(int val)
    {
        k = val;
        foreach (var item in grid)
        {
            var enyakinSite = pointGenerator.points.OrderBy(t => Vector3.Distance(t.transform.position, item.center)).FirstOrDefault();
            item.color = enyakinSite.color;
            
            item.GenerateMesh();
        }
        //KillGrid();
        
        //GenerateGrid();
    }

   


    



    private void GenerateGrid()
    {
        grid = new VoronoiKare[x, x];
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < x; j++)
            {
                Vector3 centerOfCell = new Vector3();
                centerOfCell.x = k * i;
                centerOfCell.y = k * j;
                centerOfCell.z = 0;

                //grid[i, j] = new VoronoiSquaredCell(centerOfCell, k);
                grid[i, j] = Instantiate(voronoiPrefab, centerOfCell, Quaternion.identity);
                grid[i, j].width = k;
                grid[i, j].i = i;
                grid[i, j].j = j;
                grid[i, j].center = centerOfCell;
                grid[i, j].color = UnityEngine.Color.red;
                
            }
        }

    }
    private void KillGrid()
    {
        foreach (var item in grid)
        {
            Destroy(item);
        }
    }
    
}

