using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public Vector2Int gridPosition;

    public virtual IEnumerator Activate()
    {
        return null;
    }
}
