using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuVisual : MonoBehaviour
{
    private int conditionNum;
    [SerializeField]
    private GameObject[] particles;
    [SerializeField]
    private Transform[] spawnLocations;

    private void Start()
    {
        SaveLoad.Load();
        switch(Game.Current.GData.levelNumber)
        {
            case 0:
                //Set Level
                //Rain
                Instantiate(particles[0],spawnLocations[0]);
                break;
            case 1:
                //levelName = "GardenHub";
                break;
            case 2:
                //levelName = "Platformer1";
                Instantiate(particles[1], spawnLocations[1]);
                break;
            case 3:
                //levelName = "Platformer2";
                Instantiate(particles[1], spawnLocations[1]);
                break;
            case 4:
                //levelName = "Blaster1";
                Instantiate(particles[2], spawnLocations[2]);
                break;
            case 5:
                //levelName = "Blaster2";
                Instantiate(particles[2], spawnLocations[2]);
                break;
            case 6:
                //levelName = "Blaster3";
                Instantiate(particles[2], spawnLocations[2]);
                break;
            case 7:
                //levelName = "Launcher1";
                break;
            case 8:
                //levelName = "Purple1";
                Instantiate(particles[3], spawnLocations[3]);
                break;
            case 9:
                //levelName = "Blue1";
                break;
            case 10:
                //levelName = "Blue2";
                break;
            case 11:
                //levelName = "Final1";
                Instantiate(particles[0], spawnLocations[0]);
                break;
            case 12:
                //levelName = "Final2";
                break;
            case 13:
                //levelName = "Purple2";
                Instantiate(particles[4], spawnLocations[4]);
                break;
            default:
                break;
        }
    }

}
