using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberOrbsRemaining : MonoBehaviour
{
    [SerializeField]
    private int maxOrbNumber = 19;
    void Start()
    {
        Text nORemainingText = GetComponent<Text>();

        if (Game.Current.GData.stonePlatformerUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.sandStonePlatformerUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.obsidianPlatformerUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.blueKeyUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }


        if (Game.Current.GData.redKeyUnlocked== true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }
        
        if (Game.Current.GData.purpleKeyUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.yellowKeyUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.greenKeyUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.orangeKeyUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.basicBlasterUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.fuserUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.defuserUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.upwardLauncherUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.sidewaysLauncherUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.smallHollowUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.largeHollowUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.wholeUnlocked== true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.wasteUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        if (Game.Current.GData.rainbowKeyUnlocked == true)
        {
            maxOrbNumber = maxOrbNumber - 1;
        }

        nORemainingText.text = maxOrbNumber.ToString();
    }



}
