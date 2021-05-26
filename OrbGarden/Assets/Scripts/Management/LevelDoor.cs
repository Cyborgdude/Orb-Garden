using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDoor : BaseOrbGameScript
{
    [SerializeField]
    int levelNum;
    [SerializeField]
    int doorNum;
    [SerializeField]
    int destinationNum;
    [SerializeField]
    float spawnDiff;


    public int getNumber()
    {
        return doorNum;
    }
    private void open()
    {
        

        Game.Current.GData.levelNumber = levelNum;
        Game.Current.GData.doorNumber = destinationNum;

        SaveLoad.Save();


        GameObject SCM = GameObject.FindGameObjectWithTag("SceneChangeManager");
        SCM.GetComponent<SceneChangeManager>().changeLevel(levelNum, destinationNum, spawnDiff);


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<Player>().getEnterEnabled() == true)
            {
                collision.gameObject.GetComponent<Player>().setEnterEnabled(false);
                open();

            }
        }
    }
}
