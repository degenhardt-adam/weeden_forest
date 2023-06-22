using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Piece
{

    public Animator animator;

    public override IEnumerator Activate()
    {
        // Set the activation parameter
        animator.SetTrigger("GunActivate");

        // Wait for the animation to start
        yield return new WaitWhile(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("GunActivate"));

        // Wait for the animation to finish
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
    }
}
