using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject gridPrefab;
    public GameObject[,] gridArray;
    public Camera cam;

    private void Start()
    {
        gridArray = new GameObject[width, height];
        CreateGrid();

        // Center the camera over the grid.
        if (cam != null)
        {
            Vector3 centerPosition = new Vector3(width / 2.0f - 0.5f, height / 2.0f - 0.5f, cam.transform.position.z);
            cam.transform.position = centerPosition;
        }
    }

    private void CreateGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newCell = Instantiate(gridPrefab, new Vector3(x, y, 0), Quaternion.identity);
                newCell.transform.parent = transform;
                gridArray[x, y] = newCell;
            }
        }
    }
}