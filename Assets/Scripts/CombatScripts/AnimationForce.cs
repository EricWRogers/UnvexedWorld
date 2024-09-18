using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationForce : MonoBehaviour
{
    public Animator animator;
    public string currentAnim;
    public bool melee = false;
    public bool ranged = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
        currentAnim = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
    }

    // Update is called once per frame
    void Update()
    {
        if(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == currentAnim)
        {
            animator.SetBool("Melee", melee);
            animator.SetBool("Ranged", ranged);
        }
        else
        {
            currentAnim = animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
            melee = false;
            ranged = false;
            animator.SetBool("Melee", melee);
            animator.SetBool("Ranged", ranged);
        }
        
        
    }
}
