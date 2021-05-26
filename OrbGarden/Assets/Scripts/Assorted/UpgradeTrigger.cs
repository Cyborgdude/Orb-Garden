using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTrigger : BaseOrbGameScript, IPurchase
{

    //Mechanics
    [SerializeField]
    private GameObject prop;
    [SerializeField]
    GardenProp propType;
    [SerializeField]
    private int tokenCost;
    [SerializeField]
    private int coinCost;
    private bool purchaseEnabled = true;

    //UI
    [SerializeField]
    private GameObject UICanvas;
    [SerializeField]
    private GameObject tokenText;
    [SerializeField]
    private GameObject coinText;

    //Flair
    [SerializeField]
    private GameObject failBuyPS;
    [SerializeField]
    private AudioClip failBuySFX;
    [SerializeField]
    private GameObject upgradePS;
    [SerializeField]
    private AudioClip upgradeSFX;

    //Managers
    private GameObject speaker;

    private Collider2D[] nearbyObjects;



    // Start is called before the first frame update
    void Start()
    {
        
        UpdateProp();
        speaker = GameObject.FindGameObjectWithTag("Speaker");

    }

    void Update()
    {

    }

    private void UpdateProp()
    {
        //Find Tier of Prop
        int currentTier = 0;

        switch (propType)
        {
            case GardenProp.launcherStatue:
                currentTier = Game.Current.GData.statueTier;
                break;
            case GardenProp.plant:
                currentTier = Game.Current.GData.plantTier;
                break;
            case GardenProp.bomb:
                currentTier = Game.Current.GData.bombTier;
                break;
            case GardenProp.flower:
                currentTier = Game.Current.GData.flowerTier;
                break;
            case GardenProp.bigC:
                currentTier = Game.Current.GData.bigCTier;
                break;
            case GardenProp.pylonSticker:
                currentTier = Game.Current.GData.pylonStickerTier;
                break;
            case GardenProp.boltSticker:
                currentTier = Game.Current.GData.boltStickerTier;
                break;
            case GardenProp.platformBomb:
                currentTier = Game.Current.GData.platformBombTier;
                break;
        }

        //Update self info and Prop Info
        switch (currentTier)
        {
            case 0:
                break;
            case 1:
                tokenCost = tokenCost;
                coinCost = coinCost * 3;
                break;
            case 2:
                purchaseEnabled = false;
                UICanvas.SetActive(false);
                break;
        }

        prop.GetComponent<UpgradableProp>().ChangeSprite(currentTier);
        Text coinTextTComp = coinText.GetComponent<Text>();
        coinTextTComp.text = coinCost.ToString();
        Text tokenTextTComp = tokenText.GetComponent<Text>();
        tokenTextTComp.text = tokenCost.ToString();

    }

    public void IPurchase()
    {
        if(purchaseEnabled == true)
        {

            //Check Player has enough currency
            if (Game.Current.GData.Coins >= coinCost && Game.Current.GData.Tokens >= tokenCost)
            {

                switch (propType)
                {
                    //Switch based on prop type save
                    case GardenProp.launcherStatue:
                        Game.Current.GData.statueTier = Game.Current.GData.statueTier + 1;
                        break;
                    case GardenProp.plant:
                        Game.Current.GData.plantTier = Game.Current.GData.plantTier + 1;
                        break;

                    case GardenProp.bomb:
                        Game.Current.GData.bombTier = Game.Current.GData.bombTier + 2;
                        //Cause Explosion
                        nearbyObjects = Physics2D.OverlapCircleAll(prop.transform.position, 6);
                        foreach (Collider2D nearbyObject in nearbyObjects)
                        {
                            var blastable = nearbyObject.GetComponent<IBlast>();
                            if (blastable != null)
                            {
                                blastable.IBlast(AcCol.None);
                            }
                        }
                        break;

                    case GardenProp.flower:
                        Game.Current.GData.flowerTier = Game.Current.GData.flowerTier + 1;
                        break;
                    case GardenProp.bigC:
                        Game.Current.GData.bigCTier = Game.Current.GData.bigCTier + 1;
                        break;
                    case GardenProp.pylonSticker:
                        Game.Current.GData.pylonStickerTier = Game.Current.GData.pylonStickerTier + 2;
                        break;
                    case GardenProp.boltSticker:
                        Game.Current.GData.boltStickerTier = Game.Current.GData.boltStickerTier + 2;
                        break;
                    case GardenProp.platformBomb:
                        Game.Current.GData.platformBombTier = Game.Current.GData.platformBombTier + 2;
                        //Cause Explosion
                        nearbyObjects = Physics2D.OverlapCircleAll(prop.transform.position, 6);
                        foreach (Collider2D nearbyObject in nearbyObjects)
                        {
                            var blastable = nearbyObject.GetComponent<IBlast>();
                            if (blastable != null)
                            {
                                blastable.IBlast(AcCol.None);
                            }
                        }
                        break;
                }

                //Finish Purchase
                SpawnThenDestroyParticle(upgradePS, prop.transform);
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(upgradeSFX, SoundType.majorSFX, 1);
                Game.Current.GData.Coins = Game.Current.GData.Coins - coinCost;
                Game.Current.GData.Tokens = Game.Current.GData.Tokens - tokenCost;
                SaveLoad.Save();
                UpdateProp();
            }
            else
            {
                //Not enough money
                SpawnThenDestroyParticle(failBuyPS, prop.transform);
                speaker.GetComponent<Speaker>().PlaySoundFromSpeaker(failBuySFX, SoundType.majorSFX, 1);
            }
        }
        else
        {
            //Failsafe Update
            UpdateProp();
        }
    }




}
