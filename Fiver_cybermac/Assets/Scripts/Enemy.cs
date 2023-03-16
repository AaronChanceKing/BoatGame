using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] SpriteRenderer EnemySprite;
    float Speed = 0;
    float Handling = 0;
    float FollowTime = 0;
    Transform target;

    public void SetEnemy(PlayerShips ship)
    {
        Speed = ship.ShipSpeed;
        Handling = ship.ShipHandling;
        EnemySprite.sprite = ship.InGameSprite;
        Invoke("RemoveShip", 15f);
    }

    private void Start()
    {
        GetComponent<CapsuleCollider2D>().size = (EnemySprite.sprite.rect.size / EnemySprite.sprite.pixelsPerUnit);

        target = GameObject.FindGameObjectWithTag("Player").transform;

        transform.rotation = GetRotation();
    }

    private void Update()
    {
        FollowTime += Time.deltaTime;
        transform.Translate(Vector3.up * Speed * Time.deltaTime);

        if(FollowTime < 1.0f)
            transform.rotation = Quaternion.Slerp(transform.rotation, GetRotation(), Handling * .5f * Time.deltaTime);
    }

    Quaternion GetRotation()
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        angle -= 90f;

        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void HitPlayer()
    {
        //Other Stuff
        Destroy(this.gameObject);
    }

    void RemoveShip()
    {
        if (!GetComponentInChildren<SpriteRenderer>().isVisible)
            Destroy(this.gameObject);
        else
            Invoke("RemoveShip", 5f);
    }
}
