using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SetPlayer : MonoBehaviour
{
    [SerializeField] SpriteRenderer PlayerSprite;
    int Health;
    int CurrentHealth;
    public int GetHealth { get => CurrentHealth; }

    public static UnityEvent HitShip = new UnityEvent();
    public static UnityEvent HitCoin = new UnityEvent();
    public static UnityEvent GameOverEvent = new UnityEvent();

    private void Awake()
    {
        PlayerShips ship = GameManager.Instance.GetPlayerShip;

        PlayerSprite.sprite = ship.InGameSprite;
        GetComponent<PlayerMovment>().SetPlayerMovment(ship.ShipSpeed, ship.ShipHandling);
        Health = ship.ShipHealth;
        CurrentHealth = Health;
        GetComponent<CapsuleCollider2D>().size = (PlayerSprite.sprite.rect.size / 110);
        GetComponent<BoxCollider2D>().size = (PlayerSprite.sprite.rect.size / 110);

        GameManager.Instance.UpdateState(GameManager.GameState.StartGame);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            CurrentHealth--;
            collision.gameObject.GetComponent<Enemy>().HitPlayer();
            HitShip?.Invoke();

            if (CurrentHealth <= 0)
                GameOver();
        }
        else if (collision.tag == "Coin")
        {
            HitCoin?.Invoke();
        }
    }

    void GameOver()
    {
        GameManager.Instance.UpdateState(GameManager.GameState.EndGame);
        GameOverEvent?.Invoke();
    }

    private void OnDisable()
    {
        HitShip.RemoveAllListeners();
        HitCoin.RemoveAllListeners();
        GameOverEvent.RemoveAllListeners();
    }
}
