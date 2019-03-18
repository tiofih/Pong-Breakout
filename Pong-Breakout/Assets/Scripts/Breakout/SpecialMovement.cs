using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1f;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Ball"))
        {
            Collider ballColl = GameObject.FindGameObjectWithTag("Ball").GetComponent<Collider>();
            Physics.IgnoreCollision(GetComponent<Collider>(), ballColl);
        }
        Timer.OnTimerOn += Move;
    }

    private void Move(bool timer)
    {
        if (timer)
        {
            transform.position += Vector3.right * movementSpeed * Time.deltaTime;
        }
    }

    private void OnDestroy()
    {
        Timer.OnTimerOn -= Move;
    }
}
