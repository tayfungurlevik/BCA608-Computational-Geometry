using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnPlayButtonPressed = delegate { };
    public void StartAnimation()
    {
        OnPlayButtonPressed?.Invoke();
    }
}
