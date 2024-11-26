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
        animator.SetBool("preparation", true);
    }
    public void OnBackButtonClick()
    {
        animator.SetBool("preparation", false);
    }
}
