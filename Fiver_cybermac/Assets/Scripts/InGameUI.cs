using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    [SerializeField] Image HealthAmount;
    [SerializeField] TMP_Text Score;
    [SerializeField] GameObject GameOver;
   
    private void Start()
    {
        SetPlayer.HitShip.AddListener(delegate { UpdateHealth(); });
        SetPlayer.GameOverEvent.AddListener(delegate { GameOverScreen(); });
    }
    private void Update()
    {
        Score.text = GameManager.Instance.GetScore.ToString();
    }

    void UpdateHealth()
    {
        HealthAmount.fillAmount = (float)FindObjectOfType<SetPlayer>().GetHealth / (float)GameManager.Instance.GetPlayerShip.ShipHealth;
    }
    void GameOverScreen()
    {
        PlayerPrefs.SetInt("HighScore", GameManager.Instance.GetScore);
        PlayerPrefs.Save();
        GameOver.SetActive(true);
    }

    public void Continue()
    {
        GameManager.Instance.PlayGame(GameManager.Instance.GetPlayerShip);
    }
    public void Menu()
    {
        GameManager.Instance.EndGame();
    }
}
