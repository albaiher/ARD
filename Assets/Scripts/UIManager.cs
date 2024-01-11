using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private UIScore scoreBoard;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject winStatement;



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

    public void ExitGame() 
    {
        Application.Quit();
    }
}
