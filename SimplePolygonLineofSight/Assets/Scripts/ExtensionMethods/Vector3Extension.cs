using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Extension 
{
    public static float NoktaAcisi(this Vector3 point, Vector3 merkez)
    {
        var temp = new Vector3();
        temp.x = point.x - merkez.x;
        temp.y= point.y - merkez.y;
        float aci = Mathf.Atan2(temp.y, temp.x) * 180 / Mathf.PI;
        if (aci < 0)
        {
            return 2 * 180 - Mathf.Abs(aci);
        }
        else
            return aci;
    }
    
}
