using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject TilePrefab;
    private static readonly int Width = 10;
    private static readonly int Height = 10;

    bool[,] currentGrid = new bool[Width, Height];
    bool[,] nextGrid = new bool[Width, Height];

    GameObject[,] tiles = new GameObject[Width, Height];

    private float TimeAccu = 0.0f;
    private bool isRunning;

    [SerializeField]
    private TextMeshProUGUI statusTextLabel;

    // Start is called before the first frame update
    void Start()
    {
        // Generate tiles
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
				// Clear the grid
				currentGrid[x, y] = false;
				nextGrid[x, y] = false;

                // Instantiate the tile
                tiles[x, y] = Instantiate(TilePrefab, new Vector3(x, 0, y) * 1.05f, Quaternion.identity);

                //tiles[x,y].SetActive(false);
                tiles[x, y].GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            }
        }

		currentGrid[3, 5] = true;
		currentGrid[4, 5] = true;
		currentGrid[5, 5] = true;
		currentGrid[4, 4] = true;

        tiles[3, 5].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        tiles[4, 5].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        tiles[5, 5].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
        tiles[4, 4].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            statusTextLabel.text = "Stopped";
            return;
        }

        TimeAccu -= Time.deltaTime;

        if (TimeAccu > 0)
        {
            statusTextLabel.text = "Until tick:" + TimeAccu.ToString("0.00");
            return;
        }

        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                //Ruuls
                // 1. Fewer than 2 live neighbors --> die
                // 2. 2 or 3 live neighbours --> live on
                // 3. more than 3 live neighbors --> die
                // 4. dead cell with 3 live neighbors --> rebirth

                int numNeighbors = CountNeighbors(ref currentGrid, x, y);


				if (numNeighbors < 2 || numNeighbors > 3) // 1 && 3
                {
                    nextGrid[x, y] = false;
                }
                else if (currentGrid[x, y] == true && (numNeighbors == 2 || numNeighbors == 3)) // 2
                {
                    nextGrid[x, y] = true;
                }
                else if (numNeighbors == 3) // 4
                {
                    nextGrid[x, y] = true;
                }
                else
                {
                    nextGrid[x, y] = false; // Fall through default
                }
            }
        }

        DrawGridOnScreen(ref nextGrid);

        TimeAccu = 3; // reset tick timer

        SwapGrids(ref currentGrid, ref nextGrid);
    }

    private void SwapGrids(ref bool[,] current, ref bool[,] next)
    {
        bool[,] temp = next;

		next = current;
		current = temp;        
    }

    private void DrawGridOnScreen(ref bool[,] drawGrid)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (drawGrid[x, y] == true)
                {
                    tiles[x, y].GetComponentInChildren<MeshRenderer>().material.color = Color.white;
                }
                else
                {
                    tiles[x, y].GetComponentInChildren<MeshRenderer>().material.color = Color.black;
                }
            }
        }
    }

    private int CountNeighbors(ref bool[,] grid, int x, int y)
    {
        int liveNeighbors = 0;

        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j= y - 1; j <= y + 1; j++)
            {
                if (i < 0 || i > Width - 1 || j < 0 || j > Height - 1) continue;

                if (i == x && j == y) continue;

                if (grid[i, j] == true)
                    liveNeighbors++;
            }
        }

        return liveNeighbors;
    }

    public void RunStop()
    {
        isRunning = !isRunning;
    }
}
