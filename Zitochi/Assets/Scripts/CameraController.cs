using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera thisCamera;
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float camerax, cameray;

    private void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {   if (target == null) {
            target = GameObject.Find("PVE Player").transform;
        }
        if (target)
        {
            Vector3 point = thisCamera.WorldToViewportPoint(target.position);
            Vector3 delta = target.position - thisCamera.ViewportToWorldPoint(new Vector3(.5f, 0.4f, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }

    }
}
