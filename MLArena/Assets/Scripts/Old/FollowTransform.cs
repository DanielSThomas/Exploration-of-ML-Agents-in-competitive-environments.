using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothing;
    private Camera cam;

    [SerializeField] private Vector3 offset;

    [SerializeField] private float camDistance;

    //public float threshold;

    // Use this for initialization
    void Start()
    {
        cam = GetComponent<Camera>();
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {           
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
                 
    }


}
