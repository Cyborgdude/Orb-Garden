using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChangeManager : BaseOrbGameScript
{
    private Transform destination;
    [SerializeField]
    private Animator fade;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void changeLevel(int levelNum, int doorNum, float distanceDiff)
    {
   
        string levelName = "GardenHub";

        switch(levelNum)
        {
            case 0:
                //Set Level
                levelName = "Tutorial";
                break;
            case 1:
                levelName = "GardenHub";
                break;
            case 2:
                levelName = "Platformer1";
                break;
            case 3:
                levelName = "Platformer2";
                break;
            case 4:
                levelName = "Blaster1";
                break;  
            case 5:
                levelName = "Blaster2";
                break;
            case 6:
                levelName = "Blaster3";
                break;
            case 7:
                levelName = "Launcher1";
                break;
            case 8:
                levelName = "Purple1";
                break;
            case 9:
                levelName = "Blue1";
                break;
            case 10:
                levelName = "Blue2";
                break;
            case 11:
                levelName = "Final1";
                break;
            case 12:
                levelName = "Final2";
                break;
            case 13:
                levelName = "Purple2";
                    break;
        }

        //Load Level
        StartCoroutine(LoadLevel(levelName));

        StartCoroutine(TransportPlayer(doorNum, distanceDiff));

    }

    IEnumerator LoadLevel(string ln)
    {
        fade.SetTrigger("StartAni");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(ln, LoadSceneMode.Single);
        destroyUnwantedOrbClones();
    }

    IEnumerator TransportPlayer(int dn, float dd)
    {

        yield return new WaitForSeconds(1.1f);

       

        //Find Level Door of Door Num, delay + Ani, teleport to location
        GameObject[] doors = GameObject.FindGameObjectsWithTag("LevelDoor");

        foreach (GameObject door in doors)
        {
            if (door.GetComponent<LevelDoor>().getNumber() == dn)
            {
                destination = door.transform;
            }
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
       
        player.transform.position = new Vector2(destination.position.x + dd, destination.position.y);
        fade.SetTrigger("EndAni");
    }

    private void destroyUnwantedOrbClones()
    {
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Orb");
        foreach (GameObject orb in clones)
        {
            if (orb.GetComponent<BasicOrb>().aloneAndLost() == true)
            {
                Destroy(orb);
            }
        }
    }
}
