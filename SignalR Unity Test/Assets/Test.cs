using UnityEngine;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;

public class Test : MonoBehaviour
{
    private int x;
    private static HubConnection connection;
    void Start()
    {
        SetPosition(5);
        Debug.Log("Hello World!");
        connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:59890/chat")
            .Build();
        connection.Closed += async (error) =>
        {
            await Task.Delay(Random.Range(0, 5) * 1000);
            await connection.StartAsync();
        };

        Connect();

        Send(Random.Range(0, 5).ToString());
        Send(Random.Range(0, 5).ToString());
        Send(Random.Range(0, 5).ToString());
    }

    private async void Connect()
    {
        connection.On<string, string>("broadcastMessage", (name, message) =>
        {
            Debug.Log($"{name}: {message}");
            SetPosition(int.Parse(message));
        });

        try
        {
            await connection.StartAsync();

            Debug.Log("Connection started");
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
            await connection.InvokeAsync("Send", connection.GetHashCode().ToString(), msg);
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void SetPosition(int x)
    {
        this.x = x;
    }

    private void Update()
    {
        transform.position = new Vector2(x, 0);
    }
}