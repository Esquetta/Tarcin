using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuController : MonoBehaviour
{

    GameObject Levels,Locks;
    
    void Start()
    {

        Levels = GameObject.Find("Levels");
        Locks = GameObject.Find("Locks");

        for (int i = 0; i < Levels.transform.childCount; i++)
        {
            Levels.transform.GetChild(i).gameObject.SetActive(false);

        }
        for (int i = 0; i < Locks.transform.childCount; i++)
        {
            Locks.transform.GetChild(i).gameObject.SetActive(false);

        }



        //PlayerPrefs.DeleteAll();

        for (int i = 0; i < PlayerPrefs.GetInt("Level"); i++)
        {
            Levels.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
        
       
    }

    public void Button(int button)
    {
        if (button==1)
        {
            SceneManager.LoadScene("Game1");
        }
        else if (button==2)
        {
            for (int i = 0; i < Locks.transform.childCount; i++)
            {
                Locks.transform.GetChild(i).gameObject.SetActive(true);

            }
            for (int i = 0; i < Levels.transform.childCount; i++)
            {
                Levels.transform.GetChild(i).gameObject.SetActive(true);

            }

            for (int i = 0; i < PlayerPrefs.GetInt("Level"); i++)
            {
                Locks.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        else if(button==3)
        {
            Application.Quit();
        }
    }
    public void Level(int Level)
    {
        SceneManager.LoadScene(Level);
    }
}
