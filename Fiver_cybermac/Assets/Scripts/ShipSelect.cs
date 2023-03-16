using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipSelect : MonoBehaviour
{
    [SerializeField] List<PlayerShips> ShipOptions;
    PlayerShips selection;
    [SerializeField] int currentShip = 0;

    [Header("Text Objects")]
    [SerializeField] TMP_Text Speed;
    [SerializeField] TMP_Text Handling;
    [SerializeField] TMP_Text Health;

    [Header("Sprite")]
    [SerializeField] SpriteRenderer Ship;


    private void Start()
    {
        ChangeShip();
    }

    public void NextShip()
    {
        if (currentShip + 1 < ShipOptions.Count)
            currentShip++;
        else
            currentShip = 0;

        ChangeShip();
    }
    public void PrevShip()
    {
        if (currentShip - 1 >= 0)
            currentShip--;
        else
            currentShip = ShipOptions.Count - 1;

        ChangeShip();
    }

   public void Play()
    {
        GameManager.Instance.PlayGame(selection);
    }

    void ChangeShip()
    {
        selection = ShipOptions[currentShip];

        Ship.sprite = selection.ShipImage;

        Speed.text = selection.ShipSpeed.ToString();
        Handling.text = selection.ShipHandling.ToString();
        Health.text = selection.ShipHealth.ToString();
    }
}
