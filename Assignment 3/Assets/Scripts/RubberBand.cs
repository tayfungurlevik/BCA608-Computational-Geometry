using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberBand : MonoBehaviour
{
    [SerializeField]
    private RubberPiece rubberPiece;
    private List<RubberPiece> rubberPieces=new List<RubberPiece>();
    private void OnEnable()
    {
        GameManager.OnPlayButtonPressed += GameManager_OnPlayButtonPressed;
    }

    private void GameManager_OnPlayButtonPressed()
    {
        var left = Instantiate(rubberPiece, new Vector3(-5, 0.5f, 0),Quaternion.identity);
        left.speed = new Vector3(1, 0, 0);
        left.initialLength = 10;
        left.startPoint = new Vector3(0, 0, -left.initialLength / 2);
        left.endPoint = new Vector3(0, 0, +left.initialLength / 2);
        rubberPieces.Add(left);
        var right = Instantiate(rubberPiece, new Vector3(5, 0.5f, 0), Quaternion.identity);
        right.speed = new Vector3(-1, 0, 0);
        right.initialLength = 10;
        right.startPoint = new Vector3(0, 0, -right.initialLength / 2);
        right.endPoint = new Vector3(0, 0, +right.initialLength / 2);
        rubberPieces.Add(right);
        var top = Instantiate(rubberPiece, new Vector3(0, 0.5f, 5), Quaternion.identity);
        top.speed = new Vector3(0, 0, -1);
        top.initialLength = 10;
        top.startPoint = new Vector3(-top.initialLength / 2, 0,0 );
        top.endPoint = new Vector3(top.initialLength / 2, 0, 0);
        rubberPieces.Add(top);
        var bottom = Instantiate(rubberPiece, new Vector3(0, 0.5f, -5), Quaternion.identity);
        bottom.speed = new Vector3(0, 0, 1);
        bottom.initialLength = 10;
        bottom.startPoint = new Vector3(-bottom.initialLength / 2, 0, 0);
        bottom.endPoint = new Vector3(bottom.initialLength / 2, 0, 0);
        rubberPieces.Add(bottom);
    }

    private void OnDisable()
    {
        GameManager.OnPlayButtonPressed -= GameManager_OnPlayButtonPressed;
    }
}
