﻿ using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icewall : MonoBehaviour
{
    public float time;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, time);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            collision.GetComponent<enemy>().speed /=4;

        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            other.GetComponent<enemy>().speed *= 4;

        }
    }
}
