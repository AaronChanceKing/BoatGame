using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Starting Cam")]
    [SerializeField] float lerpSpeed;
    [SerializeField] float Z = -10;
    [SerializeField] Transform target;
    Vector3 targetPos;
    bool Rotate;
    float TurnClamp;

    private void Awake()
    {
        Rotate = FindObjectOfType<PlayerMovment>().GetCameraRotation;
        TurnClamp = FindObjectOfType<PlayerMovment>().GetClamp;
    }

    private void Update()
    {
        if (target == null) return;
        targetPos = target.position;

        transform.position = new Vector3(
            Mathf.Lerp(transform.position.x, targetPos.x, lerpSpeed * Time.deltaTime),
            Mathf.Lerp(transform.position.y, targetPos.y, lerpSpeed * Time.deltaTime),
            Z
            );

        if(Rotate)
        {
            float targetRot = target.rotation.eulerAngles.z;
            Vector3 value = Vector3.zero;

            if ((targetRot >= TurnClamp || targetRot <= 360 - TurnClamp) && targetRot - transform.rotation.eulerAngles.z != 0)
            {
                value.z = targetRot;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(value), Time.deltaTime);
            }
        }
    }

}
