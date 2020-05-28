using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class PointGenerator : MonoBehaviour
{
    [SerializeField]
    private PointPlaceholder pointPlaceholder;
    
    public Dictionary<int,PointPlaceholder> points;
    public  static event Action<Vector3,Color,int>  OnPointAdded = delegate { };
    private int k=10;
    private int counter=0;
    private void OnEnable()
    {
        ControlPanel.OnKChanged += ControlPanel_OnKChanged;
    }

    private void ControlPanel_OnKChanged(int obj)
    {
        k = obj;
    }

    private void Start()
    {
        points = new Dictionary<int, PointPlaceholder>();
    }
    public int RemovePoint(int key)
    {
        counter++;
        points.Remove(key);
        return counter;
    }
    
    public void AddPoint(Vector3 newPoint)
    {
        counter++;
        var nokta = Instantiate(pointPlaceholder, newPoint, Quaternion.identity);
        nokta.color= UnityEngine.Random.ColorHSV(0f, 1f, 0.4f, 1f, 0.5f, 1f);
        nokta.color.a = 0.5f;
        
        nokta.transform.Translate(Vector3.back);
        //Debug.Log("Eklenen nokta" + nokta.transform.position);
        nokta.Index = counter;
        nokta.K = k;
        points.Add(counter, nokta);
        
        OnPointAdded?.Invoke(newPoint,nokta.color, nokta.Index);
       
    }
    public void MovePoint(int key,Vector3 newPosition)
    {
        points[key].transform.position = newPosition;
        points[key].transform.Translate(Vector3.back);
    }

}
