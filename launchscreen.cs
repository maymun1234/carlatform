using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class launchscreen : MonoBehaviour
{


    public GameObject[] HUD;
    public GameObject[] mainmenu;

    public GameObject gsameoverscreen;


    void awake(){
        Time.timeScale = 0.0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        gsameoverscreen.SetActive(false);
        Time.timeScale = 0.0f;
        if (mainmenu != null)
        {
            foreach (GameObject menuElement in mainmenu)
            {
                menuElement.SetActive(true);
            }
        }

        if (HUD != null)
        {
            foreach (GameObject menuElement in HUD)
            {
                menuElement.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void debugtest(){
        Debug.Log("gbgfngfnn");
        Time.timeScale = 1.0f;


        if (mainmenu != null)
        {
            foreach (GameObject menuElement in mainmenu)
            {
                menuElement.SetActive(false);
            }
        }

        if (HUD != null)
        {
            foreach (GameObject menuElement in HUD)
            {
                menuElement.SetActive(true);
            }
        }

 
    }
}
