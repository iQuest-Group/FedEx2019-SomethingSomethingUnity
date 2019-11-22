using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;

public class NetworkTester : MonoBehaviour
{
    [SerializeField] private Transform netPlayer;

    private HubConnection connection;
    private Vector3 otherPlayerPos;
    private string hubUrl = "http://localhost:5000/messages";

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
        connection.On<string>("ReceiveMessage", (message) =>
        {
            Debug.Log($"ReceiveMessage: {message}");
        });
        connection.On<float, float>("Move", (movex, movey) =>
        {
            Debug.Log("Doing move!!!!! ");
            MovePlayer(new Vector3(movex, 0f, movey));
        });

        Connect();
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