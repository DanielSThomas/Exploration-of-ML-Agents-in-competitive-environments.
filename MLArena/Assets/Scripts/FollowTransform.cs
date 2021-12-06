using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    public Transform followTarget;
    

    // Start is called before the first frame update
    private void Update()
    {
        this.transform.position = followTarget.TransformPoint(Vector3.zero);
        //this.transform.rotation = followTarget.rotation * Quaternion.Euler(Vector3.zero);
    }

    public void Map()
    {
               
    }

}
