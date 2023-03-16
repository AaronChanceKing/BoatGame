using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneManager : MonoBehaviour
{
    [SerializeField] Dictionary<Vector2, GameObject> ZonesActive = new Dictionary<Vector2, GameObject>();
    [SerializeField] GameObject StartingZone;
    [SerializeField] List<GameObject> ZonePreFabs;
    GameObject player;
    GameObject inZone = null;
    GameObject prevZone;
    Vector2[] SpawnPoints = new Vector2[]
    {
        new Vector2(50, 0),
        new Vector2(50, 50),
        new Vector2(50, -50),

        new Vector2(-50, 0),
        new Vector2(-50, -50),
        new Vector2(-50, 50),

        new Vector2(0, 50),
        new Vector2(0, -50),
    };

    private void Start()
    {
        ZonesActive.Add(Vector2.zero, StartingZone);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        GetCurrentZone();
        RemoveOldZones();
        if(inZone != prevZone)
            AddNewZones();
    }

    void GetCurrentZone()
    {
        prevZone = inZone;

        foreach (var zone in ZonesActive.Values)
        {
            if (zone.GetComponent<BoxCollider>().bounds.Contains(player.transform.position))
            {
                inZone = zone;
            }
        }
    }

    void RemoveOldZones()
    {
        if (ZonesActive.Count < 3) return;

        List<Vector2> toRemove = new List<Vector2>();

        foreach (var zone in ZonesActive.Keys)
        {
            if (Vector3.Distance(zone, inZone.transform.position) > 200)
            {
                toRemove.Add(zone);
            }
        }

        foreach(var remove in toRemove)
        {
            Destroy(ZonesActive[remove].gameObject);
            ZonesActive.Remove(remove);
        }
    }

    void AddNewZones()
    {
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            if (ZonesActive.ContainsKey(new Vector2(SpawnPoints[i].x + inZone.transform.position.x, SpawnPoints[i].y + inZone.transform.position.y)))
                continue;

            GameObject newZone = Instantiate(ZonePreFabs[Random.Range(0, ZonePreFabs.Count)]);
            newZone.transform.position = new Vector3(inZone.transform.position.x + SpawnPoints[i].x, inZone.transform.position.y + SpawnPoints[i].y, inZone.transform.position.z);

            ZonesActive.Add(newZone.transform.position, newZone);
        }
    }
}
