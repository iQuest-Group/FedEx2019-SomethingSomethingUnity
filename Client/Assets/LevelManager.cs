using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PlayerPrefab;

    public List<GameObject> players = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(List<PlayerPosition> playerPositions)
    {
        int maxOrder = playerPositions.Max(x => x.Order);
        foreach (var playerPosition in playerPositions)
        {
            if(players.Exists(x => x.GetComponent<PlayerManager>().ID == playerPosition.Id))
            {
                continue;
            }
            var player = Instantiate(PlayerPrefab, new Vector3(playerPosition.PosX, 0, playerPosition.PosY), Quaternion.identity, transform);
            var playerManager = player.GetComponent<PlayerManager>();
            playerManager.ID = playerPosition.Id;
            playerManager.playerPosition = playerPosition;
            playerManager.nextPosition = new PlayerPosition(playerPosition.Id, playerPosition.Name, playerPosition.PosX, playerPosition.PosY);
            if (playerPosition.Order == maxOrder && !players.Exists(x => x.GetComponent<PlayerManager>().currentPlayer))
            {
                playerManager.currentPlayer = true;
            }
            players.Add(player);
        }
    }

    public void DestroPlayer(int id)
    {
        var player = players.Find(p => p.GetComponent<PlayerManager>().ID == id);
        if(player != null)
        {
            players.Remove(player);
            GameObject.Destroy(player);
        }
    }

    public int GetCurrentPlayerId()
    {
        int id = -1;
        foreach(var player in players)
        {
            var pm = player.GetComponent<PlayerManager>();
            if (pm.currentPlayer)
            {
                id = pm.ID;
            }

        }
        return id;
    }

    public PlayerPosition GetCurrentPlayer()
    {
        foreach (var player in players)
        {
            var pm = player.GetComponent<PlayerManager>();
            if (pm.currentPlayer)
            {
                return pm.playerPosition;
            }

        }
        return null;
    }

    public void MovePlayer(PlayerPosition playerPosition)
    {
        GameObject player = players.Find(p => p.GetComponent<PlayerManager>().ID == playerPosition.Id);
        MoveController moveController = player.GetComponent<MoveController>();
        moveController.CalculateMoveInGrid(playerPosition);
    }
}
