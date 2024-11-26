using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private Animator animator;
    bool isPreparation = false;
    public void StartGame()
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnExitButtonClick()
    {
        Application.Quit();
    }
    public void OnStartButtonClick()
    {
        if (isPreparation)
        {
            StartGame();
        }
        else
        {
            animator.SetBool("preparation", true);
        }
    }
}
