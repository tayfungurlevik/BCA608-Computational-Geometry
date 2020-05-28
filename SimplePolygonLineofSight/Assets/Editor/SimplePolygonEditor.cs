using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SimplePolygon))]
public class SimplePolygonEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        SimplePolygon simplePolygon = target as SimplePolygon;
       
        GUILayout.Space(20f);
        if (GUILayout.Button("Polygon Oluştur"))
        {
            switch (simplePolygon.secilenYontem)
            {
                case SimplePolygon.Yontemler.Yontem1:
                    simplePolygon.GeneratePolygon1();
                    break;
                case SimplePolygon.Yontemler.Yontem2:
                    simplePolygon.GeneratePolygon2();
                    break;
                default:
                    break;
            }
            
        }
        GUILayout.Space(20f);
        if (GUILayout.Button("Dosyadan Oku"))
        {
            simplePolygon.DosyadanOku();
        }

    }
}
