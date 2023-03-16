using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetHighScore : MonoBehaviour
{
    [SerializeField] TMP_Text Score;

    void Start()
    {
        Score.text = $"High Score: {PlayerPrefs.GetInt("HighScore", 0).ToString()}";
    }
}
