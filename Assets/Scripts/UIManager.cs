using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Netcode;

public class UIManager : MonoBehaviour
{

    [SerializeField] private UIScore scoreBoard;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject winStatement;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;



    void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            this.ActivateMenu();
        }
    }
    
    public void Replay() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ActivateMenu() 
    {
        Time.timeScale = 0f;
        mainMenu.SetActive(true);
    }

    public void PlayGame() 
    {
        Time.timeScale = 1f;
        mainMenu.SetActive(false);
    }

    public void WinGame()
    {
        this.ActivateMenu();
        this.playButton.SetActive(false);
        this.winStatement.SetActive(true);
    }

    public void StartHost() 
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient() 
    {
        NetworkManager.Singleton.StartClient();
    }
    public void ExitGame() 
    {
        Application.Quit();
    }
}
