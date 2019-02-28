using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeToEnd = 60f;

    private TMP_Text timerText;
    private float timer;
    private bool isTimerOn;

    public static event Action<bool> OnTimerOn = delegate { };
    public static event Action OnTimerEnd = delegate { };
    public static event Action OnReset = delegate { };

    private void Awake()
    {
        timerText = GetComponent<TMP_Text>();
        OnTimerEnd += StopTimer;
    }

    private void Start()
    {
        ResetTimer();
        //DelayBeforeStart();
    }

    public void DelayBeforeStart()
    {
        //StopAllCoroutines();
        StartCoroutine(DelayBeforeStartCoroutine());
    }

    public void ResetTimer()
    {
        timer = timeToEnd;
        UpdateHud();
        OnReset();
        DelayBeforeStart();
    }

    public void StartTimer()
    {
        isTimerOn = true;
    }

    public void StopTimer()
    {
        isTimerOn = false;
        StopAllCoroutines();
    }

    private void Update()
    {
        OnTimerOn(isTimerOn);

        if (isTimerOn)
        {
            if (timer < 0f)
            {
                OnTimerEnd();
            }
            timer -= Time.fixedDeltaTime;
            UpdateHud();
        }

        // if (Input.GetMouseButtonDown(0))
        // {
        //     StartTimer();
        // }

        // if (Input.GetMouseButtonDown(1))
        // {
        //     ResetTimer();
        // }

    }

    private void UpdateHud()
    {
        string minutes = Mathf.Floor(timer / 60f).ToString("00");
        string seconds = (timer % 60).ToString("00");
        timerText.SetText(minutes + ":" + seconds);
    }

    private IEnumerator DelayBeforeStartCoroutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        StartTimer();
    }
}
