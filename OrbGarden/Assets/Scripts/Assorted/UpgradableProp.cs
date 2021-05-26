using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableProp : BaseOrbGameScript
{
    [SerializeField]
    private Sprite[] sprites;

    private Sprite newSprite;






    public void ChangeSprite(int spriteNum)
    {
        Sprite newSprite = sprites[spriteNum];
        gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
    }

}
