using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomSlider : MonoBehaviour
{
    public Slider slider;

    public delegate void floatDelegate(float _f);
    public static event floatDelegate Event;
    public static void __Event(float _f) { if (Event != null) Event(_f); }

    public void UpdateZoom()
    {
        __Event(slider.value);
    }
}
