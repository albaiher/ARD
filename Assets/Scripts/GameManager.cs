using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public UIScore player1Score;
    public UIScore player2Score;
    public UIManager uiManager;
    [SerializeField] private int scoreToWin = 3;
    private AudioSource winAudio;
    private bool win = false;

    public static GameManager instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        this.winAudio = this.GetComponent<AudioSource>();
    }


    void Update()
    {
        if (PlayerHasWon() && !win)
        {
            WinGame();
        }
    }
    private void WinGame() 
    {
        win = true;
        winAudio.Play();
        uiManager.WinGame();
    }

    private bool PlayerHasWon()
    {
        return player1Score.Score.Value >= scoreToWin || player2Score.Score.Value >= scoreToWin;
    }
}
