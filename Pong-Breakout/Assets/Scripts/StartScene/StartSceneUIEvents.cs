using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneUIEvents : MonoBehaviour
{
    [SerializeField] private Animator settingsPanel;
    [SerializeField] private Animator transitionPanel;
    [SerializeField] private AudioSource audioSource;
    private bool isInScene;
    public static event Action OnClickEvent = delegate { };

    public void ToPongScene()
    {
        OnClickEvent();
        audioSource.Play();
        transitionPanel.SetBool("IsInScene", true);
        SceneManager.LoadScene("PongScene");
    }

    public void ToSettings()
    {
        OnClickEvent();
        audioSource.Play();
        isInScene = !isInScene;
        if (!settingsPanel.enabled)
        {
            settingsPanel.enabled = true;
        }
        settingsPanel.SetBool("IsInScene", isInScene);
    }
}
