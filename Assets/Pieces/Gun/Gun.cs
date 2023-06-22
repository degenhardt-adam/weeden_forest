using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Piece
{

    public Animator animator;
    public GameObject laserPrefab;
    public GameObject laserEndPrefab;
    public int laserRange = 3;
    public Vector2Int direction = new Vector2Int(0, 1);

    private GridSystem gridSystem;
    private List<GameObject> lasers = new List<GameObject>();

    private void Start()
    {
        gridSystem = GridSystem.instance;
    }

    public override IEnumerator Activate()
    {
        // Set the activation parameter
        animator.SetTrigger("GunActivate");

        // Wait for the animation to start
        yield return new WaitWhile(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("GunActivate"));

        // Wait for the animation to finish
        yield return new WaitWhile(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        Vector2Int currentPosition = gridPosition;
        for (int i = 0; i < laserRange; i++)
        {
            currentPosition += direction;
            if (gridSystem.IsInGrid(currentPosition))
            {
                GameObject prefab = i == laserRange - 1 ? laserEndPrefab : laserPrefab;
                GameObject laser = gridSystem.InstantiateOnTile(prefab, currentPosition);
                lasers.Add(laser);
            }
            else
            {
                break;
            }
            yield return null;
        }

        // Pause while laser displays
        yield return new WaitForSeconds(0.2f);

        foreach (GameObject laser in lasers)
        {
            Destroy(laser);
        }
        lasers.Clear();

        // Transition back to the idle state
        animator.SetTrigger("GunIdle");
    }
}
