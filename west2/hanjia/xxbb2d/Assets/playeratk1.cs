﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeratk1 : MonoBehaviour
{
    public Vector3 dir;
    public float speed = 3;
    public float atk;
    public bool jisu = false;

    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    public void jisuon()
    {
        this.GetComponent<SpriteRenderer>().color = Color.blue;
        jisu = true;
    }
    void Update()
    {
     
        GetComponent<Rigidbody2D>().MovePosition(dir.normalized * speed * Time.deltaTime + this.transform.position);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
      
        if (collision.tag == "enemy" && collision.GetComponent<enemy>().insky <= 0 && Vector3.Distance(this.transform.position, collision.transform.position) <= 0.3) 
        {

            collision.GetComponent<enemy>().hp -= atk;
            if (jisu)
            {
               AudioClip jisu = Resources.Load<AudioClip>("jisu/jisu");
                AudioSource.PlayClipAtPoint(jisu, Camera.main.transform.position, 0.5f);
                collision.GetComponent<enemy>().stun.Add(0.5f);

            }
            Destroy(this.gameObject);
        }
        if (collision.tag == "obj")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }

    }
}
