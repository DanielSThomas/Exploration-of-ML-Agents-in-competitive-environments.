using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OddBall : MonoBehaviour
{
    public bool carried = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EliminationAgent>() != null && carried == false)
        {
            collision.GetComponent<EliminationAgent>().setCarry(true);

            this.transform.parent = collision.transform;

            carried = true;
        }
    }
}
