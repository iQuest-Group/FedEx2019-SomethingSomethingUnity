using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    // private string serverUrl = "http://localhost:55386";
    private string serverUrl = "https://fedex2019gameserver.azurewebsites.net";

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

    public async Task OnJoinNewGameClickedAsync()
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync($"{serverUrl}/api/game/spawn");
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
            HttpResponseMessage response = await client.PostAsync($"{serverUrl}/api/game/reset", httpContent);
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
            //var data = new { id = GameManager.Instance.levelManager.GetCurrentPlayerId() };
            var json = JsonConvert.SerializeObject(GameManager.Instance.levelManager.GetCurrentPlayerId());
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync($"{serverUrl}/api/game/leave", stringContent);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            UIScreen.SetActive(true);
            leaveButton.gameObject.SetActive(false);
        }
        catch (HttpRequestException e)
        {
            Debug.LogError(e.Message);
        }
    }
}
