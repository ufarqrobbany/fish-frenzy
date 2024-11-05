using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter()
    {
        animator.SetBool("isHovering", true);
        Debug.Log("Enter");
    }

    public void OnPointerExit()
    {
        animator.SetBool("isHovering", false);
                Debug.Log("Exit");
    }
}

