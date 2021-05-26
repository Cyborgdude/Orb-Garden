using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Game : MonoBehaviour
{
    public static Game Current;

    [SerializeField]
    public int Coins;
    [SerializeField]
    public int Tokens;
    [SerializeField]
    public float MusicMulti;
    [SerializeField]
    public float MajorSFXMulti;
    [SerializeField]
    public float MinorSFXMulti;
    [SerializeField]
    public float VoiceMulti;
    [SerializeField]
    public bool stonePlatformerUnlocked;
    [SerializeField]
    public bool sandStonePlatformerUnlocked;
    [SerializeField]
    public bool obsidianPlatformerUnlocked;
    [SerializeField]
    public bool blueKeyUnlocked;
    [SerializeField]
    public bool redKeyUnlocked;
    [SerializeField]
    public bool purpleKeyUnlocked;
    [SerializeField]
    public bool yellowKeyUnlocked;
    [SerializeField]
    public bool greenKeyUnlocked;
    [SerializeField]
    public bool orangeKeyUnlocked;
    [SerializeField]
    public bool basicBlasterUnlocked;
    [SerializeField]
    public bool fuserUnlocked;
    [SerializeField]
    public bool defuserUnlocked;
    [SerializeField]
    public bool upwardLauncherUnlocked;
    [SerializeField]
    public bool sidewaysLauncherUnlocked;
    [SerializeField]
    public bool smallHollowUnlocked;
    [SerializeField]
    public bool largeHollowUnlocked;
    [SerializeField]
    public bool wholeUnlocked;
    [SerializeField]
    public bool wasteUnlocked;
    [SerializeField]
    public bool rainbowKeyUnlocked;
    [SerializeField]
    public int statueTier;
    [SerializeField]
    public int plantTier;
    [SerializeField]
    public int bombTier;
    [SerializeField]
    public int flowerTier;
    [SerializeField]
    public int bigCTier;
    [SerializeField]
    public int pylonStickerTier;
    [SerializeField]
    public int boltStickerTier;


    [SerializeField]
    public int levelNumber;

    [SerializeField]
    public int doorNumber;

    [SerializeField]
    public int platformBombTier;



    public GameData GData;

    private void Awake()
    {
        if (Current == null)
        {
            DontDestroyOnLoad(gameObject);
            Current = this;
            GData = new GameData(0, 0, 0, 0, 0, 0, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
        }
        else if (Current != this)
        {
            Destroy(gameObject);
        }
    }



}
