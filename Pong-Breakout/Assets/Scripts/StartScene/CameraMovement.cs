using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform table;

    private void Awake()
    {
        StartSceneUIEvents.OnClickEvent += ChangeCameraSpeed;
    }

    private void Update()
    {
        LookAtTable();
        ClampMovement();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeCameraSpeed();
        }
    }

    private void ChangeCameraSpeed()
    {
        StartCoroutine(ChangeCameraSpeedCoroutine());
    }

    private void ClampMovement()
    {
        float x = Mathf.Clamp(transform.position.x, -36f, 36f);
        float z = Mathf.Clamp(transform.position.z, -36f, 36f);
        transform.position = new Vector3(x, transform.position.y, z);
    }

    private void LookAtTable()
    {
        transform.LookAt(table);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    private IEnumerator ChangeCameraSpeedCoroutine()
    {
        speed = 180f;
        yield return new WaitForSecondsRealtime(1f);
        speed = 1f;
    }

    private void OnDestroy()
    {
        StartSceneUIEvents.OnClickEvent -= ChangeCameraSpeed;
    }

}
