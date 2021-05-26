using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData
{
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


    public GameData(int CoinN, int TokenN,
                    float MusicM, float MajorSFXM, float MinorSFXM, float VoiceM, bool spUnlocked, bool sspUnlocked, bool opUnlocked, bool bkUnlocked, bool rkUnlocked,
                    bool pkUnlocked, bool ykUnlocked, bool gkUnlocked, bool okUnlocked, bool bbUnlocked, bool fUnlocked, bool dfUnlocked, bool ulUnlocked, bool slUnlocked,
                    bool shUnlocked, bool lhUnlocked, bool wUnlocked, bool waUnlocked, bool rakUnlocked,
                    int statueT, int plantT, int bombT, int flowerT, int bigCT, int pylonStickerT, int boltStickerT,
                    int levelN, int doorN, int pBombT)
    {
        Coins = CoinN;
        Tokens = TokenN;

        MusicMulti = MusicM;
        MajorSFXMulti = MajorSFXM;
        MinorSFXMulti = MinorSFXM;
        VoiceMulti = VoiceM;

        stonePlatformerUnlocked = spUnlocked;
        sandStonePlatformerUnlocked = sspUnlocked;
        obsidianPlatformerUnlocked = opUnlocked;
        blueKeyUnlocked = bkUnlocked;
        redKeyUnlocked = rkUnlocked;
        purpleKeyUnlocked = pkUnlocked;
        yellowKeyUnlocked = ykUnlocked;
        greenKeyUnlocked = gkUnlocked;
        orangeKeyUnlocked = okUnlocked;
        basicBlasterUnlocked = bbUnlocked;
        fuserUnlocked = fUnlocked;
        defuserUnlocked = dfUnlocked;
        upwardLauncherUnlocked = ulUnlocked;
        sidewaysLauncherUnlocked = slUnlocked;
        smallHollowUnlocked = shUnlocked;
        largeHollowUnlocked = lhUnlocked;
        wholeUnlocked = wUnlocked;
        wasteUnlocked = waUnlocked;
        rainbowKeyUnlocked = rakUnlocked;

        statueTier = statueT;
        plantTier = plantT;
        bombTier = bombT;
        flowerTier = flowerT;
        bigCTier = bigCT;
        pylonStickerTier = pylonStickerT;
        boltStickerTier = boltStickerT;

        levelNumber = levelN;
        doorNumber = doorN;

        platformBombTier = pBombT;
    }

}
