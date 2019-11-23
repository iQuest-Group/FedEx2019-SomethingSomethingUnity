using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Button startButton;
    public Button resetButton;
    public Button leaveButton;
    public GameObject UIScreen;
    HttpClient client = new HttpClient();
    void Start()
    {
        resetButton.onClick.AddListener(async delegate { await OnResetGameAsync(); });
        startButton.onClick.AddListener(async delegate { await OnJoinNewGameClickedAsync(); });
        leaveButton.onClick.AddListener(async delegate { await OnLeaveGameAsync(); });
        leaveButton.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public async Task OnJoinNewGameClickedAsync()
    { 
        try
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:55386/api/game/spawn");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            UIScreen.SetActive(false);
            leaveButton.gameObject.SetActive(true);
        }
        catch (HttpRequestException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async Task OnResetGameAsync()
    {
        try
        {
            var httpContent = new StringContent("content");
            HttpResponseMessage response = await client.PostAsync("http://localhost:55386/api/game/reset", httpContent);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            Debug.LogError(e.Message);
        }
    }

    public async Task OnLeaveGameAsync()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync("http://localhost:55386/api/game/spawn");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            UIScreen.SetActive(false);
        }
        catch (HttpRequestException e)
        {
            Debug.LogError(e.Message);
        }
    }
}
