using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float MaxSpeed;
    [SerializeField] float TurnClamp = 45;
    [SerializeField] bool CameraRotation = true;
    public bool GetCameraRotation { get => CameraRotation; }
    public float GetClamp { get => TurnClamp; }
    float CurrentSpeed = 0;
    [SerializeField] float Handling;
    bool Ready = false;

    [SerializeField] ParticleSystem trail;

    public void SetPlayerMovment(float speed, float handling)
    {
        SetPlayer.GameOverEvent.AddListener(delegate { SetPlayerMovment(0, 0); GetComponent<BoxCollider2D>().isTrigger = false; });
        MaxSpeed = speed;
        Handling = handling;
        Ready = true;
    }

    private void Update()
    {
        trail.startSpeed = Mathf.Clamp(CurrentSpeed, 0, 5);
        if (!Ready) return;

        SetSpeed();
        SetRotation();

        transform.Translate(Vector3.up * CurrentSpeed * Time.deltaTime);
    }

    void SetSpeed()
    {
        float speedInput = Input.GetAxis("Speed");
        
        if(speedInput == 0)
        {
            CurrentSpeed = Mathf.Clamp(CurrentSpeed - (Handling * Time.deltaTime), 0, MaxSpeed);
        }
        //Faster
        else if(speedInput > 0)
        {
            float increase = (CurrentSpeed + Handling) * Time.deltaTime;

            CurrentSpeed = Mathf.Clamp(CurrentSpeed + increase, 0, MaxSpeed);
        }
        //Slower
        else if(speedInput < 0)
        {
            float decrease = (CurrentSpeed + Handling) * Time.deltaTime;

            CurrentSpeed = Mathf.Clamp(CurrentSpeed - decrease, 0, MaxSpeed);
        }
    }
    void SetRotation()
    {
        float rotation = Input.GetAxis("Rotate");
        float turn =
            Mathf.Clamp(CurrentSpeed, 0, 1) * 
            -rotation *
            (Handling * 20) * 
            Time.deltaTime;

        Vector3 value = Vector3.zero;

        if(CameraRotation)
            value.z = transform.rotation.eulerAngles.z + turn;
        else
            value.z = Mathf.Clamp(
                transform.rotation.eulerAngles.z > 180 ?
                (transform.rotation.eulerAngles.z - 360) + turn :
                transform.rotation.eulerAngles.z + turn,
                -TurnClamp,
                TurnClamp);

        transform.rotation = Quaternion.Euler(value);
    }
}
