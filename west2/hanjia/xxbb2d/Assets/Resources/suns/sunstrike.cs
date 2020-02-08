 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sunstrike : MonoBehaviour
{
    public float time;
    public float boomtime;
    float Nowtime=0;
    public float atk=0;
     AudioClip boom;
    List<GameObject> enemyin = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        boom = Resources.Load<AudioClip>("suns/sunstrike_ignite");
        Destroy(this.gameObject, time);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            enemyin.Add(collision.gameObject);

        }
   
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "enemy")
        {
            enemyin.Remove(other.gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        Nowtime += Time.deltaTime;
        if (Nowtime > boomtime)
        {
            foreach (GameObject x in enemyin)
            {
                if (x.GetComponent<enemy>().insky <= 0)
                    x.GetComponent<enemy>().hp -=400;
            }
            boomtime = 999;
            AudioSource.PlayClipAtPoint(boom, Camera.main.transform.position, 0.5f);



        }
    }
}
