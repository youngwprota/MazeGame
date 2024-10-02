using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawner : MonoBehaviour
{
    public GameObject KeyPrefab;
    public float scale = 10f;
    public int NumberOfKeys = 3;

    public void SpawnKeys(MazeGeneratorCell[,] maze)
    {
        List<MazeGeneratorCell> freeCells = new List<MazeGeneratorCell>();

        for (int x = 0; x < maze.GetLength(0)-1; x++)
        {
            for (int y = 0; y < maze.GetLength(1)-1; y++)
            {
                freeCells.Add(maze[x, y]);
            }
        }

        for (int i = 0; i < NumberOfKeys; i++)
        {
            if (freeCells.Count == 0) break;  

            int randomIndex = UnityEngine.Random.Range(0, freeCells.Count);
            MazeGeneratorCell randomCell = freeCells[randomIndex];

            freeCells.RemoveAt(randomIndex);

            Vector2 keyPosition = new Vector2(randomCell.X*scale+scale/2, randomCell.Y*scale+scale/2);

            Instantiate(KeyPrefab, keyPosition, Quaternion.identity);
        }

    }
}
