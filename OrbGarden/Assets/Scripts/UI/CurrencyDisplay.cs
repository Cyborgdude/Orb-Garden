using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrencyDisplay : BaseOrbGameScript
{
    private Text ScoreText;
    [SerializeField]
    private bool isToken;

    // Start is called before the first frame update
    void Start()
    {
        ScoreText = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update()
    {
        if(isToken == true)
        {
            ScoreText.text = Game.Current.GData.Tokens.ToString();
        }
        else
        {
            ScoreText.text = Game.Current.GData.Coins.ToString();
        }

    }
}
