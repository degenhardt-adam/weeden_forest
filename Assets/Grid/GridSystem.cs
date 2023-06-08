using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject gridPrefab;
    public GameObject[,] gridArray;
    public Piece[,] pieceArray;
    public Camera cam;
    public GameObject generatorPrefab;
    public TurnSystem turnSystem;

    private void Start()
    {
        gridArray = new GameObject[width, height];
        pieceArray = new Piece[width, height];
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

    private void Update()
    {
        if (turnSystem.currentPhase == Phase.Editing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mouseWorldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
                int x = Mathf.FloorToInt(mouseWorldPosition.x + 0.5f);
                int y = Mathf.FloorToInt(mouseWorldPosition.y + 0.5f);

                if (x >= 0 && y >= 0 && x < width && y < height)
                {
                    if (pieceArray[x, y] == null)
                    {
                        GameObject newGenerator = Instantiate(generatorPrefab, new Vector3(x, y, 0), Quaternion.identity);
                        Piece generatorPiece = newGenerator.GetComponent<Piece>();
                        pieceArray[x, y] = generatorPiece;
                        turnSystem.AddGenerator(newGenerator.GetComponent<Generator>());
                    }
                }
            }
        }
    }
}