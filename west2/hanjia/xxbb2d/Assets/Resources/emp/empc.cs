 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class empc : MonoBehaviour
{
    public float time;
    public float boomtime;
    float Nowtime=0;
     AudioClip boom;
   public  List<GameObject> enemyin = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        boom = Resources.Load<AudioClip>("emp/emp_discharge");
        Destroy(this.gameObject, time);
        
    }

    // Update is called once per frame
    void Update()
    {
        Nowtime += Time.deltaTime;
        if (Nowtime > boomtime)
        {

            boomtime = 999;
            foreach (GameObject x in enemyin)
            {
                if(x.GetComponent<enemy>().insky <= 0)
                x.GetComponent<enemy>().hp -= 200;
            }
            AudioSource.PlayClipAtPoint(boom, Camera.main.transform.position,0.3f);



        }
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
}
