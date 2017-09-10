using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    [SerializeField]
    private GameObject startMenu;
    [SerializeField]
    private GameObject overMenu;
    
    // the player prefab to instantiate
    [SerializeField]
    private GameObject playerPrefab;
    
    // singleton instantiation
    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            // is someone is mess with GameManager singleton before the current
            // GameManager awakening, destroy it and receate a new one with the current instance
            Destroy(gameObject);
            
        }
        
        DontDestroyOnLoad(gameObject);
        
        Assert.IsNotNull(startMenu);
        Assert.IsNotNull(overMenu);
        Assert.IsNotNull(playerPrefab);

        // start with startMenu
        startMenu.SetActive(true);
    }

    private bool gameOver = false;
    public bool GameOver { // getter method for gameOver state
        get { return gameOver; }
    }

    private bool playerActivated = false;
    public bool PlayerActivated {
        get { return playerActivated; }
    }

    private bool gameStarted = false;
    public bool  GameStarted {
        get { return gameStarted; }
    }
    
    public void OnPlayerActivated() {
        gameStarted = true;
        playerActivated = true;
        gameOver = false;
    }
    
    public void OnGameOver() {
        startMenu.SetActive(false);
        overMenu.SetActive(true);
        
        gameOver = true;
        playerActivated = false;
        gameStarted = false;
    }
    
    public void StartGame() {
        // diable the menu ui
        startMenu.SetActive(false);
        overMenu.SetActive(false);

        gameStarted = true;
        playerActivated = false;
        gameOver = false;
    }
    
    public void Restart() {
        startMenu.SetActive(false);
        overMenu.SetActive(false);
        
        gameStarted = true;
        gameOver = false;
        playerActivated = false;

        //reinstantiate the player
        Instantiate(playerPrefab);
    }
    
    public void ToMainMenu() {
        startMenu.SetActive(true);
        overMenu.SetActive(false);
        
        gameOver = false;
        gameStarted = false;
        playerActivated = false;
    }
	
}
