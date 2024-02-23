using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLocations : MonoBehaviour
{
    [SerializeField]
    List<Vector2Int> spikesLocations = new List<Vector2Int>();

    [SerializeField]
    List<Vector2Int> holesLocations = new List<Vector2Int>();

    public List<Vector2Int> GetSpikesRelative() { return spikesLocations; }

    public List<Vector2Int> GetSpikesFinalLocation(Vector2Int center)
    {
        List<Vector2Int> finalLocation = spikesLocations;

        for (int i = 0; i <finalLocation.Count;i++)
        {
            finalLocation[i] += center;
        }

        return finalLocation; 
    }

    public List<Vector2Int> GetHolesRelative() { return holesLocations; }

    public List<Vector2Int> GetHolesFinalLocation(Vector2Int center)
    {
        List<Vector2Int> finalLocation = holesLocations;

        for (int i = 0; i < finalLocation.Count; i++)
        {
            finalLocation[i] += center;
        }

        return finalLocation;
    }
}
