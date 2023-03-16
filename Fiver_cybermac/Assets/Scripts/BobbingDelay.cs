using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbingDelay : MonoBehaviour
{
    float DelayTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        DelayTime = Random.Range(0f, 5f);
        GetComponent<Animator>().speed = 0;
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(DelayTime);


        GetComponent<Animator>().speed = 1;
    }
}
