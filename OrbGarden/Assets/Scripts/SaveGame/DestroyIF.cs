using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyIF : BaseOrbGameScript
{
    [SerializeField]
    private int conditionNum;
    // 1 = Bomb Destroyed


    // Start is called before the first frame update
    void Start()
    {
        SaveLoad.Load();
        switch(conditionNum)
        {
            // 1 = Bomb Destroyed
            case 1:
                if(Game.Current.GData.bombTier >= 2)
                {
                    Destroy(gameObject);
                }
                break;
            case 2:
                if(Game.Current.GData.basicBlasterUnlocked == true)
                {
                    Destroy(gameObject);
                }
                break;
            case 3:
                if(Game.Current.GData.blueKeyUnlocked == true)
                {
                    Destroy(gameObject);
                }
                break;
            case 4:
                if (Game.Current.GData.purpleKeyUnlocked == true)
                {
                    Destroy(gameObject);
                }
                break;
            case 5:
                if (Game.Current.GData.platformBombTier >= 2)
                {
                    Destroy(gameObject);
                }
                break;
            case 6:
                if (Game.Current.GData.defuserUnlocked == true || Game.Current.GData.blueKeyUnlocked == true)
                {
                    Destroy(gameObject);
                }
                break;
            case 7:
                if (Game.Current.GData.stonePlatformerUnlocked == true &&
                    Game.Current.GData.sandStonePlatformerUnlocked == true &&
                    Game.Current.GData.obsidianPlatformerUnlocked == true &&
                    Game.Current.GData.blueKeyUnlocked == true &&
                    Game.Current.GData.redKeyUnlocked == true &&
                    Game.Current.GData.purpleKeyUnlocked == true &&
                    Game.Current.GData.yellowKeyUnlocked == true &&
                    Game.Current.GData.greenKeyUnlocked == true &&
                    Game.Current.GData.orangeKeyUnlocked == true &&
                    Game.Current.GData.basicBlasterUnlocked == true &&
                    Game.Current.GData.fuserUnlocked== true &&
                    Game.Current.GData.defuserUnlocked== true &&
                    Game.Current.GData.upwardLauncherUnlocked == true &&
                    Game.Current.GData.sidewaysLauncherUnlocked == true &&
                    Game.Current.GData.smallHollowUnlocked == true &&
                    Game.Current.GData.largeHollowUnlocked == true &&
                    Game.Current.GData.wholeUnlocked == true &&
                    Game.Current.GData.wasteUnlocked == true &&
                    Game.Current.GData.rainbowKeyUnlocked == true 
                    )
                {
                   
                    Destroy(gameObject);
                }
                break;
            default:

                break;
        }

    }


}
