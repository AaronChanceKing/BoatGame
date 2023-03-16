using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] PlayerShips Player;

    [Header("States")]
    [SerializeField] GameState CurrentState;
    [SerializeField] Difficulty CurrentDifficulty;
    [SerializeField] float[] ScoreChange = new float[] { 60f, 120f, 180f, 240f};
    [SerializeField] int HitReduction = 5;
    [SerializeField] int CoinCost = 5;


    public PlayerShips GetPlayerShip { get => Player; }
    public Difficulty GetDifficulty { get => CurrentDifficulty; }
    public int GetScore { get => Mathf.RoundToInt(GameScore); }
    float GameScore = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        if (CurrentState == GameState.StartUp)
            UpdateState(GameState.Menu);
        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateState(GameState state)
    {
        if (state == CurrentState) return;

        CurrentState = state;
        switch(state)
        {
            case GameState.Menu:
                break;
            case GameState.StartGame:
                GameScore = 0;
                CurrentDifficulty = Difficulty.Easy;
                SetPlayer.HitCoin.AddListener(delegate { PickUpCoin(); });
                SetPlayer.HitShip.AddListener(delegate { HitShip(); });
                UpdateState(GameState.Playing);
                break;
            case GameState.Playing:
                StartCoroutine(ScoreUpdate());
                break;
            case GameState.EndGame:
                break;
            default:
                Debug.LogError($"{state} not set up.");
                break;
        }
    }

    public void PlayGame(PlayerShips ship)
    {
        Player = ship;

        StartCoroutine(LoadLevel(1));
    }

    public void EndGame()
    {
        UpdateState(GameState.Menu);
        StartCoroutine(LoadLevel(0));
    }

    public void PickUpCoin() => GameScore += CoinCost;

    public void HitShip()
    {
        GameScore = GameScore - HitReduction < 0 ? 0 : GameScore - HitReduction;
    }

    IEnumerator ScoreUpdate()
    {
        while(CurrentState == GameState.Playing)
        {
            GameScore += Time.deltaTime;

            if (GameScore > ScoreChange[0] && GameScore < ScoreChange[1] && CurrentDifficulty != Difficulty.Medium)
                CurrentDifficulty = Difficulty.Medium;
            else if (GameScore > ScoreChange[1] && GameScore < ScoreChange[2] && CurrentDifficulty != Difficulty.Hard)
                CurrentDifficulty = Difficulty.Hard;
            else if (GameScore > ScoreChange[2] && GameScore < ScoreChange[3] && CurrentDifficulty != Difficulty.VeryHard)
                CurrentDifficulty = Difficulty.VeryHard;
            else if (GameScore > ScoreChange[3] && CurrentDifficulty != Difficulty.Godly)
                CurrentDifficulty = Difficulty.Godly;


            yield return null;
        }

        if (GameScore > ScoreChange[0] && GameScore < ScoreChange[1] && CurrentDifficulty != Difficulty.Medium)
            CurrentDifficulty = Difficulty.Medium;
        else if (GameScore > ScoreChange[1] && GameScore < ScoreChange[2] && CurrentDifficulty != Difficulty.Hard)
            CurrentDifficulty = Difficulty.Hard;
        else if (GameScore > ScoreChange[2] && GameScore < ScoreChange[3] && CurrentDifficulty != Difficulty.VeryHard)
            CurrentDifficulty = Difficulty.VeryHard;
        else if (GameScore > ScoreChange[3] && CurrentDifficulty != Difficulty.Godly)
            CurrentDifficulty = Difficulty.Godly;


        yield return null;
    }

    IEnumerator LoadLevel(int level)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(level);
        loading.allowSceneActivation = false;
        //Loading Screen

        while (loading != null && !loading.isDone)
        {
            if (loading.progress >= .9f)
            {
                loading.allowSceneActivation = true;
            }

            yield return null;
        }

    }

    public enum GameState 
    {
        StartUp,
        Menu,
        Playing,
        StartGame,
        EndGame
    }

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        VeryHard,
        Godly
    }
}
