using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    [SerializeField]
    private PointPlaceholder pointPlaceholder;
    
    public List<PointPlaceholder> points;
    public  static event Action<Vector3,Color,int>  OnPointAdded = delegate { };
    public static event Action<List<PointPlaceholder>,int> OnPointRemoved = delegate { };
    private void Start()
    {
        points = new List<PointPlaceholder>();
    }
    
    public void RemovePoint(int index)
    {
        if (points.Count>0)
        {
            var sileneceknokta = points[index];
            points.RemoveAt(index);
            Destroy(sileneceknokta.gameObject);
            OnPointRemoved?.Invoke(points,index);
        }
        
    }
    public void AddPoint(Vector3 newPoint)
    {
       
        var nokta = Instantiate(pointPlaceholder, newPoint, Quaternion.identity);
        nokta.color= UnityEngine.Random.ColorHSV(0f, 1f, 0.4f, 1f, 0.5f, 1f);
        nokta.color.a = 0.5f;
        
        nokta.transform.Translate(Vector3.back);
        Debug.Log("Eklenen nokta" + nokta.transform.position);
        points.Add(nokta);
        nokta.Index = points.Count - 1;
        OnPointAdded?.Invoke(newPoint,nokta.color,points.Count-1);
       
    }

}
