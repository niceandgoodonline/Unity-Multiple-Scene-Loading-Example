using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameCap : MonoBehaviour
{
    public int targetFPS = 30;
    public void OnEnable()
    {
        Application.targetFrameRate = targetFPS;
    }
}
