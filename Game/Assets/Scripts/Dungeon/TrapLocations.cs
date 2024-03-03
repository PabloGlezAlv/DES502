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

    [ExecuteInEditMode]
    void OnDrawGizmos()
    {
        for (int i = 0; i < holesLocations.Count; i++)
        {
            Vector2 loc = holesLocations[i];
            loc += new Vector2(0.5f, 0.5f);
            Gizmos.color = Color.black;
            Gizmos.DrawCube(loc, Vector3.one);
        }
        for (int j = 0; j < spikesLocations.Count; j++)
        {
            Vector2 loc = spikesLocations[j];
            loc += new Vector2(0.5f, 0.5f);
            Gizmos.color = Color.white;
            Gizmos.DrawCube(loc, Vector3.one);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Vector3.zero, new Vector2(22, 10));
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(Vector3.zero, new Vector2(18, 6));

    }
}
