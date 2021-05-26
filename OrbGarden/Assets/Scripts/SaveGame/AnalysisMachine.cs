using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalysisMachine : BaseOrbGameScript
{
    //Flair
    [SerializeField]
    private AudioClip failSFX;
    [SerializeField]
    private AudioClip successSFX;
    [SerializeField]
    private GameObject failPS;
    [SerializeField]
    private GameObject succeedPS;
    [SerializeField]
    private GameObject operatePS;

    //Managers
    private GameObject speaker;

    void Start()
    {
        speaker = GameObject.FindGameObjectWithTag("Speaker");
    }

    public void processOrb(OrbType orbType, Transform itemTransform)
    {
        //Spawn Analysis Particle
        SpawnThenDestroyParticle(operatePS, transform);

        //Switch on orb type passed in by player
        switch (orbType)
        {
            case OrbType.stonePlatformer:
                utilizeOrb(ref Game.Current.GData.stonePlatformerUnlocked, itemTransform);
                break;
            case OrbType.sandstonePlatformer:
                utilizeOrb(ref Game.Current.GData.sandStonePlatformerUnlocked, itemTransform);
                break;
            case OrbType.obsidianPlatformer:
                utilizeOrb(ref Game.Current.GData.obsidianPlatformerUnlocked, itemTransform);
                break;
            case OrbType.blueKey:
                utilizeOrb(ref Game.Current.GData.blueKeyUnlocked, itemTransform);
                break;
            case OrbType.redKey:
                utilizeOrb(ref Game.Current.GData.redKeyUnlocked, itemTransform);
                break;
            case OrbType.purpleKey:
                utilizeOrb(ref Game.Current.GData.purpleKeyUnlocked, itemTransform);
                break;
            case OrbType.yellowKey:
                utilizeOrb(ref Game.Current.GData.yellowKeyUnlocked, itemTransform);
                break;
            case OrbType.greenKey:
                utilizeOrb(ref Game.Current.GData.greenKeyUnlocked, itemTransform);
                break;
            case OrbType.orangeKey:
                utilizeOrb(ref Game.Current.GData.orangeKeyUnlocked, itemTransform);
                break;
            case OrbType.basicBlaster:
                utilizeOrb(ref Game.Current.GData.basicBlasterUnlocked, itemTransform);
                break;
            case OrbType.fuser:
                utilizeOrb(ref Game.Current.GData.fuserUnlocked, itemTransform);
                break;
            case OrbType.defuser:
                utilizeOrb(ref Game.Current.GData.defuserUnlocked, itemTransform);
                break;
            case OrbType.upwardLauncher:
                utilizeOrb(ref Game.Current.GData.upwardLauncherUnlocked, itemTransform);
                break;
            case OrbType.sidewaysLauncher:
                utilizeOrb(ref Game.Current.GData.sidewaysLauncherUnlocked, itemTransform);
                break;
            case OrbType.smallHollow:
                utilizeOrb(ref Game.Current.GData.smallHollowUnlocked, itemTransform);
                break;
            case OrbType.largeHollow:
                utilizeOrb(ref Game.Current.GData.largeHollowUnlocked, itemTransform);
                break;
            case OrbType.whole:
                utilizeOrb(ref Game.Current.GData.wholeUnlocked, itemTransform);
                break;
            case OrbType.waste:
                utilizeOrb(ref Game.Current.GData.wasteUnlocked, itemTransform);
                break;
            case OrbType.rainbowKey:
                utilizeOrb(ref Game.Current.GData.rainbowKeyUnlocked, itemTransform);
                break;
        }
    }

    private void utilizeOrb(ref bool saveUnlockState, Transform itemTransform)
    {
        //Use ref to save file to alter said save file
        if (saveUnlockState != true)
        {
            saveUnlockState = true;
            //Spawn Success Particle
            SpawnThenDestroyParticle(succeedPS, itemTransform);
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(successSFX, SoundType.majorSFX, 1);
            Game.Current.GData.Tokens = Game.Current.GData.Tokens + 1;
            spawnCoins();

            SaveLoad.Save();

        }
        else
        {
            //Spawn Fail Particle
            SpawnThenDestroyParticle(failPS, itemTransform);
            speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(failSFX, SoundType.majorSFX, 1);
        }
    }

    private void spawnCoins()
    {

    }
}
