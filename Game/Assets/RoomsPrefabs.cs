using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsPrefabs : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField]
    private GameObject skeletonPrefab;
    [SerializeField]
    private GameObject torret;
    

    public void generateRandomRoom(Vector2Int center, ref List<Vector2Int> SpikesPositions)
    {

        SpikesPositions.Add(new Vector2Int(center.x + 3, center.y + 3));
    }
}
