using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawner : MonoBehaviour
{
    public GameObject DoorPrefab;

    public void SpawnDoor(MazeGeneratorCell[,] maze, int width, int height)
    {

        int x = width - 1;  
        int y = height / 2;


        Vector2 doorPosition = new Vector2(x * 10 + 5, y * 10 + 5);

        Instantiate(DoorPrefab, doorPosition, Quaternion.identity);

    }
}
