using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(PlayerPosition playerPosition)
    {
        var player = Instantiate(PlayerPrefab, new Vector3(playerPosition.PosX, 0, playerPosition.PosY), Quaternion.identity, transform);
        Debug.Log("PLayer" + player.name);
    }
}
