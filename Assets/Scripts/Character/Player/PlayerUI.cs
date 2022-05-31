using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerUI : MonoBehaviour
{
   
    static public PlayerUI Instance;
    [SerializeField]Image healtBar;
    [SerializeField] TextMeshProUGUI scoretext;
    [SerializeField] TextMeshProUGUI highScoreText;
    public int MaxScore;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        MaxScore = SaveSystem.LoadBestScore();
        highScoreText.text = MaxScore.ToString();
    }
    public void SetSizeHealthBar(float value)
    {
        if (Player.Instance.isDie) return;
        healtBar.rectTransform.DOScaleX(value, 0.2f);
    }
    public void SetScore()
    {
        if (Player.Instance.isDie) return;
        if (Player.Instance.Scoreget > MaxScore)
        {
            MaxScore = Player.Instance.Scoreget;
            highScoreText.text = MaxScore.ToString();
        }
        scoretext.text = Player.Instance.Scoreget.ToString();
    }
}
