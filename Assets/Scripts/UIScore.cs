using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public int playerID;
    private int score = 0;
    private Text UI;

    public int Score { 
        get {return score;} 
        set {score = value;} 
    }

    // Start is called before the first frame update
    void Start()
    {
        SphereMovement.instance.onPlayerHitSphere += PlayerHitSphere;
        UI = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayerHitSphere(int id) 
    {
        this.score++;
        UI.text = this.score.ToString();
    }
}
