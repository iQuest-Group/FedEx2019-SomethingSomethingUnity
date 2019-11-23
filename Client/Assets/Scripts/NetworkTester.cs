using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

public class PlayerPosition
{
    public PlayerPosition()
    {

    }
    public PlayerPosition(int Id, string Name, int PosX, int PosY)
    {
        this.Id = Id;
        this.PosX = PosX;
        this.PosY = PosY;
        this.Name = Name;
    }
    public int Id { get; set; }

    public string Name { get; set; }

    public int PosX { get; set; }

    public int PosY { get; set; }

    public int Order { get; set; }
}


public class NetworkTester : MonoBehaviour
{
    [SerializeField] private Transform netPlayer;

    public HubConnection connection;
    private Vector3 otherPlayerPos;
    private string hubUrl = "http://localhost:55386/server";

    private void Start()
    {
        otherPlayerPos = netPlayer.position;
        InitNetworkTesting();
    }

    private void InitNetworkTesting()
    {
        Debug.Log("Initialize SignalR :)");
        connection = new HubConnectionBuilder()
            .WithUrl(hubUrl)
            .Build();
        connection.Closed += async (error) =>
        {
            Debug.Log("There was some error!");
            await Task.Delay(UnityEngine.Random.Range(0, 5) * 1000);
            Debug.Log("Connecting!");
            await connection.StartAsync();
            Debug.Log("SignalR Started");
            
        };
        // connection.On<string>("ReceiveMessage", (message) =>
        // {
        //     Debug.Log($"ReceiveMessage: {message}");
        // });
        // connection.On<float, float>("Move", (movex, movey) =>
        // {
        //     Debug.Log("Doing move!!!!! ");
        //     MovePlayer(new Vector3(movex, 0f, movey));
        // });

        //connection.On<PlayerPosition>("ReceiveSingleSpawnPoint", (playerPos) =>
        //{
        //    UnityMainThreadDispatcher.Instance().Enqueue(SpawnPlayer(playerPos));
        //});

        connection.On<List<PlayerPosition>>("ReceiveSpawnPoints", (playerPos) =>
        {
            UnityMainThreadDispatcher.Instance().Enqueue(SpawnPlayers(playerPos));
        });

        connection.On<PlayerPosition>("ReceiveMovement", (playerPos) =>
        {
            UnityMainThreadDispatcher.Instance().Enqueue(ReceiveMovement(playerPos));
        });

        connection.On("ReceiveGameReset", () =>
        {
            UnityMainThreadDispatcher.Instance().Enqueue(ResetGame());
        });

        connection.On<int>("ReceiveGameLeave", (id) =>
        {
            UnityMainThreadDispatcher.Instance().Enqueue(LeaveGame(id));
        });

        Connect();


    }

    public IEnumerator ReceiveMovement(PlayerPosition playerPosition)
    {
        if(playerPosition.Id != GameManager.Instance.GetCurrentPlayerPosition().Id)
        {
            GameManager.Instance.levelManager.MovePlayer(playerPosition);
        }
        
        yield return null;
    }
    
    public void SendMovement(PlayerPosition playerPosition)
    {
        connection.SendAsync("SendMovement", playerPosition);
    }

    private IEnumerator LeaveGame(int id)
    {
        Debug.Log("Leave Game player with id: " + id);
        GameManager.Instance.LeaveGame(id);
        yield return null;
    }

    private IEnumerator ResetGame()
    {
        Debug.Log("Reset Game");
        GameManager.Instance.ResetGame();
        yield return null;
    }

    private IEnumerator SpawnPlayers(List<PlayerPosition> playerPositions)
    {
        GameManager.Instance.levelManager.SpawnPlayer(playerPositions);
        yield return null;
    }

    private async void Connect()
    {
        try
        {
            Debug.Log("Connecting!");
            await connection.StartAsync();
            Debug.Log("SignalR Started");
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private async void Send(string msg)
    {
        try
        {
            await connection.InvokeAsync("SendMessageToAll", connection.GetHashCode().ToString() + " --- " + msg);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void Update()
    {
        netPlayer.position = otherPlayerPos;
    }
}