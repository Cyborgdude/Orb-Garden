using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoad
{
    public static void Test()
    {
        
    }


    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();

        FileStream file = File.Create(Application.persistentDataPath + "/GameSaves.sav");

        GameData GData = new GameData
            (
            Game.Current.GData.Coins,
            Game.Current.GData.Tokens,

            Game.Current.GData.MusicMulti,
            Game.Current.GData.MajorSFXMulti,
            Game.Current.GData.MinorSFXMulti,
            Game.Current.GData.VoiceMulti,
            Game.Current.GData.stonePlatformerUnlocked,
            Game.Current.GData.sandStonePlatformerUnlocked,
            Game.Current.GData.obsidianPlatformerUnlocked,
            Game.Current.GData.blueKeyUnlocked,
            Game.Current.GData.redKeyUnlocked,
            Game.Current.GData.purpleKeyUnlocked,
            Game.Current.GData.yellowKeyUnlocked,
            Game.Current.GData.greenKeyUnlocked,
            Game.Current.GData.orangeKeyUnlocked,
            Game.Current.GData.basicBlasterUnlocked,
            Game.Current.GData.fuserUnlocked,
            Game.Current.GData.defuserUnlocked,
            Game.Current.GData.upwardLauncherUnlocked,
            Game.Current.GData.sidewaysLauncherUnlocked,
            Game.Current.GData.smallHollowUnlocked,
            Game.Current.GData.largeHollowUnlocked,
            Game.Current.GData.wholeUnlocked,
            Game.Current.GData.wasteUnlocked,
            Game.Current.GData.rainbowKeyUnlocked,

            Game.Current.GData.statueTier,
            Game.Current.GData.plantTier,
            Game.Current.GData.bombTier,
            Game.Current.GData.flowerTier,
            Game.Current.GData.bigCTier,
            Game.Current.GData.pylonStickerTier,
            Game.Current.GData.boltStickerTier,

            Game.Current.GData.levelNumber,
            Game.Current.GData.doorNumber,

            Game.Current.GData.platformBombTier
            );

        bf.Serialize(file, GData);
        file.Close();
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/GameSaves.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();

            FileStream file = File.Open(Application.persistentDataPath + "/GameSaves.sav", FileMode.Open);

            GameData GData = (GameData)bf.Deserialize(file);

            Game.Current.GData = GData;
            file.Close();
        }
    }

}
