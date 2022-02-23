using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] Transform[] spawnPoints;


    public Transform[] getSpawnPoints()
    {
        return spawnPoints;
    }
}
