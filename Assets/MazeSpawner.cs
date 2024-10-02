using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSpawner : MonoBehaviour
{
    public GameObject CellPrefab;
    public float scale = 10f;
    public GameObject KeyPrefab; 
    public KeySpawner KeySpawner;

    public GameObject DoorPrefab;
    public DoorSpawner DoorSpawner;

    private void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        MazeGeneratorCell[,] maze = generator.GenerateMaze();

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector2(x*scale, y*scale), Quaternion.identity).GetComponent<Cell>();

                c.WallLeft.SetActive(maze[x, y].WallLeft);
                c.WallBottom.SetActive(maze[x, y].WallBottom);
            }
        }

        if (DoorSpawner != null)
        {
            DoorSpawner.DoorPrefab = DoorPrefab;
            DoorSpawner.SpawnDoor(maze, 22, 22);
        }

        if (KeySpawner != null)
        {
            KeySpawner.KeyPrefab = KeyPrefab;  
            KeySpawner.SpawnKeys(maze); 
        }
    }

}
