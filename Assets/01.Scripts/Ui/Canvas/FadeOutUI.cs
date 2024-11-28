using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FadeOutUI : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] public UnityEvent onFadeOutEnd;
    public void FadeOut()
    {
        animator.SetBool("FadeOut", true);
    }
    public void OnAnimEnd()
    {
        onFadeOutEnd?.Invoke();
    }
}
