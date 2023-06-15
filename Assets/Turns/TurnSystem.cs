using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Editing, Player, Enemy }

public class TurnSystem : MonoBehaviour
{
    public GridSystem gridSystem;
    public Phase currentPhase;

    private void Start()
    {
        currentPhase = Phase.Editing;
    }

    private void Update()
    {
        switch (currentPhase)
        {
            case Phase.Editing:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    currentPhase = Phase.Player;
                    StartCoroutine(ResolvePlayerPhase());
                }
                break;
            case Phase.Player:
                // Player phase is resolved in ResolvePlayerPhase()
                break;
            case Phase.Enemy:
                // Resolve enemy phase here
                // After resolving:
                currentPhase = Phase.Editing;
                break;
        }
    }

    private IEnumerator ResolvePlayerPhase()
    {
        foreach (var pos in gridSystem.activationOrder)
        {
            GameObject gridTile = gridSystem.gridArray[pos.x, pos.y];
            // Change color to green
            var spriteRenderer = gridTile.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.green, 0.5f);
            
            if (gridSystem.pieceArray[pos.x, pos.y] is not null)
            {
                yield return gridSystem.pieceArray[pos.x, pos.y].Activate();
            }

            // Change color back to white over time
            StartCoroutine(FadeColorBackRoutine(spriteRenderer));

            // Pause for x frames before next tile activation
            for (int i = 0; i < 7; i++)
            {
                yield return null;
            }
        }

        // Now that all player actions are resolved, proceed to the enemy phase
        currentPhase = Phase.Enemy;
    }

    private IEnumerator FadeColorBackRoutine(SpriteRenderer spriteRenderer)
    {
        while (spriteRenderer.color != Color.white)
        {
            spriteRenderer.color = Color.Lerp(spriteRenderer.color, Color.white, 0.03f);
            yield return null;
        }
    }

}

