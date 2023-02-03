using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public bool logging;
	public string inThisScene;
	public List<string> scenesConnectedTo;

    public delegate void doorDelegate(Door _d);
    public static event doorDelegate DoorEvent;
    public static void __DoorEvent(Door _d) { if (DoorEvent != null) DoorEvent(_d); }
    
    private void OnTriggerEnter(Collider c)
    {
        if (logging) slog.inst.Print($"Collider beloging to {c.gameObject.name} has entered Door in {inThisScene} scene.");
        if (c.CompareTag("Player"))
        {
          	__DoorEvent(this);
        }
    }
}
