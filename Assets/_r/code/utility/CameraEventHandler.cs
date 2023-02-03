using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEventHandler : MonoBehaviour
{
    public Camera cam;
    public float sliderRange = 70f;
    public float fovMin      = 20f;
    public float fovMax      = 70f;

    public void __init()
    {
        if (cam == null) this.gameObject.GetComponent<Camera>();
    }

    private void OnEnable()
    {
        __init();
        SubscribeToEvents(true);
    }

    private void OnDisable()
    {
        SubscribeToEvents(false);
    }

    private void SubscribeToEvents(bool state)
    {
        if(state)
        {
            CameraZoomSlider.Event += SetFieldOfView;
        }
        else
        {
            CameraZoomSlider.Event += SetFieldOfView;
        }
    }

    private void SetFieldOfView(float _value)
    {
        cam.fieldOfView = Mathf.Clamp(_value * sliderRange, fovMin, fovMax);
    }
}
