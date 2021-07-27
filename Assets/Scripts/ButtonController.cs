using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("ModeFight", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SlideFightButton()
    {
        //transform.Translate(200f, 0, 0);
        animator.SetBool("ModeFight", true);
        animator.SetBool("ModeEscape", false);
    }

    public void SlideEscapeButton()
    {
        animator.SetBool("ModeEscape", true);
        animator.SetBool("ModeFight", false);
    }
}
