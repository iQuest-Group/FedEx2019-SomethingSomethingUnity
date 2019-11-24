using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager levelManager;
    public HUDManager hUDManager;
    public NetworkTester network;

    private void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("Level").GetComponent<LevelManager>();
        hUDManager = GameObject.FindGameObjectWithTag("Hud").GetComponent<HUDManager>();
        network = GameObject.FindGameObjectWithTag("Network").GetComponent<NetworkTester>();

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void LeaveGame(int id)
    {
        levelManager.DestroPlayer(id);
    }

    public PlayerPosition GetCurrentPlayerPosition()
    {
        return levelManager.GetCurrentPlayer();
    }
}
