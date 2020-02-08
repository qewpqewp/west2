using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float ghost;
    public float hp;
    public float maxhp;
    public float atk;
    public GameObject bullet;
    public float atkdelay;
    float atktime = 0;
    AudioClip click;
    AudioSource voice;
    int jisu=0;
    float buff=0;

    public GameObject ico1;
    public GameObject ico2;
    public GameObject r;
    public GameObject ico11;
    public GameObject ico21;
    public Skill T;
  
    public float speed;
    public Rigidbody2D rd;
    Animator ani;
    Dictionary<string, GameObject> febs =new Dictionary<string, GameObject>();
    Dictionary<string,Sprite> icos =new Dictionary<string, Sprite>();
    Dictionary<string, float> cds = new Dictionary<string, float>();
    Dictionary<string, float> nowcds = new Dictionary<string, float>();
    Dictionary<string, Sprite> icos1 = new Dictionary<string, Sprite>();
    Dictionary<string, int> voicenumber= new Dictionary<string, int>();

    List<string> skillnames = new List<string>();
    public GameObject ball1,ball2,ball3;
    public string D;
    public string F;
    [System.Serializable]
    public class Skill
    {
        public Queue<char> balls=new Queue<char>();
        public string Getskill()
        {
            
            if (balls.Count == 3)
            {
                int q=0;
                int w=0;
                int e=0;
                for(int i = 0; i < balls.Count; i++)
                {
                    if (balls.ElementAt(i) == 'Q')
                    {
                     
                        q++;
                    }
                    if (balls.ElementAt(i) == 'W')
                    {
                      
                        w++;
                    }
                    if (balls.ElementAt(i) == 'E')
                    {
                        
                        e++;
                    }
                }
                if (q == 1 && e == 1 && w == 1)
                {
                    return "bdao";
                }
                if(q==3 )
                {
                    return "jisu";
                }
                if (w == 3)
                {
                    return "emp";
                }
                if (e == 3)
                {
                    return "suns";
                }
                if (q == 2 && w == 1)
                {
                    return "ghost";
                }
                if(q==2 && e == 1)
                {
                    return "wall";
                }
                if(w==2 && q == 1)
                {
                    return "wind";
                }
                if(w==2 && e == 1)
                {
                    return "buff";
                }
                if(q==1 && e == 2)
                {
                    return "two";
                }
                if(w==1 && e == 2)
                {
                    return "ball";
                }




            }return "No";
        }

       public void Q()
        {
            if (balls.Count != 3)
            {
                balls.Enqueue('Q');
            }
            else
            {
                balls.Dequeue();
                balls.Enqueue('Q');
            }
        }
        public void W()
        {
            if (balls.Count != 3)
            {
                balls.Enqueue('W');
            }
            else
            {
                balls.Dequeue();
                balls.Enqueue('W');
            }
        }

        public void E()
        {
            if (balls.Count != 3)
            {
                balls.Enqueue('E');

            }
            else
            {
                balls.Dequeue();
                balls.Enqueue('E');
            }
        }


    }

    SpriteRenderer sr;
    int dir;
    public float lastx=1;
    public float lasty=0;
    void Start()
    {
        rd = this.GetComponent<Rigidbody2D>();
        ani = this.GetComponent<Animator>();
        sr = this.GetComponent<SpriteRenderer>();
        voice = this.GetComponent<AudioSource>();
        voice.clip = Resources.Load<AudioClip>("spawn/spawn" + (Random.Range(1, 5) + 1).ToString());
        voice.Play();
        LoadResources();
   
    }
    void LoadResources()
    {
        voicenumber["fail"] = 13;
        voicenumber["r"] = 17;
        voicenumber["buff"] = 5;
        voicenumber["wall"] = 5;
        voicenumber["ball"] = 7;
        voicenumber["emp"] = 10;
        voicenumber["suns"] = 5;
        voicenumber["bdao"] = 6;
        voicenumber["wind"] = 5;
        voicenumber["ghost"] = 5;
        voicenumber["jisu"] = 5;
        voicenumber["two"] = 6;
        cds["ghost"]= 1f / 22 * Time.deltaTime;
        cds["jisu"]= 1f / 10 * Time.deltaTime;
        cds["two"]= 1f / 15 * Time.deltaTime;
        cds["buff"] = 1f / 8 *Time.deltaTime;
        cds["wall"] = 1f / 12 * Time.deltaTime;
        cds["ball"] = 1f / 22 * Time.deltaTime;
        cds["emp"] = 1f / 15 * Time.deltaTime;
        cds["suns"] = 1f / 12 * Time.deltaTime;
        cds["bdao"] = 1f / 20 * Time.deltaTime;
        cds["wind"] = 1f / 15 * Time.deltaTime;
        cds["r"] = 1f / 1 * Time.deltaTime;
        nowcds["ghost"] = 1;
        nowcds["jisu"] = 1;
        nowcds["two"] = 1;
        nowcds["buff"] = 1;
        nowcds["wall"] = 1;
        nowcds["ball"] = 1;
        nowcds["emp"] = 1;
        nowcds["suns"] = 1;
        nowcds["bdao"] = 1;
        nowcds["wind"] = 1;
        nowcds["r"] = 1;
        click = Resources.Load<AudioClip>("clips/click");
        skillnames.Add("buff");
        skillnames.Add("wall");
        skillnames.Add("ball");
        skillnames.Add("emp");
        skillnames.Add("suns");
        skillnames.Add("bdao");
        skillnames.Add("wind");
        skillnames.Add("ghost");
        skillnames.Add("two");
        skillnames.Add("jisu");
        foreach (string name in skillnames)
        {
            febs.Add(name, Resources.Load<GameObject>(name + "/feb"));
            icos.Add(name,Resources.Load<Sprite>("ico/invoker/" + name));
            icos1.Add(name, Resources.Load<Sprite>("ico/invoker/" + name+"1"));

        }
        icos["Q"]= Resources.Load<Sprite>("ico/Q" );
        icos["W"] = Resources.Load<Sprite>("ico/W");
        icos["E"] = Resources.Load<Sprite>("ico/E");
        Debug.Log("loding success");

    }
     void cast(string name)
    {
        outghost();
        say(name);
        if (name == "two")
        {
            GameObject temp = (GameObject)Instantiate(febs[name]);

            temp.transform.position = this.transform.position + new Vector3(1, 0);
            GameObject temp1 = (GameObject)Instantiate(febs[name]);

            temp1.transform.position = this.transform.position + new Vector3(-1, 0);

        }
        if (name == "ghost")
        {
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("ghost/ghost"), Camera.main.transform.position, 0.5f);
            inghost();

        }
        if (name=="buff")
        { 
            GameObject temp = (GameObject)Instantiate(febs[name], this.transform);
            buff += 10;
            temp.transform.position = this.transform.position + new Vector3(0, 1);


        }
        if (name=="wall")
        {
            GameObject temp = (GameObject)Instantiate(febs[name]);
            temp.transform.Rotate(0, 0, -45 * dir);
            temp.transform.position = this.transform.position + new Vector3(lastx, lasty);


        }
        if (name=="suns")

        {
            GameObject[] temps=GameObject.FindGameObjectsWithTag("enemy");
            if (buff > 0)
            {
                int i = 0;
                foreach (GameObject temp in temps)
                {
                   
                    GameObject ss1 = (GameObject)Instantiate(febs[name]);
                    ss1.transform.position = temp.transform.position + new Vector3(0.3f, 0.3f, 0);
                    GameObject ss2 = (GameObject)Instantiate(febs[name]);
                    ss2.transform.position = temp.transform.position + new Vector3(-0.3f, -0.3f, 0);
                    if (i > 4)
                    {
                        ss1.GetComponent<AudioSource>().Stop();
                        ss2.GetComponent<AudioSource>().Stop();

                    }
                    else
                    {
                        i++;
                    }
                }
            }
            else
            {
                GameObject temp = GameObject.FindGameObjectWithTag("enemy");
                GameObject ss1 = (GameObject)Instantiate(febs[name]);
                ss1.transform.position = temp.transform.position;
            }
          
            


        }
        if (name=="bdao")
        {
            if (buff > 0)
            {   for(int i = 0; i < 8; i++)
                {
                   
                    GameObject temp = (GameObject)Instantiate(febs[name]);
                    temp.transform.position = this.transform.position + new Vector3(getx(i), gety(i));

                    temp.GetComponent<bdao>().dirx = getx(i);
                    temp.GetComponent<bdao>().diry = gety(i);
                    if (i != 0)
                    {
                        temp.GetComponent<AudioSource>().Stop();
                    }
                    temp.transform.Rotate(0, 0, -45 * i);
                }
                

            }
            else
            {
                GameObject temp = (GameObject)Instantiate(febs[name]);
                temp.transform.position = this.transform.position + new Vector3(lastx, lasty);
                temp.GetComponent<bdao>().dirx = lastx;
                temp.GetComponent<bdao>().diry = lasty;
                temp.transform.Rotate(0, 0, -45 * dir);
            }

        }
        if (name=="ball")
        {
            GameObject temp = (GameObject)Instantiate(febs[name]);
            temp.transform.position = this.transform.position + new Vector3(lastx, lasty);
            temp.GetComponent<fireball>().dirx = lastx;
            temp.GetComponent<fireball>().diry = lasty;

        }
        if (name=="wind")
        {
            GameObject temp = (GameObject)Instantiate(febs[name]);
            temp.transform.position = this.transform.position + new Vector3(lastx, lasty);
            temp.GetComponent<wind>().dirx = lastx;
            temp.GetComponent<wind>().diry = lasty;

        }
        if (name=="emp")
        {
            GameObject temp = (GameObject)Instantiate(febs[name]);
            temp.transform.position = this.transform.position + new Vector3(lastx, lasty);

        }
        if (name == "jisu")
        {
            if (jisu != 3)
            {
                jisu = 3;
            }
        }
    }
    void doatk()
    {
        
        outghost();
        GameObject temp = (GameObject)Instantiate(bullet);
        temp.transform.position = this.transform.position;
        temp.GetComponent<playeratk1>().dir = new Vector3(lastx,lasty);
        if (jisu != 0)
        {
            jisu--;
            temp.GetComponent<playeratk1>().jisuon();

        }

        temp.GetComponent<playeratk1>().atk = atk;
        if (buff != 0)
        {
            temp.GetComponent<playeratk1>().atk += 50;
        }
    }
     void keycheck()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (atktime > atkdelay)
            {
                doatk();
                atktime = 0;

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.LoadLevel(0);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            outghost();
            if (T.balls.Count == 0)
            {
                ball1.SetActive(true);

            }
            if (T.balls.Count == 1)
            {
                ball2.SetActive(true);

            }
            if (T.balls.Count == 2)
            {
                ball3.SetActive(true);

            }
            AudioSource.PlayClipAtPoint(click, Camera.main.transform.position);
            T.Q();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            outghost();
            if (T.balls.Count == 0)
            {
                ball1.SetActive(true);

            }
            if (T.balls.Count == 1)
            {
                ball2.SetActive(true);

            }
            if (T.balls.Count == 2)
            {
                ball3.SetActive(true);

            }
            AudioSource.PlayClipAtPoint(click, Camera.main.transform.position);
            T.W();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            outghost();
            if (T.balls.Count == 0)
            {
                ball1.SetActive(true);

            }
            if (T.balls.Count == 1)
            {
                ball2.SetActive(true);

            }
            if (T.balls.Count == 2)
            {
                ball3.SetActive(true);

            }
            AudioSource.PlayClipAtPoint(click, Camera.main.transform.position);
            T.E();
        }
        if (Input.GetKeyDown(KeyCode.R) && r.GetComponent<Image>().fillAmount >= 1)
        {
            outghost();
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("clips/invoke"), Camera.main.transform.position, 0.3f);
            r.GetComponent<Image>().fillAmount = 0;
  
            string skill = T.Getskill();
            if (skill == "No" || skill == D || skill == F)
            {
                say("fail");
            }
            else
            {
                say("r");
                ico21.GetComponent<Image>().sprite = ico11.GetComponent<Image>().sprite;
                ico11.GetComponent<Image>().sprite = icos1[skill];

                ico2.GetComponent<Image>().sprite = ico1.GetComponent<Image>().sprite;
                ico1.GetComponent<Image>().sprite = icos[skill];
                F = D;
                D = skill;
            }

        }

        if (Input.GetKeyDown(KeyCode.D) && ico1.GetComponent<Image>().fillAmount >= 1)
        {
            if (D != "error")
            {
                nowcds[D] = 0;
                cast(D);
            }
        }
        if (Input.GetKeyDown(KeyCode.F) && ico2.GetComponent<Image>().fillAmount >= 1)
        {
            if (F != "error")
            {
                nowcds[F] = 0;
                cast(F);
            }
        }
    }
     void say(string name)
    {

        

        if (voice.clip)
        {

            if (!voice.isPlaying || (voice.time >= 0.3 * voice.clip.length && name!="fail" &&name != "r"))
        {
            
            voice.clip = Resources.Load<AudioClip>("clips/"+name+ Random.Range(1, voicenumber[name]+1));
            voice.Play();
        }
        }
        else
        {
            voice.clip = Resources.Load<AudioClip>("clips/" + name + Random.Range(1, voicenumber[name] + 1));
            voice.Play();
        }

    }
    void cdcheck()
    {
        r.GetComponent<Image>().fillAmount += cds["r"];
        if (D != "")
            ico1.GetComponent<Image>().fillAmount = nowcds[D];
        if (F != "")
            ico2.GetComponent<Image>().fillAmount = nowcds[F];

        foreach (string name in skillnames)
        {
            if (nowcds[name] < 1)
            {
                nowcds[name] += cds[name];
            }
        }
    }
     void move()
    {
       
        float moveX = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float moveY = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        UpdateDir(moveX, moveY);
        if (moveX != 0 || moveY != 0)
        {
            ani.SetInteger("state", 1);
            if (moveX < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

        }
        else
        {
            ani.SetInteger("state", 0);
        }
        Vector3 moveto;
        if (dir == 1 || dir == 3 || dir == 5 || dir == 7)
        {
            moveto = (new Vector3(moveX, moveY, 0) / 1.414f) + transform.position;
        }
        else
        {
            moveto = new Vector3(moveX, moveY, 0) + transform.position;
        }
        rd.MovePosition(moveto);
    }
     void getball()
    {

        if (T.balls.Count == 3) {
            ball1.GetComponent<Image>().sprite = icos[T.balls.ElementAt(2).ToString()];
            ball2.GetComponent<Image>().sprite = icos[T.balls.ElementAt(1).ToString()];
            ball3.GetComponent<Image>().sprite = icos[T.balls.ElementAt(0).ToString()];
            return; }
    if(T.balls.Count == 2)
        {
            ball1.GetComponent<Image>().sprite = icos[T.balls.ElementAt(1).ToString()];
            ball2.GetComponent<Image>().sprite = icos[T.balls.ElementAt(0).ToString()];
            return;
        }
        if (T.balls.Count == 1)
        {
           
            ball1.GetComponent<Image>().sprite = icos[T.balls.ElementAt(0).ToString()];
        }


    }
     void checkhp()
    {
        if (hp <= 0)
        {
            this.transform.DetachChildren();
            Application.LoadLevel(0);
            Destroy(this.gameObject);
        }
    }
    
    void Update()
    {

        keycheck();
        cdcheck();
        move();
        getball();
        checkhp();
        atktime += Time.deltaTime;
        if (buff > 0)
        {
            atktime += Time.deltaTime;
            buff -= Time.deltaTime;
        }
            ghostcheck();

    }
    void outghost()
    {
        this.tag = "player";
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }
    void inghost()
    {
        this.GetComponent<SpriteRenderer>().color = Color.gray;
        ghost += 10;
        this.tag = "Untagged";
        GameObject[] temps= GameObject.FindGameObjectsWithTag("enemy");
        foreach(GameObject temp in temps)
        {
            temp.GetComponent<enemy>().target = null;
        }
    }

    void ghostcheck()
    {
        if (ghost > 0)
        {
            ghost -= Time.deltaTime;
            if (ghost <= 0)
            {
                outghost();
            }
        }
    }
    void UpdateDir(float x, float y)
    {
  
        if (x <0 && y >0)
        {
            lastx = -1;
            lasty = 1;
        
            dir = 7;
        }
        if (x == 0 && y>0)
        {
            lastx = 0;
            lasty = 1;
            dir = 0;
        }
        if (x >0 && y >0)
        {
            lastx = 1;
            lasty = 1;
            dir = 1;
        }
        if (x <0 && y == 0)
        {
            lastx = -1;
            lasty = 0;
            dir = 6;
        }
        if (x>0 && y == 0)
        {
            lastx = 1;
            lasty = 0;
            dir = 2;
        }
        if (x <0 && y <0)
        {
            lastx = -1;
            lasty = -1;
            dir = 5;
        }
        if (x == 0 && y <0)
        {
            lastx = 0;
            lasty = -1;
            dir = 4;
        }
        if (x >0 && y<0)
        {
            lastx = 1;
            lasty = -1;
            dir = 3;
        }

    }
    int getx(int dir)
    {
        if (dir == 7)
        {
            return -1;
            
        }
        if (dir == 0)
        {
            return 0;
            
        }
        if (dir == 1)
        {
            return 1;
           
        }
        if (dir==6)
        {
            return -1;
        }
        if (dir==2)
        {
            return 1;
        }
        if (dir==5)
        {
            return -1;
        }
        if (dir == 4)
        {
            
            return 0;
        }
        if (dir == 3)
        {
            return 1;
            
        }
        return 0;
    }
    int gety(int dir)
    {
        if (dir ==7)
        {
            return 1;
        }
        if (dir ==0)
        {
            return 1;
        }
        if (dir==1)
        {
            return 1;
        }
        if (dir==6)
        {
            return 0;
        }
        if (dir==2)
        {
            return 0;
        }
        if (dir == 5)
        {
            return -1;
        }
        if (dir == 4)
        {
            return -1;
        }
        if (dir == 3)
        {
            return -1;
        }
        return 0;
    }

}
