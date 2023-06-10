using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Editing, Player, Enemy }

public class TurnSystem : MonoBehaviour
{
    public Phase currentPhase;
    public List<Generator> generators;

    private void Start()
    {
        currentPhase = Phase.Editing;
        generators = new List<Generator>();
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

    public void AddGenerator(Generator generator)
    {
        generators.Add(generator);
    }

    private IEnumerator ResolvePlayerPhase()
    {
        foreach (Generator generator in generators)
        {
            yield return StartCoroutine(generator.Activate());
        }

        // After all generators have activated, move to enemy phase
        currentPhase = Phase.Enemy;
    }
}

