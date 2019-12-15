using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dropdown : MonoBehaviour
{
    public int number;
    // Start is called before the first frame update
    void Start()
    {

       
        for(int i = 0; i < 5; i++)
        {
            if (i != number)
            {
                Dropdown.OptionData temp;
                temp = new Dropdown.OptionData(i.ToString());
                this.GetComponent<Dropdown>().options.Add(temp);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel()
    {
      int Level = int.Parse(GetComponent<Dropdown>().captionText.text.ToString());
         SceneManager.LoadScene(Level);


    }
}
