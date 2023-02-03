using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovementLoop : MonoBehaviour
{
    public Vector3 start, end;
    private Vector3 target, next, last;
    private float step;
    void Start()
    {
        if (start == Vector3.zero) start = transform.position; 
        step   =  5f * Time.deltaTime;
        target = end;
        last   = end;
        next   = start;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            target = next;
            next   = last;
            last   = target;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }

}
