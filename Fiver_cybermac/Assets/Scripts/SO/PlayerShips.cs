using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerShip", menuName = "ScriptableObjects/PlayerShip")]
public class PlayerShips : ScriptableObject
{
    public Sprite ShipImage;
    public Sprite InGameSprite;
    public float ShipSpeed;
    public float ShipHandling;
    public int ShipHealth;
}
