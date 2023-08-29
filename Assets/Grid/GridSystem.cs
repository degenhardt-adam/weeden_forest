using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public int width;
    public int height;
    public GameObject gridPrefab;
    public Camera cam;
    public ShopItem selectedShopItem;
    public TurnSystem turnSystem;
    public PowerManager powerManager;

    public GameObject[,] gridArray;
    public Piece[,] pieceArray;
    public List<Vector2Int> activationOrder;

    public static GridSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GridSystem instance found!");
            return;
        }
        instance = this;
    }

    private void Start()
    {
        gridArray = new GameObject[width, height];
        pieceArray = new Piece[width, height];
        CreateGrid();
        GenerateActivationOrder();

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
                GameObject newCell = Instantiate(gridPrefab, new Vector3(x, y, 1), Quaternion.identity);
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

                Vector2Int pos = new Vector2Int(x, y);
                if (IsInGrid(pos) && pieceArray[x, y] == null && selectedShopItem != null)
                {
					if (powerManager.Power >= selectedShopItem.powerCost)
                    {
                        GameObject newGenerator = InstantiateOnTile(selectedShopItem.devicePrefab, pos);
                        AddPieceToTile(newGenerator.GetComponent<Piece>(), pos);
                        powerManager.Power -= selectedShopItem.powerCost;
                    }
                }
            }
        }
    }

    public bool IsInGrid(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < width && pos.y >= 0 && pos.y < height;
    }

    public GameObject InstantiateOnTile(GameObject prefab, Vector2Int pos)
    {
        if (IsInGrid(pos))
        {
            Vector3 tilePos = gridArray[pos.x, pos.y].transform.position;
            return Instantiate(prefab, tilePos + new Vector3(0, 0, -1), Quaternion.identity);
        }
        else return null;
    }

    public void AddPieceToTile(Piece piece, Vector2Int tile)
    {
        if (pieceArray[tile.x, tile.y] == null)
        {
            pieceArray[tile.x, tile.y] = piece;
            piece.gridPosition = tile;
        }
    }

    private void GenerateActivationOrder()
    {
        int x = (width - 1) / 2;
        int y = height / 2;
        AddToActivationOrder(x, y);

        for (int layer = 1; layer <= Mathf.Max(width, height); layer++)
        {
            // Move right
            for (int i = 0; i < layer * 2 - 1; i++)
            {
                x++;
                AddToActivationOrder(x, y);
            }
            // Move down
            for (int i = 0; i < layer * 2 - 1; i++)
            {
                y--;
                AddToActivationOrder(x, y);
            }
            // Move left
            for (int i = 0; i < layer * 2; i++)
            {
                x--;
                AddToActivationOrder(x, y);
            }
            // Move up
            for (int i = 0; i < layer * 2; i++)
            {
                y++;
                AddToActivationOrder(x, y);
            }
        }
    }

    private void AddToActivationOrder(int x, int y)
    {
        Vector2Int pos = new Vector2Int(x, y);
        if (IsInGrid(pos))
        {
            activationOrder.Add(pos);
        }
    }

}