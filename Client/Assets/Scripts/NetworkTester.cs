using System;
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
}


public class NetworkTester : MonoBehaviour
{
    [SerializeField] private Transform netPlayer;

    private HubConnection connection;
    private Vector3 otherPlayerPos;
    // private string hubUrl = "http://localhost:55386/server";
    private string hubUrl = "https://fedex2019gameserver.azurewebsites.net/server";

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



        connection.On<PlayerPosition>("ReceiveMovement", (playerPos) =>
        {
            Debug.Log($"ReceiveMessage: {playerPos}");
        });

        connection.On("PingBack", () =>
        {
            theTime.Stop();
            Debug.Log($"Return back time is: {theTime.ElapsedMilliseconds} ms");
        });

        Connect();

    }

    private System.Diagnostics.Stopwatch theTime;

    public void TestConnectionLag()
    {

        theTime = new System.Diagnostics.Stopwatch();
        theTime.Start();
        connection.InvokeAsync("PingHub");
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

    private void MovePlayer(Vector3 deltaMove)
    {

        Debug.Log($"Moving {otherPlayerPos} by {deltaMove} ");
        otherPlayerPos += deltaMove;
        Debug.Log("moved");
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