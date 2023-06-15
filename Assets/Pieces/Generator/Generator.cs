using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Piece
{
    public Animator animator;
    public PowerManager powerManager;
    public Piece frontPiece;
    public Piece rightPiece;
    public Piece backPiece;
    public Piece leftPiece;

    // Start is called before the first frame update
    void Start()
    {
        // Get the PowerManager
        powerManager = FindObjectOfType<PowerManager>();
        if (powerManager == null)
        {
            Debug.LogError("No PowerManager found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override IEnumerator Activate()
    {
        yield return PlayAnimation("GeneratorActivate");
        powerManager.Power += 5;
    }

    private IEnumerator PlayAnimation(string stateName)
    {
        // Set the activation parameter
        animator.SetTrigger(stateName);

        // Wait for the animation to start
        yield return new WaitWhile(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(stateName));

        // Wait for the animation to finish
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);
    }
}
