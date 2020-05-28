using UnityEngine;

public class PointPlaceholder:MonoBehaviour
{
    
    public int scale = 50;
    private int oldK = 10;
    public Color color;
    public int Index;
    public int K { get { return oldK; } set { oldK = value; } }

    private void OnEnable()
    {
        ControlPanel.OnKChanged += ControlPanel_OnKChanged;
    }
    private void OnDisable()
    {
        ControlPanel.OnKChanged -= ControlPanel_OnKChanged;
    }
    private void ControlPanel_OnKChanged(int newK)
    {
        var oldPos = transform.position;
        var newPos = oldPos * newK / oldK;
        transform.position = newPos;
        oldK = newK;
    }

    private void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            scale++;
            transform.localScale = new Vector3(scale, scale, scale);
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            scale--;
            transform.localScale = new Vector3(scale, scale, scale);
        }
    }


}