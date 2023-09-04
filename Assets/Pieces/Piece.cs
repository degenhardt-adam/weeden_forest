using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Vector2Int gridPosition;
    public PowerManager powerManager;

    void Awake()
    {
        // Get the PowerManager
        powerManager = FindObjectOfType<PowerManager>();
        if (powerManager == null)
        {
            Debug.LogError("No PowerManager found in the scene.");
        }
    }

    public virtual IEnumerator Activate()
    {
        return null;
    }
}
