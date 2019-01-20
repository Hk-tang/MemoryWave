using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{

    public Text scoreText;
    public Text maxComboText;
    public Text accuracyText;
    public Text numPerfectText;
    public Text numGreatText;
    public Text numGoodText;
    public Text numBadText;
    public Text numMissText;
    public Button backButton;
    public Button retryButton;

    private int endScore = 0;
    private int endMaxCombo = 0;
    private int endAccuracy = 0;
    private int endNumPerfect = 0;
    private int endNumGreat = 0;
    private int endNumGood = 0;
    private int endNumBad = 0;
    private int endNumMiss = 0;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(GoBack);
        endScore = 420;
        scoreText.text = "FINAL SCORE: " + endScore.ToString();
        maxComboText.text = "MAX COMBO: " + endMaxCombo.ToString() + "x";
        accuracyText.text = "ACCURACY: " + endAccuracy.ToString() + "%";
        numPerfectText.text = "PERFECTS: " + endNumPerfect.ToString();
        numGreatText.text = "GREATS: " + endNumGreat.ToString();
        numGoodText.text = "GOODS: " + endNumGood.ToString();
        numBadText.text = "BADS: " + endNumBad.ToString();
        numMissText.text = "MISSES: " + endNumMiss.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Update is called once per frame
    void GoBack()
    {
        numMissText.text = "BACK FUNCTIONALITY IS WIP";
    }
}
