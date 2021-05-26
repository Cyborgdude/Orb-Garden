using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nest : BaseOrbGameScript
{
    [SerializeField]
    private bool gardenNest;
    [SerializeField]
    private GameObject orbToSpawn;
    [SerializeField]
    private OrbType orbType;
    [SerializeField]
    private int spawnNumber = 1;

    [SerializeField]
    private float spawnDelay = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        Invoke("checkOwnVarAndAct", spawnDelay);
    }
    private void Update()
    {

    }

    private void spawnOrbsAtNest()
    {
        for(int i = 0; i < spawnNumber; i++)
        {
            Instantiate(orbToSpawn, new Vector2(transform.position.x, transform.position.y), transform.rotation);
        }
    }

    private bool checkIfUnlocked()
    {
        switch (orbType)
        {
            case OrbType.stonePlatformer:
                return Game.Current.GData.stonePlatformerUnlocked;
            case OrbType.sandstonePlatformer:
                return Game.Current.GData.sandStonePlatformerUnlocked;
            case OrbType.obsidianPlatformer:
                return Game.Current.GData.obsidianPlatformerUnlocked;
            case OrbType.blueKey:
                return Game.Current.GData.blueKeyUnlocked;
            case OrbType.redKey:
                return Game.Current.GData.redKeyUnlocked;
            case OrbType.purpleKey:
                return Game.Current.GData.purpleKeyUnlocked;
            case OrbType.yellowKey:
                return Game.Current.GData.yellowKeyUnlocked;
            case OrbType.greenKey:
                return Game.Current.GData.greenKeyUnlocked;
            case OrbType.orangeKey:
                return Game.Current.GData.orangeKeyUnlocked;
            case OrbType.basicBlaster:
                return Game.Current.GData.basicBlasterUnlocked;
            case OrbType.fuser:
                return Game.Current.GData.fuserUnlocked;
            case OrbType.defuser:
                return Game.Current.GData.defuserUnlocked;
            case OrbType.upwardLauncher:
                return Game.Current.GData.upwardLauncherUnlocked;
            case OrbType.sidewaysLauncher:
                return Game.Current.GData.sidewaysLauncherUnlocked;
            case OrbType.smallHollow:
                return Game.Current.GData.smallHollowUnlocked;
            case OrbType.largeHollow:
                return Game.Current.GData.largeHollowUnlocked;
            case OrbType.whole:
                return Game.Current.GData.wholeUnlocked;
            case OrbType.waste:
                return Game.Current.GData.wasteUnlocked;
            case OrbType.rainbowKey:
                return Game.Current.GData.rainbowKeyUnlocked;

            default:
                return false;
                
        }
    }

    private void checkOwnVarAndAct()
    {
        if (gardenNest == false || checkIfUnlocked() == true)
        {
            spawnOrbsAtNest();
        }
    }


}
