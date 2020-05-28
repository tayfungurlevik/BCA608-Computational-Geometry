using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    [SerializeField]
    private Slider KVal;
   
    
    [SerializeField]
    private VoronoiGrid voronoiGrid;
    [SerializeField]
    private PointGenerator pointGenerator;

    

    public static event Action<int> OnKChanged = delegate { };
    public static event Action<int> OnGridXChanged = delegate { };
    public static event Action<int> OnGridYChanged = delegate { };
    public static event Action<int> OnSiteCountChanged = delegate { };
    
    private void OnEnable()
    {
        KVal.onValueChanged.AddListener(delegate { KValueChanged(); });
       
    }
    //private void Update()
    //{
    //    //Debug.Log(Input.mousePosition);
    //}

   

    private void KValueChanged()
    {
        OnKChanged?.Invoke((int)KVal.value);
    }
    
    
}
